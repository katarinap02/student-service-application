﻿namespace CLI.Console;

static class ConsoleViewUtils //klasa sluzi da bi se uspesno preveo iz string u int kad unesemo sa konzole
{
    public static int SafeInputInt()
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }




}