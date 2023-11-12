using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewEnum
{
    public static Subject.Semester SafeInputEnum1()
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
    public static Student.Status SafeInputEnum2()
    {
        Student.Status x = Student.Status.S;
        string rawInput = System.Console.ReadLine() ?? string.Empty;

        if (rawInput == "S")
        {

        }
        else if (rawInput == "B")
        {
            x = Student.Status.B;
        }
        else
        {
            System.Console.WriteLine("Not a valid string, enter B  or S: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return x;
    }
}
