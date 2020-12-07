package main;

import (
    "bufio"
    "fmt"
    "log"
    "os"
)

func main(){
	file, err := os.Open("input6.txt")
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

	answerBuffer := []string {};
	count := 0;
	for _, line := range lines {
		if line == ""{
			count += getAnswerCount(answerBuffer);
			answerBuffer = [] string{};
		}else{
			answerBuffer = append(answerBuffer, line);
		}
	}
	count += getAnswerCount(answerBuffer);

	print(count);

	if err := scanner.Err(); err != nil {
        log.Fatal(err)
    }
}

func getAnswerCount(answers []string) int{
	answerMap := make(map[rune]int)

	for _, line := range answers{
		for _, character := range line{
			answerMap[character] += 1;
		}
	}

	count := 0;
	for _, value := range answerMap{
		if value == len(answers){
			count++;
		}
	}

	return count;

}