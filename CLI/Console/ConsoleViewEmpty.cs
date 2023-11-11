using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewEmpty
{
    public static string SafeInputEmpty() //klasa sluzi kako se ne bi uneo prazan string
    {
        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (string.IsNullOrEmpty(rawInput))
        {
            System.Console.WriteLine("You entered an empty string, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return rawInput;
    }
}
