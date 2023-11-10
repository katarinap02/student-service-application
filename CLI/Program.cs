
using CLI.Console;
using CLI.DAO;

namespace CLIExample;

class Program
{
    static void Main()
    {
        StudentDao students = new StudentDao();
        ProfessorDao professors = new ProfessorDao();
        ConsoleView view = new ConsoleView(students, professors);
        view.Run();
    }
}
