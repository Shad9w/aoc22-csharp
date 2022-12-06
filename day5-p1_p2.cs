// Part
int part = 1;

// Read file
string[] lines = File.ReadAllLines("../../../day5.txt");

bool moving = false;

string[][] crates= new string[lines.Length][];

int total_lines = 9;

List<string>[] myList = new List<string>[total_lines];
for (int i = 0; i < total_lines; i++)
{
    myList[i] = new List<string>();
}

for (int i = 0; i < lines.Length; i++)
{
    if(!moving)
    {
        crates[i] = new string[lines[i].Length];
        for(int idx = 0; idx < lines[i].Length; idx++)
        {
            if((idx - 1) % 4 == 0)
            {
                if (lines[i][idx].ToString() != " ")
                {
                    myList[(idx - 1)/4].Add(lines[i][idx].ToString());
                }
            }
        }
    }

    // empty line
    if (lines[i] == "")
    {
        moving = true;
        continue;
    }

    if (moving)
    {
        string[] moves = lines[i].Split(" ");

        // 1 - how many, 3 - from, 5 - to
        int how_many = int.Parse(moves[1]);
        int from = int.Parse(moves[3]) - 1;
        int to = int.Parse(moves[5]) - 1;

        // part 1
        if (part == 1)
        {

            for (int m = 0; m < how_many; m++)
            {
                myList[to].Insert(0, myList[from].First());
                myList[from].RemoveAt(0);
            }
        }

        // part 2
        else
        {
            for (int m = 0; m < how_many; m++)
            {
                if (myList[from].Count > how_many - 1 - m)
                {
                    myList[to].Insert(0, myList[from][how_many - 1 - m]);
                    myList[from].RemoveAt(how_many - 1 - m);
                }
            }
        }

    }
}

for(int i = 0; i < total_lines; i++)
{
    Console.Write(myList[i].First());
}

