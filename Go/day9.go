package main;

import (
    "bufio"
    "log"
	"os"
	"strconv"
)

func main(){
	file, err := os.Open("input9.txt")
    if err != nil {
        log.Fatal(err)
    }
    defer file.Close()

	numbers := []int {};
	scanner := bufio.NewScanner(file)
    for scanner.Scan() {
		line := scanner.Text();
		commandNumber, _ := strconv.Atoi(line)
		numbers = append(numbers, commandNumber);
	}

	target := 29221323;

	for i, start := range(numbers){
		sum := start;
		for y, val := range(numbers[i+1:]){
			if sum == target{
				subset := numbers[i:i+y+2];
				min, max := MinMax(subset);
				print(min + max);

				break;
			}
			if sum > target{
				break;
			}

			sum += val;
		}
	}

	
	// i := 25;
	// for i < len(numbers){
	// 	current := numbers[i];
		
	// 	if !containsTwoSum(numbers[i-25:i], current){
	// 		break;
	// 	}

	// 	i++;
	// }

	if err := scanner.Err(); err != nil {
        log.Fatal(err)
    }
}
func MinMax(array []int) (int, int) {
    var max int = array[0]
    var min int = array[0]
    for _, value := range array {
        if max < value {
            max = value
        }
        if min > value {
            min = value
        }
    }
    return min, max
}
func containsTwoSum(numbers []int, search int) bool{

	for _, value := range(numbers){
		l := search - value;
		if contains(numbers, l){
			return true;
		}
	}
	return false;
}

func contains(s []int, e int) bool {
    for _, a := range s {
        if a == e {
            return true
        }
    }
    return false
}