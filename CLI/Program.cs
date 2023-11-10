
using CLI.Console;
using CLI.DAO;
using CLI.Model;

namespace CLIExample;

class Program
{
    static void Main()
    {
        StudentDao students = new StudentDao();
        ProfessorDao professors = new ProfessorDao();
        SubjectDao subjects = new SubjectDao();
        ConsoleView view = new ConsoleView(students, professors, subjects);
        view.Run();
    }
}
