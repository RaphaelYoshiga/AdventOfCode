package main;

import (
    "bufio"
    "fmt"
    "log"
    "os"
)

func main(){
	file, err := os.Open("input5.txt")
    if err != nil {
        log.Fatal(err)
    }
    defer file.Close()

	lines := []string {}

    scanner := bufio.NewScanner(file)
    for scanner.Scan() {
		lines = append(lines, scanner.Text());        
	}

	fmt.Print(lines)

	seatsMap := make(map[int]bool)

	maxId := 0;
	for _, line := range lines{
		row := getSeatRow(line);
		column := getColumn(line[len(line) - 3:]);
		seatId := row * 8 + column;

		seatsMap[seatId] = true;
		maxId = max(maxId, seatId);
	}	

	i := 1;
	for i <= maxId{
		_, contains := seatsMap[i];

		if contains == false{
			println("My seat: ")
			println(i)
		}
		i++;
	}
	
	println("Tada:")
	println(maxId)

    if err := scanner.Err(); err != nil {
        log.Fatal(err)
    }
}
func getColumn(line string) int{
	min := 0;
	max:= 7;

	i := 0;
	for i <= 2{
		letter := line[i];

		if letter == 'L'{
			max = min + (max - min)/ 2 ;
		}else{
			min = min + 1 + (max - min)/ 2;
		}

		i++;
	}
	if line[2] == 'L'{
		return min;
	}

	return max;
}

func getSeatRow(line string) int{
	min := 0;
	max:= 127;

	i := 0;
	for i <= 6{
		letter := line[i];

		if letter == 'F'{
			max = min + (max - min)/ 2 ;
		}else{
			min = min + 1 + (max - min)/ 2;
		}

		i++;
	}

	if line[6] == 'F'{
		return min;
	}

	return max;
}

func max(a int, b int) int{
	if a > b{
		return a;
	}
	return b;
}
