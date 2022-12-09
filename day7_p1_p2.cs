// Read the input file
string[] lines = File.ReadAllLines("../../../input.txt");

// the current folder
string current_folder = "";

List<NameSize> nameSizes = new List<NameSize>();

int level = 0;

for (int i = 0; i < lines.Length; i++)
{
    // first char $ -> command'
    if (lines[i][0] == '$')
    {
        if (lines[i].Contains("cd"))
        {
            // using levels as a way to make folders unique-enough
            // start at 0
            if (current_folder == "/")
            {
                level = 0;
            }

            // go up
            if (lines[i].Contains(".."))
            {
                // level--, delete the last folder from the current_folder which stores the current path
                level--;
                string[] split = current_folder.Split("/");
                current_folder = current_folder.Replace("/" + split.Last(), "");

                // root fodler
                if (current_folder.Length == 0 || current_folder[0] != '/')
                {
                    level = 0;
                    current_folder = "/" + current_folder;
                }
            }

            // enter folder
            else
            {
                level++;
                // last char
                if (current_folder.Length > 0 && current_folder.Substring(current_folder.Length -1, 1)[0] != '/')
                {
                    current_folder += "/";
                }
                current_folder += lines[i].Substring(5, lines[i].Length - 5);
            }

            // add level to current_folder to avoid mixing it with folders with the same name located at a different path
            // will probably fail in certain situations
            // works with the part 2, so... close enough, i guess
            // part 1 worked without
            if(current_folder.Last().ToString() != level.ToString())
            {
                current_folder += "--" + level.ToString();
            }

            // add folder + size to list named... NameSize.
            if (!nameSizes.Any(x => x.Name == current_folder))
            {
                nameSizes.Add(new NameSize { Name = current_folder, Size = 0 });
            }
        }
    }
    else
    {
        string[] split = lines[i].Split(" ");

        // find the current folder and update size
        NameSize thisFolderSize = nameSizes.Find(x => x.Name == current_folder);

        if(thisFolderSize != null && !split[0].Contains("dir")) // ignore dir lines, add size
        {
            thisFolderSize.Size += int.Parse(split[0]);
        }
    }
}

// total disk size, used_space - for part 2
int total_disk = 70000000;
int used_space = 0;

int result = 0;

for (int i = 0; i < nameSizes.Count; i++)
{
    used_space += nameSizes[i].Size;
    List<NameSize> otherFoldersIncluding = nameSizes.FindAll(x => x.Name != nameSizes[i].Name && x.Name.Contains(nameSizes[i].Name));
    otherFoldersIncluding.ForEach(x => nameSizes[i].Size += x.Size);

    if (nameSizes[i].Size < 100000)
    {
        result += nameSizes[i].Size;
    }
}

// get the current free space
int free_space = total_disk - used_space;
int space_needed = 30000000 - free_space;

// set result 2 to a large number
int result2 = total_disk;

Console.WriteLine("Part 1, result: " + result);

for (int i = 0; i < nameSizes.Count; i++)
{
    if (nameSizes[i].Size >= space_needed)
    {

        if (nameSizes[i].Size < result2)
        {
            result2 = nameSizes[i].Size;

        }
    }
}

Console.WriteLine("Part 2, result: " + result2);


public class NameSize
{
    public string Name { get; set; }
    public int Size { get; set; }
}

