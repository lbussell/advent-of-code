namespace AdventOfCode;

public class Day02Solution : Solution
{
    private IEnumerable<char[]> EncryptedMatches { get; }

    public Day02Solution() : base(2022, 2, "Rock Paper Scissors")
    {
        EncryptedMatches = ParseInput(input);
    }

    public override string SolvePartOne()
    {
        return EncryptedMatches.Select(em => em.Select(RPSCalculator.DecryptMove).ToArray())
            .Select(moves => RPSCalculator.CalculateMatchScore(moves[0], moves[1]))
            .Sum()
            .ToString();
    }

    public override string SolvePartTwo()
    {
        return EncryptedMatches.Select(em => {
                var opponentMove = RPSCalculator.DecryptMove(em[0]);
                var yourMove = RPSCalculator.CalculateYourMove(opponentMove, RPSCalculator.DecryptResult(em[1]));
                return new[] { opponentMove, yourMove };
            }).ToArray()
            .Select(moves => RPSCalculator.CalculateMatchScore(moves[0], moves[1]))
            .Sum()
            .ToString();
    }

    private protected IEnumerable<char[]> ParseInput(string input)
    {
        foreach (string line in input.Split(Environment.NewLine))
        {
            yield return line.Split(' ').Select(char.Parse).ToArray();
        }
    }
}

// X = lose
// Y = draw
// Z = win

public static class RPSCalculator
{
    public static int CalculateMatchScore(RPSMove opponentMove, RPSMove yourMove)
    {
        int score = 0;
        score += GetChoiceScore(yourMove);
        score += GetResultScore(GetMatchResult(opponentMove, yourMove));
        return score;
    }

    public static RPSResult GetMatchResult(RPSMove opponentMove, RPSMove yourMove)
    {
        if (yourMove == opponentMove) return RPSResult.Draw;
        return yourMove switch
        {
            RPSMove.Rock => (opponentMove == RPSMove.Scissors) ? RPSResult.Win : RPSResult.Lose,
            RPSMove.Paper => (opponentMove == RPSMove.Rock) ? RPSResult.Win : RPSResult.Lose,
            RPSMove.Scissors => (opponentMove == RPSMove.Paper) ? RPSResult.Win : RPSResult.Lose,
            _ => throw new ArgumentException($"{yourMove} is an invalid rock paper scissors choice.")
        };
    }

    public static int GetChoiceScore(RPSMove c) => c switch
    {
        RPSMove.Rock => 1,
        RPSMove.Paper => 2,
        RPSMove.Scissors => 3,
        _ => throw new ArgumentException($"{c} is an invalid rock paper scissors choice.")
    };

    public static int GetResultScore(RPSResult r) => r switch
    {
        RPSResult.Lose => 0,
        RPSResult.Draw => 3,
        RPSResult.Win => 6,
        _ => throw new ArgumentException($"{r} is an invalid rock paper scissors outcome.")
    };

    public static RPSMove DecryptMove(char encryptedMove) => encryptedMove switch
        {
            'A' => RPSMove.Rock,
            'B' => RPSMove.Paper,
            'C' => RPSMove.Scissors,
            'X' => RPSMove.Rock,
            'Y' => RPSMove.Paper,
            'Z' => RPSMove.Scissors,
            _ => throw new ArgumentException($"{encryptedMove} is not a valid encrypted rock paper scissors move.")
        };
    
    public static RPSResult DecryptResult(char encryptedResult) => encryptedResult switch 
        {
            'X' => RPSResult.Lose,
            'Y' => RPSResult.Draw,
            'Z' => RPSResult.Win,
            _ => throw new ArgumentException($"{encryptedResult} is not a valid encrypted rock paper scissors result.")
        };
    
    public static RPSMove CalculateYourMove(RPSMove opponentMove, RPSResult desiredResult)
    {
        if (desiredResult == RPSResult.Draw) return opponentMove;
        return opponentMove switch
        {
            RPSMove.Rock => (desiredResult == RPSResult.Win) ? RPSMove.Paper : RPSMove.Scissors,
            RPSMove.Paper => (desiredResult == RPSResult.Win) ? RPSMove.Scissors : RPSMove.Rock,
            RPSMove.Scissors => (desiredResult == RPSResult.Win) ? RPSMove.Rock : RPSMove.Paper,
            _ => throw new ArgumentException($"{opponentMove} is an invalid rock paper scissors choice.")
        };
    }
}

public enum RPSMove
{
    Rock,
    Paper,
    Scissors
}

public enum RPSResult
{
    Lose,
    Draw,
    Win
}