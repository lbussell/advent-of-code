namespace AdventOfCode;

public class Day07Solution : Solution
{

    public Day07Solution() : base(2022, 7, "No Space Left On Device") { }

    public override string SolvePartOne()
    {
        Day7File tree = ParseDirectoryTree(input.Split(Environment.NewLine));
        return tree.LimitedSize.ToString();
    }

    private static Day7File ParseDirectoryTree(string[] input)
    {
        Day7File root = new Day7File("/");
        Day7File current = root;
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
                    current = current.GetChild(splitLine[2]);
                    break;
                case ["dir", _]:
                    current.AddToDirectory(new Day7File(splitLine[1], current));
                    break;
                default:
                    current.AddToDirectory(new Day7File(splitLine[1], current, int.Parse(splitLine[0])));
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

public class Day7File
{
    private int _size { get; }
    public int Size
        { get => IsDirectory ? DirectoryContent.Select(f => f.Size).Sum() : _size; }
    public int LimitedSize
    {
        get 
        {
            if (IsDirectory)
            {
                return DirectoryContent.Select(f => f.Size < 100000 ? f.Size : 0).Sum();
            }
            else
            {
                return Size < 100000 ? Size : 0;
            }
        }
    }
    public string Name { get; }
    public Day7File? Parent { get; }
    public List<Day7File> DirectoryContent { get; }
    public bool IsDirectory { get => DirectoryContent.Count() > 0; }

    public Day7File(string name, Day7File? parent = null, int size = 0) {
        Parent = parent;
        Name = name;
        _size = size;
        DirectoryContent = new List<Day7File>();
    }

    public Day7File GetChild(string name)
    {
        return DirectoryContent.Where(f => f.Name == name).First();
    }

    public void AddToDirectory(Day7File file) =>
        DirectoryContent.Add(file);
}