using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewEnum
{
    public static Subject.Semester SafeInputEnum()
    {
        Subject.Semester x = Subject.Semester.Summer;
        string rawInput = System.Console.ReadLine() ?? string.Empty;

        if (rawInput == "Summer")
        {

        }
        else if (rawInput == "Winter")
        {
             x = Subject.Semester.Winter;
        }
        else
        {
            System.Console.WriteLine("Not a valid string, enter Winter or Summer: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return x;
    }
}
