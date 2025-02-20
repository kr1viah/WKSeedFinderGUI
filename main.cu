#include <iostream>
#include <cuda_runtime.h>
#include <vector>

#define PCG_DEFAULT_INC_64 1442695040888963407ULL
#define Math_TAU 6.2831853071795864769252867666
#define CMP_EPSILON 0.00001

typedef struct { uint64_t state;  uint64_t inc; } pcg32_random_t;
typedef struct {
    int character;
    int abilityCharacter;
    double abilityLevel;
    int itemCounts[8];
    double startTime;
    int32_t colorState;
    double intensity;
    // would do rgb but cba to figure out imports/packages/whatever or to make my own colour converter
} loadout;

// __device__
// bool seenSeeds[4294967296];

// std::vector<char> characterSet = {
//     '0', '1', '2', '2', '3', '4', '5', '6', '7', '8', '9',
//     'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
// };

// helper functions

__forceinline__ __device__
double clamp(double m_a, double m_min, double m_max) {
	if (m_a < m_min) {
		return m_min;
	} else if (m_a > m_max) {
		return m_max;
	}
	return m_a;
}

__forceinline__ __device__
__device__ float lerp(float a, float b, float t) {
    return a + t * (b - a);
}

__device__ void intToChar(uint32_t num, char* str, int maxLength) {
    int index = 0;
    while (num > 0 && index < maxLength - 1) {
        int digit = num % 10;
        str[index++] = '0' + digit;
        num /= 10;
    }
    if (index < maxLength) {
        str[index] = '\0';
    }
}

__forceinline__ __device__
__device__ double pinch(double v) { // function run() uses
	if (v < 0.5) {
		return -v * v;
	}
	return v * v;
}

__device__ double run(double x, double a, double b, double c) { // TorCurve.run() in windowkill
	c = pinch(c);
	x = fmaxf(0, fminf(1, x));
    const float eps = 0.00001f;
	double s = exp(a);
	double s2 = 1.0 / (s + eps);
	double t = fmaxf(0, fminf(1, b));
	double u = c;

	double res, c1, c2, c3;

	if (x < t) {
		c1 = (t * x) / (x + s*(t-x) + eps);
		c2 = t - pow(1/(t+eps), s2-1)*pow(abs(x-t), s2);
		c3 = pow(1/(t+eps), s-1) * pow(x, s);
	} else {
		c1 = (1-t)*(x-1)/(1-x-s*(t-x)+eps) + 1;
		c2 = pow(1/((1-t)+eps), s2-1)*pow(abs(x-t), s2) + t;
		c3 = 1 - pow(1/((1-t)+eps), s-1)*pow(1-x, s);
	}

	if (u <= 0) {
		res = (-u)*c2 + (1+u)*c1;
	} else {
		res = (u)*c3 + (1-u)*c1;
	}

	return res;
}

__forceinline__ __device__
double smoothCorner(double x, double m, double l, double s) { // TorCurve.smoothCorner in windowkill
	double s1 = pow(s/10.0, 2.0);
	return 0.5 * ((l*x + m*(1.0+s1)) - sqrt(pow(abs(l*x-m*(1.0-s1)), 2.0)+4.0*m*m*s1));
}
// end of helper functions

// random number generator (pcg32)

__device__
uint32_t pcg32_random_r(pcg32_random_t* rng);
__device__
void pcg32_srandom_r(pcg32_random_t* rng, uint64_t initstate, uint64_t initseq);
__device__
uint32_t pcg32_boundedrand_r(pcg32_random_t* rng, uint32_t bound);

class RandomPCG {
	pcg32_random_t pcg;
	uint64_t current_seed = 0; // The seed the current generator state started from.
	uint64_t current_inc = 0;

public:
	static const uint64_t DEFAULT_SEED = 12047754176567800795U;
	static const uint64_t DEFAULT_INC = PCG_DEFAULT_INC_64;

    __device__
	RandomPCG(uint64_t p_seed = DEFAULT_SEED, uint64_t p_inc = DEFAULT_INC);

    __device__
	void seed(uint64_t p_seed) {
		current_seed = p_seed;
		pcg32_srandom_r(&pcg, current_seed, current_inc);
	}
    __device__
	uint64_t get_seed() { return current_seed; }

    __device__
	void set_state(uint64_t p_state) { pcg.state = p_state; }
    __device__
	uint64_t get_state() const { return pcg.state; }

    __device__
	uint32_t rand() {
		return pcg32_random_r(&pcg);
	}
    __device__
	uint32_t randbound(uint32_t bounds) {
		return pcg32_boundedrand_r(&pcg, bounds);
	}

    __device__
	double randd() {
		uint32_t proto_exp_offset = rand();
		if (proto_exp_offset == 0) {
			return 0;
		}
		uint64_t significand = (((uint64_t)rand()) << 32) | rand() | 0x8000000000000001U;
		return ldexp((double)significand, -64 - __clzll(proto_exp_offset));
	}
    __device__
	double randf() {
		uint32_t proto_exp_offset = rand();
		if (proto_exp_offset == 0) {
			return 0;
		}
		return (double) (float) (ldexp((double)(rand() | 0x80000001), -32 - __clz(proto_exp_offset)));
	}

    __device__
    double randfn(double p_mean, double p_deviation) {
        double temp = randf();
        if (temp < 0.00001) {
        temp += 0.00001;
    }
        return p_mean + p_deviation * (cos(6.2831853071795864769252867666 * static_cast<double>(randf())) * sqrt(-2.0 * log(static_cast<double>(temp))));
    }

    __device__
	double randomDouble(double p_from, double p_to);
    __device__
	double randomFloat(float p_from, float p_to);
    __device__
	int randomInteger(int p_from, int p_to);
};

__device__
RandomPCG::RandomPCG(uint64_t p_seed, uint64_t p_inc) :
		pcg(),
		current_inc(p_inc) {
	seed(p_seed);
}

__device__
double RandomPCG::randomDouble(double p_from, double p_to) {
	return randd() * (p_to - p_from) + p_from;
}

__device__
double RandomPCG::randomFloat(float p_from, float p_to) {
	return (double) (randf()*(p_to - p_from) + p_from);
}

__device__
int RandomPCG::randomInteger(int p_from, int p_to) {
	if (p_from == p_to) {
		return p_from;
	}
	return randbound(abs(p_from - p_to) + 1) + min(p_from, p_to);
}

__device__
uint32_t pcg32_random_r(pcg32_random_t* rng)
{
    uint64_t oldstate = rng->state;
    
    rng->state = oldstate * 6364136223846793005ULL + (rng->inc|1);
    
    uint32_t xorshifted = ((oldstate >> 18u) ^ oldstate) >> 27u;
    uint32_t rot = oldstate >> 59u;
    return (xorshifted >> rot) | (xorshifted << ((-rot) & 31));
}

__device__
void pcg32_srandom_r(pcg32_random_t* rng, uint64_t initstate, uint64_t initseq)
{
    rng->state = 0U;
    rng->inc = (initseq << 1u) | 1u;
    pcg32_random_r(rng);
    rng->state += initstate;
    pcg32_random_r(rng);
}

__device__
uint32_t pcg32_boundedrand_r(pcg32_random_t *rng, uint32_t bound) {
	uint32_t threshold = -bound % bound;

	for (;;) {
		uint32_t r = pcg32_random_r(rng);
		if (r >= threshold)
			return r % bound;
	}
}

class RandomNumberGenerator {
protected:
	RandomPCG randbase;
public:
    __device__
	void set_seed(uint64_t p_seed) { randbase.seed(p_seed); }
    __device__
	uint64_t get_seed() { return randbase.get_seed(); }

    __device__
	void set_state(uint64_t p_state) { randbase.set_state(p_state); }
    __device__
	uint64_t get_state() const { return randbase.get_state(); }

    __device__
	uint32_t randbound(uint32_t bounds) {
		return randbase.randbound(bounds);
	}
    __device__
	uint32_t randi() { return randbase.rand(); }
    __device__
	double randf() { return randbase.randf(); }
    __device__
	double randf_range(float p_from, float p_to) {
        return randbase.randomFloat(p_from, p_to);
    }
    __device__
	double randfn(float p_mean = 0.0, float p_deviation = 1.0) { return randbase.randfn(p_mean, p_deviation); }
    __device__
	int randi_range(int p_from, int p_to) { return randbase.randomInteger(p_from, p_to); }
    __device__
    void shuffle(int *arr, int n) {
        if (n <= 1) return;

        for (int i = n - 1; i > 0; i--) {
            int j = randbase.randbound(i + 1);
            
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
};

// end of random number generator

// seed function

__device__
loadout get_results(uint64_t seed) {
    RandomNumberGenerator rng;
    RandomNumberGenerator globalRng;


//                                 speed fireRate multiShot wallPunch splashDamage piercing freezing infection
    int itemCategories[8] = {      0,    1,       2,        3,        4,           5,       6,       7        };
    float itemCosts[8] = {         1.0f, 2.8f,    3.3f,     1.25f,    2.0f,        2.4f,    1.5f,    2.15f    };
    //                 basic mage laser melee pointer swarm
    int charList[6] = {0,    1,   2,    3,    4,      5};

    int itemCounts[8];

    rng.set_seed(seed);
    double intensity = rng.randf_range(0.20f, 1.0f);

    int character = charList[rng.randi() % 6];
    int abilityChar = charList[rng.randi() % 6];
    double abilityLevel = 1.0 + round(run(rng.randf(), 1.5/(1.0+intensity),1.0,0.0)*6);

    double itemCount = 8.0;


    double points = 0.66 * itemCount * rng.randf_range(0.5, 1.5) * (1.0 + 4.0*pow(intensity, 1.5));

    double itemDistSteepness = rng.randf_range(-0.5, 2.0);
    
    double itemDistArea = 1.0 / (1.0 + pow(2.0, 0.98*itemDistSteepness));

    globalRng.set_seed(rng.get_seed());
    globalRng.shuffle(itemCategories, 8);
    
    if (rng.randf() < intensity) {
        int multishotIdx = -1;
        for (int i = 0; i < itemCount; ++i) {
            if (itemCategories[i] == 2) {
                multishotIdx = i;
                break;
            }
        }

        if (multishotIdx != -1) {
            // Remove the multishot element
            for (int i = multishotIdx; i < itemCount - 1; ++i) {
                itemCategories[i] = itemCategories[i + 1];
            }
        }

        // Insert multiShot at a new index
        int insertIdx = itemCount - 1 - rng.randi_range(0, 2);
        for (int i = itemCount; i > insertIdx; --i) {
            itemCategories[i] = itemCategories[i - 1];
        }
        itemCategories[insertIdx] = 2;
    }

    if (rng.randf() < intensity) {
        int fireRateIdx = -1;
        for (int i = 0; i < itemCount; ++i) {
            if (itemCategories[i] == 1) {
                fireRateIdx = i;
                break;
            }
        }

        if (fireRateIdx != -1) {
            // Remove the firerate element
            for (int i = fireRateIdx; i < itemCount - 1; ++i) {
                itemCategories[i] = itemCategories[i + 1];
            }
        }

        // Insert firerate at a new index
        int insertIdx = itemCount - 1 - rng.randi_range(0, 2);
        for (int i = itemCount; i > insertIdx; --i) {
            itemCategories[i] = itemCategories[i - 1];
        }
        itemCategories[insertIdx] = 1;
    }

    double catMax = 7.0;
    // int total = 0; // why does this exist?
    for (int i = 0; i < 8; i++) {
        int item = itemCategories[i];
        double catT = (double) i / catMax;
        double cost = itemCosts[item];
        cost = 1.0 + ((cost - 1.0) / 2.5);
        double baseAmount = 0.0;

        double special = 0.0;
        if (i == 7) {
            special += 4.0 * rng.randf_range(0.0, pow(intensity, 2.0));
        }
        double amount = fmax(0.0, 3.0 * run(catT, itemDistSteepness, 1.0, 0.0) + 3.0 * clamp(rng.randfn(0.0, 0.15), -0.5, 0.5));
        
        itemCounts[item] = (int) clamp(round(baseAmount+amount*((points/cost)/(1.0+5.0*itemDistArea))+special), 0.0, 26.0);
    }

    intensity = -0.05 + intensity*lerp(0.33, 1.2, smoothCorner(((double) itemCounts[2]*1.8+(double) itemCounts[1])/12.0, 1.0, 1.0, 4.0)); // TODO: smoothCorner()

    double finalT = rng.randfn((float) pow(intensity, 1.2), 0.05);
    double startTime = clamp(lerp(60.0*2.0, 60.0*20.0, finalT), 60.0*2.0, 60.0*25.0);

    rng.randf();
    rng.randf();
    int colorState = rng.randi_range(0, 2);
    return loadout{character, abilityChar, abilityLevel, {itemCounts[0], itemCounts[1], itemCounts[2], itemCounts[3], itemCounts[4], itemCounts[5], itemCounts[6], itemCounts[7]}, startTime, colorState};
}
__device__ uint32_t djb2Hash(const char *str) {
    unsigned long hash = 5381;
    int c;
    while (c = *str++) {
        hash = ((hash << 5) + hash) + c; /* hash * 33 + c */
    }

    return hash;
}

__device__ bool shouldStop = false;

__device__
uint32_t hash = 0;

__global__
void bruteForce(double timeMin, double timeMax, int colourState, int fireRateMin, int fireRateMax, int freezingMin, int freezingMax, int infectionMin, int infectionMax, int multiShotMin, int multiShotMax, int piercingMin, int piercingMax, int speedMin, int speedMax, int splashDamageMin, int splashDamageMax, int wallPunchMin, int wallPunchMax, int abilityLevelMin, int abilityLevelMax, int character, int ability) {
    
    int totalThreads = blockDim.x * gridDim.x;
    int idx = blockIdx.x * blockDim.x + threadIdx.x;
    int64_t i = idx;
    loadout loadout = get_results(0);
    for (;i < 4294967296;i = i + totalThreads) {
        loadout = get_results(i);
		if (
            (character == -1 || loadout.character == character) &&
            (ability == -1 || loadout.abilityCharacter == ability) &&
            (colourState == -1 || loadout.colorState == colourState) &&
            loadout.abilityLevel >= abilityLevelMin &&
            loadout.abilityLevel <= abilityLevelMax &&
            loadout.itemCounts[0] >= speedMin &&
            loadout.itemCounts[0] <= speedMax &&
            loadout.itemCounts[1] >= fireRateMin &&
            loadout.itemCounts[1] <= fireRateMax &&
            loadout.itemCounts[2] >= multiShotMin &&
            loadout.itemCounts[2] <= multiShotMax &&
            loadout.itemCounts[3] >= wallPunchMin &&
            loadout.itemCounts[3] <= wallPunchMax &&
            loadout.itemCounts[4] >= splashDamageMin &&
            loadout.itemCounts[4] <= splashDamageMax  &&
            loadout.itemCounts[5] >= piercingMin &&
            loadout.itemCounts[5] <= piercingMax &&
            loadout.itemCounts[6] >= freezingMin &&
            loadout.itemCounts[6] <= freezingMax &&
            loadout.itemCounts[7] >= infectionMin &&
            loadout.itemCounts[7] <= infectionMax &&
            loadout.startTime >= timeMin &&
            loadout.startTime <= timeMax) {
            shouldStop = true;
            hash = i;
        }
        if (shouldStop) {
            return;
        }
    }
}

extern "C" {

__declspec(dllexport) unsigned int startBruteForce(double timeMin, double timeMax, int colourState, int fireRateMin, int fireRateMax, int freezingMin, int freezingMax, int infectionMin, int infectionMax, int multiShotMin, int multiShotMax, int piercingMin, int piercingMax, int speedMin, int speedMax, int splashDamageMin, int splashDamageMax, int wallPunchMin, int wallPunchMax, int abilityLevelMin, int abilityLevelMax, int character, int ability) {
    bruteForce<<<2048,256>>>( timeMin,  timeMax,  colourState,  fireRateMin,  fireRateMax,  freezingMin,  freezingMax,  infectionMin,  infectionMax,  multiShotMin,  multiShotMax,  piercingMin,  piercingMax,  speedMin,  speedMax,  splashDamageMin,  splashDamageMax,  wallPunchMin,  wallPunchMax,  abilityLevelMin,  abilityLevelMax,  character,  ability);

    uint32_t h_hash;

    // Copy winning hash to host memory
    cudaMemcpyFromSymbol(&h_hash, hash, sizeof(uint32_t), 0, cudaMemcpyDeviceToHost);

    return {h_hash};
}
}