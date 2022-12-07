// read the input file
string[] lines = File.ReadAllLines("../../../input.txt");

// 4 for part 1, 14 for part 2
int distinct = 14;

// assummed this would have more lines
for (int i = 0; i < lines.Length; i++)
{
    string last = "";

    // loop through characters
    for(int j = 0; j < lines[i].Length; j++)
    {
        // add character to sting
        last = last + lines[i][j];

        // if string is long enough
        if(last.Length >= distinct)
        {
            // set string to be the size of the number of distinct charaters we need to find
            last = last.Substring(last.Length - distinct, distinct);

            // if all characters are distinct write the number of charcters processed + 1 (index starts at 0)
            if (last.Distinct().Count() == last.Length)
            {
                Console.WriteLine( (j + 1) );
                break;
            }
        }
    }
}
