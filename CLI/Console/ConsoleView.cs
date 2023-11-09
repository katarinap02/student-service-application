
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console;

public class ConsoleView
{
   // System.Console.WriteLine("Enter a number ");
    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
              //  DoStudent();
                break;
            case "2":
              //  DoProfessor();
                break;
            case "3":
              //  DoSUbject();
                break;
        }
    }
    private void HandleMenu(string input)
    {
        switch (input)
        {
            case "1":
               // ShowAll();
                break;
            case "2":
              //  Add();
                break;
            case "3":
              //  Update();
                break;
            case "4":
             //   Remove();
                break;
        }
    }
}
