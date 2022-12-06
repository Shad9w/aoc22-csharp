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
        for(int m = 0; m < int.Parse(moves[1]); m++)
        {
            myList[int.Parse(moves[5]) - 1].Insert(0, myList[int.Parse(moves[3]) - 1].First());
            myList[int.Parse(moves[3]) - 1].RemoveAt(0);
        }


    }
}

// result
for(int i = 0; i < total_lines; i++)
{
    Console.Write(myList[i].First());
}

