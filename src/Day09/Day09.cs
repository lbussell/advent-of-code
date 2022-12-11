namespace AdventOfCode
{
    using System.Text;
    using AdventOfCode.Day09Util;

    public class Day09Solution : Solution
    {
        public Day09Solution() : base(2022, 9, "Rope Bridge") { }

        public override string SolvePartOne()
        {
            var instructions = input.Split(Environment.NewLine)
                .Select(line => new Instruction(line));

            Head head = new();
            Tail tail = new();

            void PrintState()
            {
                int h = 5;
                int w = 6;
                for (int r = 0; r < h; r++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int c = 0; c < w; c++)
                    {
                        char toWrite = '*';
                        if (head.X == c && head.Y == r)
                        {
                            toWrite = 'H';
                        }
                        if (tail.X == c && tail.Y == r)
                        {
                            toWrite = 'T';
                        }
                        line.Append(toWrite);
                    }
                    Console.WriteLine(line.ToString());
                }
                // Console.WriteLine();
            }


            foreach (Instruction i in instructions)
            {
                for (int j = 0; j < i.Distance; j++)
                {
                    head.Move(i.Direction);
                    tail.MoveTowards(head);
                }
            }

            /*
                  012345
                4 ..**..
                3 ...**.
                2 .***..
                1 ....*.
                0 ****..
            */
            return tail.Visited.Keys.Count().ToString();
        }

        public override string SolvePartTwo()
        {
            var instructions = input.Split(Environment.NewLine)
                .Select(line => new Instruction(line));

            Head head = new();
            Tail[] knots = 
            {
                new(), //1
                new(), //2
                new(), //3
                new(), //4
                new(), //5
                new(), //6
                new(), //7
                new(), //8
                new(), //T
            };

            foreach (Instruction i in instructions)
            {
                // PrintState((-11, 14), (-5, 15), head, knots);
                for (int j = 0; j < i.Distance; j++)
                {
                    head.Move(i.Direction);

                    knots[0].MoveTowards(head);
                    for (int k = 1; k < knots.Length; k++)
                        knots[k].MoveTowards(knots[k - 1]);
                }
            }

            // PrintState((-11, 14), (-5, 15), head, knots);
            return knots.Last().Visited.Keys.Count().ToString();
        }

        private static void PrintState((int startx, int endx) w, (int starty, int endy) h, Head head, Tail[] knots)
        {
            for (int r = h.endy-1; r >= h.starty; r--)
            {
                StringBuilder line = new StringBuilder();
                for (int c = w.startx; c < w.endx; c++)
                {
                    char toWrite = '.';
                    for (int t = knots.Length - 1; t >= 0; t--)
                    {
                        if (knots[t].X == c && knots[t].Y == r)
                        {
                            toWrite = ((char) ((t+1) + '0'));
                        }
                    }
                    if (head.X == c && head.Y == r)
                    {
                        toWrite = 'H';
                    }
                    line.Append(toWrite);
                }
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine();
        }
    }
}

namespace AdventOfCode.Day09Util
{
    public abstract class RopePart
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public RopePart()
        {
            X = 0;
            Y = 0;
        }
    }

    public class Head : RopePart
    {
        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    Y += 1;
                    break;
                case Direction.Down:
                    Y -= 1;
                    break;
                case Direction.Left:
                    X -= 1;
                    break;
                case Direction.Right:
                    X += 1;
                    break;
            }
        }
    }

    public class Tail : RopePart
    {
        public Dictionary<(int x, int y), int> Visited = new();

        public void MoveTowards(RopePart toFollow)
        {
            int xdist = toFollow.X - X;
            int ydist = toFollow.Y - Y;

            if ((-1 <= xdist && xdist <= 1) && (-1 <= ydist && ydist <= 1))
            {
                AddToVisited((X, Y));
                return;
            }

            if (ydist == 2)
            {
                Y += 1;
                if (xdist >= 1)
                    X += 1;
                else if (xdist <= -1)
                    X -= 1;
            }

            else if (ydist == -2)
            {
                Y -= 1;
                if (xdist >= 1)
                    X += 1;
                else if (xdist <= -1)
                    X -= 1;
            }

            else if (xdist == 2)
            {
                X += 1;
                if (ydist == 1)
                    Y += 1;
                else if (ydist == -1)
                    Y -= 1;
            }

            else if (xdist == -2)
            {
                X -= 1;
                if (ydist == 1)
                    Y += 1;
                else if (ydist == -1)
                    Y -= 1;
            }

            else
            {
                throw new ArgumentException($"No case for (head {toFollow.X},{toFollow.Y} tail {X},{Y} xdist {xdist} ydist {ydist})");
            }

            AddToVisited((X, Y));
        }

        private void AddToVisited((int x, int y) location)
        {
            if (Visited.TryGetValue(location, out var existing))
                Visited[location] = existing + 1;
            else
                Visited.Add(location, 1);
        }
    }

    public readonly struct Instruction
    {
        public readonly Direction Direction { get; }
        public readonly int Distance { get; }

        public Instruction(string input)
        {
            string[] splitInput = input.Split(' ');
            Distance = int.Parse(splitInput[1]);
            Direction = splitInput[0] switch
            {
                "U" => Direction.Up,
                "D" => Direction.Down,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException($"{splitInput[0]} is not a valid instruction.")
            };
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
