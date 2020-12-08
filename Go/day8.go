package main;

import (
    "bufio"
    "log"
	"os"
	"strings"
	"strconv"
)

type Command struct {
	command string 
	followNumber string
	index int
}

func main(){
	file, err := os.Open("input8.txt")
    if err != nil {
        log.Fatal(err)
    }
    defer file.Close()

	commands := []Command {}


	jumps := []int {};
	scanner := bufio.NewScanner(file)
	index :=0;
    for scanner.Scan() {
		line := scanner.Text();
		fields := strings.Fields(line)

		if fields[0] == "jmp"{
			jumps = append(jumps, index);
		}
		commands = append(commands, Command { command: fields[0], followNumber: fields[1], index: index});        
		index++;
	}

	for _, val := range(jumps){
		i:=0;
		acc := 0;

		visited := make(map[int]bool);
		for true{
			if visited[i] {
				break;
			}

			if i >= len(commands){
				break;
			}

			current := commands[i];
			visited[i] = true;

			if current.command == "acc"{
				commandNumber, _ := strconv.Atoi(current.followNumber)
				acc += commandNumber;
				i++;
			}else if current.command == "nop" || val == i{
				i++;
			}else if current.command == "jmp"{
				commandNumber, _ := strconv.Atoi(current.followNumber)
				i += commandNumber;
			}else{
				print("TOMA")
			}

		}
		print(acc);
	}

	if err := scanner.Err(); err != nil {
        log.Fatal(err)
    }
}

