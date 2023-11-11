using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

static class ConsoleViewDate
{
    public static DateTime SafeInputDate() //klasa sluzi kako bi se ispravno uneo datum
    {
        DateTime input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!DateTime.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid date template, try yyyy-mm-dd (first year then month and day): ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }
}
