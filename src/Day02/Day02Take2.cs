namespace AdventOfCode;

public class Day02Take2Solution : Solution
{
    public Day02Take2Solution() : base(2022, 2, "Rock Paper Scissors Take Two") { }

    public override string SolvePartOne()
    {
        return input.Split(Environment.NewLine).Select(PlayMatchPartOne).Sum().ToString();
    }

    public static int PlayMatchPartOne(string inputLine) => inputLine switch
        {
            "A X" => 4,
            "B Y" => 5,
            "C Z" => 6,
            "A Z" => 3,
            "B X" => 1,
            "C Y" => 2,
            "A Y" => 8,
            "B Z" => 9,
            "C X" => 7,
            _ => throw new ArgumentException($"{inputLine} is not a valid encrypted rock paper scissors match.")
        };

    public override string SolvePartTwo()
    {
        return input.Split(Environment.NewLine).Select(PlayMatchPartTwo).Sum().ToString();
    }

    public static int PlayMatchPartTwo(string inputLine) => inputLine switch
        {
            "A X" => 3,
            "B Y" => 5,
            "C Z" => 7,
            "A Z" => 8,
            "B X" => 1,
            "C Y" => 6,
            "A Y" => 4,
            "B Z" => 9,
            "C X" => 2,
            _ => throw new ArgumentException($"{inputLine} is not a valid encrypted rock paper scissors match.")
        };
}