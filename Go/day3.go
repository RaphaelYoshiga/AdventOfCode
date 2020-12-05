package main;

import (
    "bufio"
    "fmt"
    "log"
    "os"
)

func day3(){
	file, err := os.Open("input3.txt")
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

	treeCount := treeFun(lines, 1, 1) * 
				treeFun(lines, 3, 1) * 
				treeFun(lines, 5, 1) * 
				treeFun(lines, 7, 1) * 
				treeFun(lines, 1, 2)
	
	
	println("Tada:")
	println(treeCount)

    if err := scanner.Err(); err != nil {
        log.Fatal(err)
    }
}

func treeFun(lines []string, xIncrement int, yIncrement int) int{
	x := 0;
	y := 0;

	treeCount := 0;
	for y < len(lines){
		
		row:= lines[y];
		checkChar:= x % len(row);

		if row[checkChar] == '#'{
			treeCount++;
		}
		
		x += xIncrement;
		y += yIncrement;
	}
	return treeCount;

}