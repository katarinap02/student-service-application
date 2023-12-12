using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO;

public class HeadDao
{
    private readonly StudentDao _studentsDao;
    private readonly ProfessorDao _professorsDao;
    private readonly SubjectDao _subjectsDao;
    private readonly ChairDao _chairsDao;
    private readonly GradeDao _gradesDao;
    private readonly StudentSubjectDao _studentsubjectsDao;
    private readonly ChairProfessorDao _chairprofessorDao;
    

    public HeadDao()
    { 
       
    }

    public HeadDao(StudentDao studentsDao, ProfessorDao professorsDao, SubjectDao subjectsDao, ChairDao chairsDao, GradeDao gradesDao, StudentSubjectDao studentsubjectDao, ChairProfessorDao chairprofessorDao) //konstruktor sa parametrima
    {
        _studentsDao = studentsDao;
        _professorsDao = professorsDao;
        _subjectsDao = subjectsDao;
        _chairsDao = chairsDao;
        _gradesDao = gradesDao;
        _studentsubjectsDao = studentsubjectDao;
        _chairprofessorDao = chairprofessorDao;
    }

    public void AddStudentHead(Student st)
    {
        _studentsDao.AddStudent(st);
    }

    public void UpdateStudentHead(Student st)
    {
        Student? olds = _studentsDao.UpdateStudent(st);
        if (olds == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student updated");
        foreach (Subject s in _subjectsDao.GetAllSubjects())
        {
            if (s.StudentsP.Contains(olds))//moze da student bude u polozenim i nepolozenim predmetima
            {
                s.StudentsP.Remove(olds); //izbaci ga iz liste(stare vrednosti)
                s.StudentsP.Add(st); // ubaci ponovo kad je odradjen upgrade

            }

            else if (s.StudentsF.Contains(olds))
            {
                s.StudentsF.Remove(olds);
                s.StudentsF.Add(st);
            }
        }

        foreach (Grade g in _gradesDao.GetAllGrades()) //promenimo u oceni studenta
        {
            if(g.student.Id == olds.Id)
            {
                g.student = st;
                _gradesDao.UpdateGrade(g); //ovo mora da bi odradio i u fajlu
            }
        }
    }


    public void RemoveStudentHead(int id)
    {
        Student? st = _studentsDao.GetStudentById(id);
        if (st is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }
        List<Grade> gr = new List<Grade>(); //sluzi da bismo stavili sve sto treba za brisanje

        foreach (Subject s in _subjectsDao.GetAllSubjects())
        {
            if (s.StudentsP.Contains(st))//moze da student bude u polozenim i nepolozenim predmetima
            {
                s.StudentsP.Remove(st);
                //ovo je za kada obrisemo studenta da se obrise iz liste predmeta
                
            }

            else if (s.StudentsF.Contains(st))
            {
                s.StudentsF.Remove(st);
            }

            StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id); //brisemo i vezu
            if (removedStudentSubject is null)
            {
                continue;
            }
        }

        foreach (Grade g in _gradesDao.GetAllGrades()) //promenimo u oceni studenta
        {
            if (g.student.Id == st.Id)
            {
                gr.Add(g); //pomocna lista iz koje cemo brisati
            }
        }

        foreach(Grade g in gr)
        {
            Grade? removedgrade = _gradesDao.RemoveGrade(g.Id);
            if (removedgrade is null)
            { continue; }
        }

        Student? removedst = _studentsDao.RemoveStudent(id); //tek kad smo obrisali sve veze obrisemo i studenta
        if (removedst is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student removed");

        
    }

    // kada budes brisala za subject ne zaboravi da izbrises iz obe liste kod studenta taj subject
    // kod profesora mora da se obrise i iz liste ali i ako nema sefa cela katedra

    // ------------------------------------------CHAIR--------------------------------------------//
    public void AddChairHead(Chair ch)
    {
        _chairsDao.AddChair(ch);
    }

    public void UpdateChairHead(Chair ch)
    {
        Chair? oldchair = _chairsDao.UpdateChair(ch);
        if (oldchair == null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair updated");

        foreach (Professor s in _professorsDao.GetAllProfessors())
        {
            if (s.chairs.Contains(oldchair))
            {
                s.chairs.Remove(oldchair); 
                s.chairs.Add(ch); 

            }
        }
    }

    public void RemoveChairHead(int id)
    {
        Chair? ch = _chairsDao.GetChairById(id);
        if (ch is null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }
        foreach (Professor s in _professorsDao.GetAllProfessors())
        {
            if (s.chairs.Contains(ch))
            {
                s.chairs.Remove(ch);

            }
        }

        ChairProfessor? removedChairProfessor = _chairprofessorDao.RemoveChairProfessor(id); //brisemo i vezu
        if (removedChairProfessor is null)
        {
            //continue;
        }

        Chair? removedChair = _chairsDao.RemoveChair(id);
        if (removedChair is null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair removed");
    }



    // ----------------------------------------GRADE--------------------------------------------//

    public void AddGradeHead(Grade gd) //ne radi kako treba..., ali ne znam da li ce nam trebati
    {
        Subject sb = gd.subject;
        Student st = gd.student;
        if (sb.StudentsF.Contains(st)) //kada dodamo ocenu za neki predmet student se prebacuje iz u listu studenata polozenih predmeta
        {
            sb.StudentsF.Remove(st);
            sb.StudentsP.Add(st);

        }
        if(st.Subjects.Contains(sb)) // dodala sam pomocnu listu u kojoj cuvamo polozene predmete studenta
        {
            st.Subjects.Remove(sb);
            st.SubjectsP.Add(sb);
        }
        _gradesDao.AddGrade(gd);
    }

    public void UpdateGreadHead(Grade g)
    {
        Grade? oldg = _gradesDao.UpdateGrade(g);
        if (oldg == null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }

        System.Console.WriteLine("Grade updated");

        foreach (Student s in _studentsDao.GetAllStudents()) //update za ocenu u studentu
        {
            if (s.Grades.Contains(oldg))
            {
                s.Grades.Remove(oldg); 
                s.Grades.Add(oldg); 

            }
        }
    }

    public void RemoveGradeHead(int id)
    {
        Grade? gr = _gradesDao.GetGradeById(id);
        if (gr is null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }
        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.Grades.Contains(gr))
            {
                s.Grades.Remove(gr);

            }
        }

            Grade? removedGrade = _gradesDao.RemoveGrade(id);
        if (removedGrade is null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }

        System.Console.WriteLine("Grade removed");
    }

}
