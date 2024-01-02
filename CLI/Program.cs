
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
        HeadDao heads = new HeadDao(students, professors, subjects, chairs, grades, studentsubject, chairprofessor);

      
        foreach (StudentSubject ss in studentsubject.GetAllStudentSubjects())
        {
            Student s= new Student();
            Subject p = new Subject();
             s = students.GetStudentById(ss.StudentId);
             p = subjects.GetSubjectById(ss.SubjectId);
            if(s!=null && p!=null)
                {
                if (!s.SubjectsP.Contains(p)) //ako ne sadrzi druga lista vec predmet ovaj deo ne radi
                {
                    s.Subjects.Add(p);
                }
                if (!p.StudentsP.Contains(s)) // dodala jer ako postoji u listi polozenih predmeta ne treba ga ponovo dodavati
                {
                    p.StudentsF.Add(s);
                }
            }
        }

        foreach (Grade g in grades.GetAllGrades())
        {
            Student s = students.GetStudentById(g.student.Id);
            s.Grades.Add(g);

        }



        foreach (ChairProfessor cp in chairprofessor.GetAllChairProfessor())
        {
            Chair c = chairs.GetChairById(cp.ChairId);
            Professor p = professors.GetProfessorById(cp.ProfessorId);
            c.Professors.Add(p);
            p.chairs.Add(c);
            
            
        }
        foreach (Subject sb in subjects.GetAllSubjects())
        {
            if (sb.ProfessorSb != null)
            {
                Professor pp = professors.GetProfessorById(sb.ProfessorSb.Id);

                pp.subjects.Add(sb);
                pp.bind = 1;
            }
            
        }



        ConsoleView view = new ConsoleView(students, professors, subjects, chairs, grades, heads);

        view.Run();


        







    }
}
