using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewPhoneNm
{
    public static string SafeInputPhone()
    {
        double input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!double.TryParse(rawInput, out input) || rawInput.Length != 10 || rawInput.Substring(0, 2) != "06")
        {
            System.Console.WriteLine("Phonumber must start with 06 and have 10 digits; example 0613330909 ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return rawInput;
    }
}
