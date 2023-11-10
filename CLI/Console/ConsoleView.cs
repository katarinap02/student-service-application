
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO; //dodala 
using CLI.Model; //dodala



namespace CLI.Console;

public class ConsoleView
{

    private readonly StudentDao _studentsDao;
   // private readonly ProfessorDao _professorsDao;
   // private readonly SubjectDao _subjectsDao;

    public ConsoleView(StudentDao studentsDao)
    {
        _studentsDao = studentsDao;
    }




    // System.Console.WriteLine("Enter a number ");
    // 2: skacemo sa 1) 
    private void HandleMenuInput(string input) 
    {
        switch (input)
        {
            case "1":
               RunMenuStudent();
                break;
            case "2":
              //  RunMenuProfessor();
                break;
            case "3":
              //  RunMenuSubject();
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
    // 1) prvo se poziva ova metoda:
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
  /**  private void DoStudent()
    {
        ShowMenu();
        string option = System.Console.ReadLine() ?? "0";
        HandleMenuStudent(option);
    }**/


    ///****** STUDENT *******
    private void HandleMenuStudent(string input) 
    {
        switch (input)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                AddStudents();
                break;
            case "3":
                UpdateStudent();
                break;
            case "4":
                RemoveStudent();
                break;
        }
    }
    // 3: ovje skacemo sa 2) ****STUDENT****
    public void RunMenuStudent()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4")
                System.Console.WriteLine("Choose an option again ");
            else
            HandleMenuStudent(userInput);
        }
    }

    
   

    private void ShowMenu() //bice pozvana u RunMenuStudent
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All ");
        System.Console.WriteLine("2: Add ");
        System.Console.WriteLine("3: Update ");
        System.Console.WriteLine("4: Remove ");
        System.Console.WriteLine("0: Close");
    }

    //**********Metode za studenta***********************************************

    private void ShowAllStudents() //ispisi studente
    {
        PrintStudents(_studentsDao.GetAllStudents());
    }

    private void PrintStudents(List<Student> students) //ispisi studente/studenta
    {
        System.Console.WriteLine("STUDENTS: ");
        string header = $"ID {"",6} | Name {"",21} | Surname {"",21} | Birthdate {"",21} | Adress {"", 21} | Phone number{"",21} | Email{"",30} | Index {"", 21} | Current school year {"", 4} |";
        System.Console.WriteLine(header);
        foreach (Student v in students)
        {
            System.Console.WriteLine(v);
        }
    }

    private void AddStudents() //dodaj studenta
    {
        Student student = InputStudent();
        _studentsDao.AddStudent(student);
        System.Console.WriteLine("Vehicle added");
    }

    private Student InputStudent()
    {
        System.Console.WriteLine("Enter student's name: ");
        string name = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's surname: ");
        string surname = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's birthdate: ");
        string birthdate = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's adress: ");
        int adress = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter student's phone numer: ");
        int phonenumber = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter student' s email: ");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's index: ");
        string index = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter current school year: ");
        int year = ConsoleViewUtils.SafeInputInt();




        return new Student(name, surname, birthdate, adress, phonenumber,email, index, year );
    }

    private void UpdateStudent() //azuriraj studente
    {
        int id = InputId();
        Student student = InputStudent();
        student.Id = id;
        Student? updatedStudent = _studentsDao.UpdateStudent(student);
        if (updatedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student updated");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter student's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveStudent() //ukloni studenta
    {
        int id = InputId();
        Student? removedStudent = _studentsDao.RemoveVehicle(id);
        if (removedStudent is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student removed");
    }
    //***********************************************************************

}
