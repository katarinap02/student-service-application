
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
        StudentSubjectDao studentsubject = new StudentSubjectDao();
        ChairProfessorDao chairprofessor = new ChairProfessorDao();

        foreach (StudentSubject ss in studentsubject.GetAllStudentSubjects())
        {
            Student s = students.GetStudentById(ss.StudentId);
            Subject p = subjects.GetSubjectById(ss.SubjectId);
            s.Subjects.Add(p);
            p.StudentsP.Add(s);
        }

      /*  foreach (ChairProfessor cp in chairprofessor.GetAllChairProfessor())
        {
            Chair c = students.GetStudentById(ss.StudentId);
            Subject p = subjects.GetSubjectById(ss.SubjectId);
            s.Subjects.Add(p);
            p.StudentsP.Add(s);
        }*/



        ConsoleView view = new ConsoleView(students, professors, subjects, chairs, grades );

        view.Run();


        







    }
}
