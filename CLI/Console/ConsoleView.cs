
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
    private void Show()
    {
        System.Console.WriteLine("\nChoose an option to work with : ");
        System.Console.WriteLine("1: Student ");
        System.Console.WriteLine("2: Professor ");
        System.Console.WriteLine("3: Subject ");
        System.Console.WriteLine("0: Close");
    }

    public void Run()
    {
        while (true)
        {
            Show();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "1" && userInput != "2" && userInput != "3")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuInput(userInput);
        }
    }



    /// STUDENT 
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

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4")
                System.Console.WriteLine("Choose an option again ");
            else
            HandleMenu(userInput);
        }
    }

    

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All ");
        System.Console.WriteLine("2: Add ");
        System.Console.WriteLine("3: Update ");
        System.Console.WriteLine("4: Remove ");
        System.Console.WriteLine("0: Close");
    }

    


}
