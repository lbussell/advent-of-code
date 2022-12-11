namespace AdventOfCode;

public class Day08Solution : Solution
{
    public Day08Solution() : base(2022, 8, "Treetop Tree House") { }

    public override string SolvePartOne()
    {
        int[][] ltr = parseInput(input);
        int[][] ttb = topToBottom(ltr);

        bool treeIsVisible(int r, int c)
        {
            if (r == 0 || r == ltr.Length || c == 0 || c == ltr[0].Length)
                return true;
            else if (ltr[r][..c].Max() < ltr[r][c]) // visible from left
                return true;
            else if (ltr[r][(c+1)..].Max() < ltr[r][c]) // visible from right
                return true;
            else if (ttb[c][..r].Max() < ttb[c][r]) // visible from top
                return true;
            else if (ttb[c][(r+1)..].Max() < ttb[c][r]) // visible from bottom
                return true;
            else
                return false;
        }

        // var visible = ltr.Select((row, r) => row.Select((_, c) => treeIsVisible(r, c)))
        //     .Select(row => row.Where(b => b).Count()).Sum();
        int visible = (ltr.Length * 2) + (ltr[0].Length * 2) - 4;
        for (int r = 1; r < ltr.Length - 1; r++)
            for (int c = 1; c < ltr[0].Length - 1; c++)
                if (treeIsVisible(r, c))
                    visible += 1;

        return visible.ToString();
    }

    public override string SolvePartTwo()
    {
        int[][] ltr = parseInput(input);
        int[][] ttb = topToBottom(ltr);

        int GetScenicScore(int row, int col)
        {
            int h = ltr[row][col];

            // left
            int left = 0;
            int c = col - 1;
            while (c >= 0)
            {
                left += 1;
                if (ltr[row][c] >= h)
                    break;
                c -= 1;
            }

            // right
            int right = 0;
            c = col + 1;
            while (c < ltr[0].Length)
            {
                right += 1;
                if (ltr[row][c] >= h)
                    break;
                c += 1;
            }

            // up
            int up = 0;
            int r = row - 1;
            while (r >= 0)
            {
                up += 1;
                if (ttb[col][r] >= h)
                    break;
                r -= 1;
            }

            // down
            int down = 0;
            r = row + 1;
            while (r < ttb[0].Length)
            {
                down += 1;
                if (ttb[col][r] >= h)
                    break;
                r += 1;
            }

            return left * right * up * down;
        }

        var scores = ltr.Select((row, r) => row.Select((_, c) => GetScenicScore(r, c)));
        var max = scores.Select(row => row.Max()).Max();

        // int scores = (ltr.Length * 2) + (ltr[0].Length * 2) - 4;
        // for (int r = 1; r < ltr.Length - 1; r++)
        //     for (int c = 1; c < ltr[0].Length - 1; c++)
        //         if (treeIsVisible(r, c))
        //             _scores += 1;

        return max.ToString();
    }

    private static int[][] parseInput(string input)
    {
        return input.Split(Environment.NewLine).Select(line => line.ToCharArray().Select(c => c - '0').ToArray()).ToArray();
    }

    private static int[][] topToBottom(int[][] trees)
    {
        var ret = new int[trees[0].Length][];
        for (int col = 0; col < trees[0].Length; col++)
        {
            ret[col] = new int[trees.Length];
            for (int row = 0; row < trees.Length; row++)
            {
                ret[col][row] = trees[row][col];
            }
        }
        return ret;
    }

    private static int[][] mirror(int[][] trees)
    {
        return trees.Select(t => t.Reverse().ToArray()).ToArray();
    }

    // private static bool 

    // private static int countVisibleTrees(int[][] trees)
    // {
    //     int visible = 0;
    //     for (int row = 0; row < trees.Length; row++)
    //     {
    //         for (int col = 0; col < trees[0].Length; col++)
    //         {

    //         }
    //     }
    // }

    // private static bool treeIsVisible(int r, int c, int[][] trees)
    // {

    // }
}