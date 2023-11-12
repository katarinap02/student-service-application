
using CLI.Console;
using CLI.DAO;
using CLI.Model;
using System;

namespace CLIExample;

class Program
{
    static void Main()
    {
        StudentDao students = new StudentDao();
        ProfessorDao professors = new ProfessorDao();
        SubjectDao subjects = new SubjectDao();
        ChairDao chairs = new ChairDao();
        GradeDao grades = new GradeDao();
        ConsoleView view = new ConsoleView(students, professors, subjects, chairs, grades );
        view.Run();
    }
}
