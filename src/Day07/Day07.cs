namespace AdventOfCode;

public class Day07Solution : Solution
{
    private const int totalSpace = 70000000;
    private const int neededSpace = 30000000;

    public Day07Solution() : base(2022, 7, "No Space Left On Device") { }

    public override string SolvePartOne()
    {
        Directory tree = ParseDirectoryTree(input.Split(Environment.NewLine));

        return GetDirectories(tree)
            .Select(d => d.Size)
            .Where(s => s <= 100000)
            .Sum()
            .ToString();
    }

    private static List<Directory> GetDirectories(Directory tree)
    {
        var dirs = new List<Directory>();

        void r(Directory d)
        {
            d.SubDirectories.ForEach((Directory subd) => 
                {
                    dirs.Add(subd);
                    r(subd);
                });
        }

        r(tree);
        return dirs;
    }

    private static Directory ParseDirectoryTree(string[] input)
    {
        Directory root = new Directory("/");
        Directory current = root;

        foreach (string line in input.Skip(1))
        {
            string[] splitLine = line.Split(' ');
            switch (splitLine)
            {
                case ["$", "ls"]:
                    continue;

                case ["$", "cd", ".."]:
                    if (current.Parent is not null)
                        current = current.Parent;
                    else
                        throw new Exception($"No directory above {current}");
                    break;

                case ["$", "cd", _]:
                    current = current.GetSubDir(splitLine[2]);
                    break;

                case ["dir", _]:
                    current.AddToDirectory(new Directory(splitLine[1], current));
                    break;

                default:
                    current.AddToDirectory(new Day7File(splitLine));
                    break;
            }
        }

        return root;
    }

    public override string SolvePartTwo()
    {
        return "unfinished";
    }
}

public class Directory
{
    public int Size { get => SubDirectories.Select(f => f.Size).Sum() + SubFiles.Select(f => f.Size).Sum(); }
    public string Name { get; }
    public Directory? Parent { get; }
    public List<Directory> SubDirectories { get; }
    public List<Day7File> SubFiles { get; }

    public Directory(string name, Directory? parent = null, int size = 0) {
        Parent = parent;
        Name = name;
        SubDirectories = new List<Directory>();
        SubFiles = new List<Day7File>();
    }

    public Directory GetSubDir(string name)
    {
        return SubDirectories.Where(f => f.Name == name).First();
    }

    public void AddToDirectory(Directory dir) =>
        SubDirectories.Add(dir);
    public void AddToDirectory(Day7File file) =>
        SubFiles.Add(file);
}

public readonly struct Day7File
{
    public readonly string Name;
    public readonly int Size;

    public Day7File(string[] args) // format: ["14848514", "b.txt"]
    {
        Size = int.Parse(args[0]);
        Name = args[1];
    }
}