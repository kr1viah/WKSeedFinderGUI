package main

/*
#cgo LDFLAGS: -lmain -L${SRCDIR}/ -lm

extern unsigned int startBruteForce(double, double, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int);
*/
import "C"
import (
	"flag"
	"fmt"
	"log"
	"math"
	"math/rand"
	"sync"
	"time"
)

const allowed = "abcdefghijklmnopqrstuvwxyz"
const Math_TAU = 6.2831853071795864769252867666

type loadout struct {
	char         Char
	abilityChar  Char
	abilityLevel float64
	itemCounts   map[upgrade]int
	startTime    float64
	colorState   int32
	r            float32
	g            float32
	b            float32
}

type upgrade int
type Char int

var itemCats = []upgrade{speed, fireRate, multiShot, wallPunch, splashDamage, piercing, freezing, infection}

var itemCosts = map[upgrade]float64{
	speed:        1.0,
	fireRate:     2.8,
	multiShot:    3.3,
	wallPunch:    1.25,
	splashDamage: 2.0,
	piercing:     2.4,
	freezing:     1.5,
	infection:    2.15,
}

var charList = [6]Char{basic, mage, laser, melee, pointer, swarm}

const (
	basic Char = iota
	mage
	laser
	melee
	pointer
	swarm
)

const (
	speed upgrade = iota
	fireRate
	multiShot
	wallPunch
	splashDamage
	piercing
	freezing
	infection
)

var shouldStop = false

var character int
var ability int
var colourState *int
var abilityLevelMin *int
var abilityLevelMax *int
var speedMin *int
var speedMax *int
var fireRateMin *int
var fireRateMax *int
var multiShotMin *int
var multiShotMax *int
var wallPunchMin *int
var wallPunchMax *int
var splashDamageMin *int
var splashDamageMax *int
var piercingMin *int
var piercingMax *int
var freezingMin *int
var freezingMax *int
var infectionMin *int
var infectionMax *int
var timeMin *float64
var timeMax *float64

func bruteForce(id int) {
	for i := id + 1; i < 4294967296; i = i + threads {
		if shouldStop {
			return
		}
		var loadout = Get_results(uint64(i))
		if (character == -1 || loadout.char == Char(character)) &&
			(ability == -1 || loadout.abilityChar == Char(ability)) &&
			(*colourState == -1 || loadout.colorState == int32(*colourState)) &&
			loadout.abilityLevel >= float64(*abilityLevelMin) &&
			loadout.abilityLevel <= float64(*abilityLevelMax) &&
			loadout.itemCounts[0] >= *speedMin &&
			loadout.itemCounts[0] <= *speedMax &&
			loadout.itemCounts[1] >= *fireRateMin &&
			loadout.itemCounts[1] <= *fireRateMax &&
			loadout.itemCounts[2] >= *multiShotMin &&
			loadout.itemCounts[2] <= *multiShotMax &&
			loadout.itemCounts[3] >= *wallPunchMin &&
			loadout.itemCounts[3] <= *wallPunchMax &&
			loadout.itemCounts[4] >= *splashDamageMin &&
			loadout.itemCounts[4] <= *splashDamageMax &&
			loadout.itemCounts[5] >= *piercingMin &&
			loadout.itemCounts[5] <= *piercingMax &&
			loadout.itemCounts[6] >= *freezingMin &&
			loadout.itemCounts[6] <= *freezingMax &&
			loadout.itemCounts[7] >= *infectionMin &&
			loadout.itemCounts[7] <= *infectionMax &&
			loadout.startTime >= *timeMin &&
			loadout.startTime <= *timeMax {
			fmt.Println("Found seed")
			winningHash = uint(i)
			shouldStop = true
			didFindSeed = true
		}
	}
}

var winningHash uint
var didFindSeed bool = false
var threads int

func main() {
	fmt.Println("Running")

	var threadCount = flag.Int("cpu", 0, "")
	timeMin = flag.Float64("timemin", 120.0, "")
	timeMax = flag.Float64("timemax", 1500.0, "")
	colourState = flag.Int("colourstate", -1, "")
	fireRateMin = flag.Int("fireratemin", 0, "")
	fireRateMax = flag.Int("fireratemax", 26, "")
	freezingMin = flag.Int("freezingmin", 0, "")
	freezingMax = flag.Int("freezingmax", 26, "")
	infectionMin = flag.Int("infectionmin", 0, "")
	infectionMax = flag.Int("infectionmax", 26, "")
	multiShotMin = flag.Int("multishotmin", 0, "")
	multiShotMax = flag.Int("multishotmax", 26, "")
	piercingMin = flag.Int("piercingmin", 0, "")
	piercingMax = flag.Int("piercingmax", 26, "")
	speedMin = flag.Int("speedmin", 0, "")
	speedMax = flag.Int("speedmax", 26, "")
	splashDamageMin = flag.Int("splashdamagemin", 0, "")
	splashDamageMax = flag.Int("splashdamagemax", 26, "")
	wallPunchMin = flag.Int("wallpunchmin", 0, "")
	wallPunchMax = flag.Int("wallpunchmax", 26, "")
	abilityLevelMin = flag.Int("abilitylevelmin", 0, "")
	abilityLevelMax = flag.Int("abilitylevelmax", 7, "")
	var abilityString = flag.String("ability", "", "")
	var characterString = flag.String("character", "", "")
	fmt.Println("Getting parameters...")
	flag.Parse()
	fmt.Println("Converting parameters...")
	ability = -1
	switch *abilityString {
	case "Bellow":
		ability = 0
	case "Time_stop":
		ability = 1
	case "Torrent":
		ability = 2
	case "Endure":
		ability = 3
	case "Detach":
		ability = 4
	case "Propagate":
		ability = 5
	}
	character = -1
	switch *characterString {
	case "Epsilon":
		character = 0
	case "Nyx":
		character = 1
	case "Bastion":
		character = 2
	case "Zephyr":
		character = 3
	case ":)":
		character = 4
	case "Mebo":
		character = 5
	}

	var start = time.Now()
	if *threadCount == 0 {
		var TimeMin = C.double(*timeMin)
		var TimeMax = C.double(*timeMax)
		var ColourState = C.int(*colourState)
		var FireRateMin = C.int(*fireRateMin)
		var FireRateMax = C.int(*fireRateMax)
		var FreezingMin = C.int(*freezingMin)
		var FreezingMax = C.int(*freezingMax)
		var InfectionMin = C.int(*infectionMin)
		var InfectionMax = C.int(*infectionMax)
		var MultiShotMin = C.int(*multiShotMin)
		var MultiShotMax = C.int(*multiShotMax)
		var PiercingMin = C.int(*piercingMin)
		var PiercingMax = C.int(*piercingMax)
		var SpeedMin = C.int(*speedMin)
		var SpeedMax = C.int(*speedMax)
		var SplashDamageMin = C.int(*splashDamageMin)
		var SplashDamageMax = C.int(*splashDamageMax)
		var WallPunchMin = C.int(*wallPunchMin)
		var WallPunchMax = C.int(*wallPunchMax)
		var AbilityLevelMin = C.int(*abilityLevelMin)
		var AbilityLevelMax = C.int(*abilityLevelMax)
		var Ability = C.int(ability)
		var Character = C.int(character)
		fmt.Println("GPU")
		fmt.Println("Starting brute force...")
		winningHash = uint(C.startBruteForce(TimeMin, TimeMax, ColourState, FireRateMin, FireRateMax, FreezingMin, FreezingMax, InfectionMin, InfectionMax, MultiShotMin, MultiShotMax, PiercingMin, PiercingMax, SpeedMin, SpeedMax, SplashDamageMin, SplashDamageMax, WallPunchMin, WallPunchMax, AbilityLevelMin, AbilityLevelMax, Character, Ability))
	}
	if *threadCount > 0 { // do the boss thing
		fmt.Println("CPU")
		fmt.Println("Starting brute force...")
		var wg = sync.WaitGroup{}
		wg.Add(*threadCount)
		for t := 0; t < *threadCount; t++ {
			go func() {
				defer wg.Done()
				bruteForce(t)
			}()
		}
		wg.Wait()
	}

	// didFindSeed := int(result.didFindSeed)
	if winningHash != 0 {
		fmt.Println("Average time per seed:", time.Since(start)/time.Duration(winningHash))
		fmt.Println("Runtime:", time.Since(start))
		fmt.Println("Corresponding hash:", winningHash)
		Print_results(uint64(winningHash))
		fmt.Println("Boss order:", Get_bosses(uint64(winningHash)))
	} else {
		fmt.Println("Seed doesn't exist!")
		fmt.Println("Runtime:", time.Since(start))
	}
}

func djb2(s string) uint32 {
	var h uint32 = 5381
	for i := 0; i < len(s); i++ {
		h = h*33 + uint32(s[i])
	}
	return h
}

func clampInt64(x, min, max int64) int64 {
	if x < min {
		return min
	} else if x > max {
		return max
	}
	return x
}

func powInt64(base, exp int64) int64 {
	result := int64(1)
	for i := int64(0); i < exp; i++ {
		result *= base
	}
	return result
}

func greedySearch(target uint32) string {
	rand.Seed(2)
	var foundOne = false
	var T []byte
	for i := 0; i < 1000; /* will always finish in a maximum of 1000 tries, given rand.Seed(2) */ i++ {
		fixedPrefix := []byte("abcd")
		fixedPrefix[0] = allowed[rand.Intn(len(allowed))]
		fixedPrefix[1] = allowed[rand.Intn(len(allowed))]
		fixedPrefix[2] = allowed[rand.Intn(len(allowed))]
		fixedPrefix[3] = allowed[rand.Intn(len(allowed))]

		T = []byte(string(fixedPrefix) + "iiiiiiii")
		H0 := djb2(string(T))

		// Compute delta = (target - H0) mod 2^32, represented as a signed 64-bit integer in [–2^31, 2^31).
		var delta int64
		diff := int64(target) - int64(H0)
		mod := int64(1) << 32
		if diff < -int64(1<<31) {
			diff += mod
		} else if diff >= int64(1<<31) {
			diff -= mod
		}
		delta = diff

		// Precompute weights: weight[i] = 33^(L-1-i).
		weights := make([]int64, 8) // Only last 8 positions are adjusted
		for i := 0; i < 8; i++ {
			weights[i] = powInt64(33, int64(7-i))
		}

		var baseIndex int64 = 8

		// Range of adjustments allowed
		minAdj := -baseIndex
		maxAdj := int64(len(allowed)-1) - baseIndex

		// Compute adjustments
		remaining := delta
		for i := 0; i < 8; i++ {
			w := weights[i]
			desired := int64(math.Round(float64(remaining) / float64(w)))
			adj := clampInt64(desired, minAdj, maxAdj)

			// Apply adjustment to character
			newIndex := baseIndex + adj
			if newIndex < 0 || newIndex >= int64(len(allowed)) {
				log.Fatalf("Digit out of range at position %d: newIndex = %d", i, newIndex)
			}
			T[4+i] = allowed[newIndex]

			remaining -= adj * w
		}
		if remaining != 0 { // If djb2(T) = target this should return false
			continue
		}
		foundOne = true
		break
	}
	if !foundOne { // should never happen, but just in case
		log.Fatalf("Something went horribly wrong. Target: %d", target)
	}

	candidate := string(T)
	if djb2(candidate) != target { // should also never happen, but just in case
		log.Fatalf("Something went wrong. Target: %d", target)
	}
	return candidate
}

func Print_results(seed uint64) {
	var stringSeed string = greedySearch(uint32(seed))

	fmt.Println("Seed:", stringSeed)

	loadout := Get_results(seed)
	switch loadout.char {
	case basic:
		fmt.Println("Character: epsilon")
	case mage:
		fmt.Println("Character: nyx")
	case laser:
		fmt.Println("Character: bastion")
	case melee:
		fmt.Println("Character: zephyr")
	case pointer:
		fmt.Println("Character: :)")
	case swarm:
		fmt.Println("Character: mebo")
	default:
		fmt.Println("Character:", loadout.char)
	}
	switch loadout.abilityChar {
	case basic:
		fmt.Println("Ability: bellow")
	case mage:
		fmt.Println("Ability: halt")
	case laser:
		fmt.Println("Ability: torrent")
	case melee:
		fmt.Println("Ability: endure")
	case pointer:
		fmt.Println("Ability: detach")
	case swarm:
		fmt.Println("Ability: propagate")
	}
	fmt.Println("Ability level:", loadout.abilityLevel)
	for i := 0; i <= 8; i++ {
		switch i {
		case 0:
			fmt.Println("Level of", speedReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 1:
			fmt.Println("Level of", fireRateReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 2:
			fmt.Println("Level of", multiShotReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 3:
			fmt.Println("Level of", wallPunchReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 4:
			fmt.Println("Level of", splashDamageReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 5:
			fmt.Println("Level of", piercingReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 6:
			fmt.Println("Level of", freezingReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		case 7:
			fmt.Println("Level of", infectionReplacements[int(loadout.char)]+":", loadout.itemCounts[upgrade(i)])
		}
	}
	fmt.Println("Starting time:", time.Second*time.Duration(loadout.startTime))
	switch loadout.colorState {
	case 0:
		fmt.Println("Color mode: outline")
	case 1:
		fmt.Println("Color mode: outline, white filling")
	case 2:
		fmt.Println("Color mode: filling, white outline")
	}
}

var speedReplacements = []string{"speed", "speed", "speed", "speed", "speed", "speed"}
var fireRateReplacements = []string{"fire rate", "fire rate", "fire rate", "fire rate", "fire rate", "tick rate"}
var multiShotReplacements = []string{"multishot", "multishot", "laser size", "multishot", "multishot", "+1 body"}
var wallPunchReplacements = []string{"wall punch", "wall punch", "wall punch", "wall punch", "wall punch", "wall punch"}
var splashDamageReplacements = []string{"splash damage", "splash damage", "splash damage", "splash damage", "splash damage", "splash damage"}
var piercingReplacements = []string{"piercing", "crowd control", "piercing", "range", "chase speed", "recover speed"}
var freezingReplacements = []string{"freezing", "freezing", "freezing", "freezing", "freezing", "freezing"}
var infectionReplacements = []string{"infection", "infection", "infection", "infection", "infection", "infection"}

func Get_bosses(seed uint64) []string {
	var rng RandomNumberGenerator
	rng.Initialise()
	rng.Set_seed(seed)
	var bossQueue = []string{}
	var germBuildup = rng.Globalrandf_range(-1.0, 2.0)
	var germsSpawned = 0
	for i := 0; i < 10; i++ {
		var addQueue = []string{
			"Spiker;", "Wyrm;", "Slimest;",
			"Spiker;", "Wyrm;", "Slimest;", "Smiley;",
		}
		var test = rng.Globalrandf()
		if test < 0.33 {
			addQueue = append(addQueue, "Orb Array;")
		}
		germBuildup += 0.5 * float64(len(addQueue))
		if germBuildup > 5.0*(1.0+1.0*float64(germsSpawned)) {
			germBuildup = rng.Globalrandf_range(-2.0, 2.0)
			germsSpawned++
			addQueue = append(addQueue, "Miasma;")
		}
		rng.shuffleString(addQueue)
		bossQueue = append(bossQueue, addQueue...)
	}
	return bossQueue
}

func Get_results(seed uint64) loadout {
	var rng RandomNumberGenerator
	var globalRng RandomNumberGenerator
	var itemCategories = make([]upgrade, len(itemCats))
	var itemCounts = make(map[upgrade]int)
	copy(itemCategories, itemCats)
	rng.Initialise()
	globalRng.Initialise()

	rng.Set_seed(seed)

	// intensity determines basis for other rolls
	var intensity = rng.Randf_range(0.20, 1.0)

	var char = charList[int(rng.Randi())%6]
	var abilityChar = charList[int(rng.Randi())%6]
	var abilityLevel = 1.0 + math.Round(run(rng.Randf(), 1.5/(1.0+intensity), 1.0, 0.0)*6)

	var itemCount = float64(len(itemCategories))
	// points determine item layout
	var points = 0.66 * itemCount * rng.Randf_range(0.5, 1.5) * (1.0 + 4.0*math.Pow(intensity, 1.5))

	var itemDistSteepness = rng.Randf_range(-0.5, 2.0)
	var itemDistArea = 1.0 / (1.0 + math.Pow(2, 0.98*itemDistSteepness))

	globalRng.Set_seed(rng.Get_seed())
	globalRng.shuffle(itemCategories)

	// chance to move offensive upgrades closer to end if not already

	if rng.Randf() < intensity {
		multishotIdx := -1
		for i, category := range itemCategories {
			if category == multiShot {
				multishotIdx = i
				break
			}
		}

		if multishotIdx != -1 {
			itemCategories = append(itemCategories[:multishotIdx], itemCategories[multishotIdx+1:]...)
		}
		insertIdx := int32(itemCount) - 1 - rng.Randi_range(0, 2)
		itemCategories = append(itemCategories[:insertIdx], append([]upgrade{multiShot}, itemCategories[insertIdx:]...)...)
	}

	if rng.Randf() < intensity {
		fireRateIdx := -1
		for i, category := range itemCategories {
			if category == fireRate {
				fireRateIdx = i
				break
			}
		}

		if fireRateIdx != -1 {
			itemCategories = append(itemCategories[:fireRateIdx], itemCategories[fireRateIdx+1:]...)
		}
		insertIdx := int32(itemCount) - 1 - rng.Randi_range(0, 2)
		itemCategories = append(itemCategories[:insertIdx], append([]upgrade{fireRate}, itemCategories[insertIdx:]...)...)
	}

	itemCounts = make(map[upgrade]int)
	var catMax = 7.0
	var total = 0
	for i := 0; i < 8; i++ {
		var item = itemCategories[i]
		var catT = float64(i) / catMax
		var cost = itemCosts[item]
		cost = 1.0 + ((cost - 1.0) / 2.5)
		baseAmount := 0.0

		var special = 0.0
		if i == 7 {
			special += 4.0 * rng.Randf_range(0.0, float32(math.Pow(intensity, 2.0)))
		}
		amount := math.Max(0.0, 3.0*run(catT, itemDistSteepness, 1.0, 0.0)+3.0*clamp(rng.Randfn(0.0, 0.15), -0.5, 0.5))
		itemCounts[item] = int(clamp(math.Round(baseAmount+amount*((points/cost)/(1.0+5.0*itemDistArea))+special), 0.0, 26.0))
		total += itemCounts[item]
	}

	// balance for offensive upgrades
	intensity = -0.05 + intensity*lerp(0.33, 1.2, smoothCorner((float64(itemCounts[multiShot])*1.8+float64(itemCounts[fireRate]))/12.0, 1.0, 1.0, 4.0))

	var finalT = rng.Randfn(float32(math.Pow(intensity, 1.2)), 0.05)
	var startTime = clamp(lerp(60.0*2.0, 60.0*20.0, finalT), 60.0*2.0, 60.0*25.0)

	// var rInt, gInt, bInt, _ = colorconv.HSVToRGB(rng.Randf(), rng.Randf(), float64(1.0)) // im fairly certain this is not accurate in the slightest
	// var r, g, b = float32(rInt) / 255, float32(gInt) / 255, float32(bInt) / 255

	rng.Randf() // to advance state 2 times to accommodate for skipped colour calculation
	rng.Randf()
	var colorState = rng.Randi_range(0, 2)
	return (loadout{char, abilityChar, abilityLevel, itemCounts, startTime, colorState, 0, 0, 0})
}

// var colorState int32
// var r, g, b float32

// windowkill/godot/C helper functions

func pinch(v float64) float64 { // function run() uses
	if v < 0.5 {
		return -v * v
	}
	return v * v
}

func run(x, a, b, c float64) float64 { // TorCurve.run() in windowkill
	c = pinch(c)
	x = math.Max(0, math.Min(1, x))

	const eps = 0.00001
	s := math.Exp(a)
	s2 := 1.0 / (s + eps)
	t := math.Max(0, math.Min(1, b))
	u := c

	var res, c1, c2, c3 float64

	if x < t {
		c1 = (t * x) / (x + s*(t-x) + eps)
		c2 = t - math.Pow(1/(t+eps), s2-1)*math.Pow(math.Abs(x-t), s2)
		c3 = math.Pow(1/(t+eps), s-1) * math.Pow(x, s)
	} else {
		c1 = (1-t)*(x-1)/(1-x-s*(t-x)+eps) + 1
		c2 = math.Pow(1/((1-t)+eps), s2-1)*math.Pow(math.Abs(x-t), s2) + t
		c3 = 1 - math.Pow(1/((1-t)+eps), s-1)*math.Pow(1-x, s)
	}

	if u <= 0 {
		res = (-u)*c2 + (1+u)*c1
	} else {
		res = (u)*c3 + (1-u)*c1
	}

	return res
}

func smoothCorner(x, m, l, s float64) float64 { // TorCurve.smoothCorner in windowkill
	s1 := math.Pow(s/10.0, 2.0)
	return 0.5 * ((l*x + m*(1.0+s1)) - math.Sqrt(math.Pow(math.Abs(l*x-m*(1.0-s1)), 2.0)+4.0*m*m*s1))
}

func lerp(a, b, t float64) float64 {
	return a + t*(b-a)
}

func clamp(m_a, m_min, m_max float64) float64 {
	if m_a < m_min {
		return m_min
	} else if m_a > m_max {
		return m_max
	}
	return m_a
}

// other helper functions

func (rng *RandomNumberGenerator) shuffleString(arr []string) {
	n := len(arr)
	if n <= 1 {
		return
	}
	for i := n - 1; i > 0; i-- {
		j := rng.randbound(uint32(i + 1))
		arr[i], arr[j] = arr[j], arr[i]
	}
}

func (rng *RandomNumberGenerator) shuffle(arr []upgrade) {
	n := len(arr)
	if n <= 1 {
		return
	}
	for i := n - 1; i > 0; i-- {
		j := rng.randbound(uint32(i + 1))
		arr[i], arr[j] = arr[j], arr[i]
	}
}
