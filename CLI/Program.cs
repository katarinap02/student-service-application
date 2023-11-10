
using CLI.Console;
using CLI.DAO;

namespace CLIExample;

class Program
{
    static void Main()
    {
        StudentDao students = new StudentDao();
        ConsoleView view = new ConsoleView(students);
        view.Run();
    }
}
