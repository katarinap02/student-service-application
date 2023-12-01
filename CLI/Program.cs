﻿
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
        HeadDao heads = new HeadDao(students, professors, subjects, chairs, grades, studentsubject);

        foreach (StudentSubject ss in studentsubject.GetAllStudentSubjects())
        {
            Student s = students.GetStudentById(ss.StudentId);
            Subject p = subjects.GetSubjectById(ss.SubjectId);
            s.Subjects.Add(p);
            p.StudentsF.Add(s);
            p.bind = 1;
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
            c.bind = 1;
            p.chairs.Add(c);
            p.bind = 1;
            
            
        }
        foreach (Subject sb in subjects.GetAllSubjects())
        {
            Professor pp = professors.GetProfessorById(sb.ProfessorSb.Id);
            pp.subjects.Add(sb);
            pp.bind = 1;
        }



        ConsoleView view = new ConsoleView(students, professors, subjects, chairs, grades, heads);

        view.Run();


        







    }
}
