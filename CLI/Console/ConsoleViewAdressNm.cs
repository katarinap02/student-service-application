using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewAdressNm
{
    public static String SafeInputAdressNm() //klasa sluzi kako bi se ispravno uneo datum
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;
        

        while (!int.TryParse(rawInput.Substring(0, rawInput.Length - 1), out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return rawInput;
    }
}
