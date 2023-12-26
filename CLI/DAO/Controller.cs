﻿using CLI.Model;
using CLI.Observer;
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
    
    public ObserverSub observerSub ;


    public HeadDao()
    {
        _studentsDao= new StudentDao();
        _professorsDao= new ProfessorDao();
        _subjectsDao = new SubjectDao() ;
        _chairsDao = new ChairDao();
        _gradesDao = new GradeDao();
        _studentsubjectsDao = new StudentSubjectDao();
        _chairprofessorDao= new ChairProfessorDao();
         observerSub = new ObserverSub();
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
        observerSub = new ObserverSub();
    }

    public void SubscribeStudent(IObserver observer)
    {
        _studentsDao.StudentObserverSub.Subscribe(observer);
    }

    public void AddStudentHead(Student st)
    {
        _studentsDao.AddStudent(st);
       // observerSub.NotifyObservers();
    }

    public List<Student> GetAllStudentsHead()
    {
        return _studentsDao.GetAllStudents();
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
       // observerSub.NotifyObservers();
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

       // observerSub.NotifyObservers();

    }

    public void SubscribeSubject(IObserver observer)
    {
        _subjectsDao.SubjectObserverSub.Subscribe(observer);
    }

    public void AddSubjectHead(Subject sb)
    {
        _subjectsDao.AddSubject(sb);
    }

    public List<Subject> GetAllSubjectsHead()
    {
        return _subjectsDao.GetAllSubjects();
    }

    public void UpdateSubjectHead(Subject sb)
    {
        Subject? olds = _subjectsDao.UpdateSubject(sb);
        if (olds == null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }

        System.Console.WriteLine("Subject updated");
        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.SubjectsP.Contains(olds))//moze da subject bude u polozenim i nepolozenim predmetima kod studenta
            {
                s.SubjectsP.Remove(olds); //izbaci ga iz liste(stare vrednosti)
                s.SubjectsP.Add(sb); // ubaci ponovo kad je odradjen upgrade

            }

            else if (s.Subjects.Contains(olds))
            {
                s.Subjects.Remove(olds);
                s.Subjects.Add(sb);
            }
        }
        foreach (Grade g in _gradesDao.GetAllGrades()) //promijenimo u ocjeni predmet
        {
            if (g.subject.Id == olds.Id)
            {
                g.subject = sb;
                _gradesDao.UpdateGrade(g); //ovo mora da bi odradio i u fajlu
            }
        }
    
    }
    public void RemoveSubjectHead(int id)
    {
        Subject? sb = _subjectsDao.GetSubjectById(id);
        if (sb is null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }
        List<Grade> gr = new List<Grade>(); //sluzi da bismo stavili sve sto treba za brisanje

        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.SubjectsP.Contains(sb))//moze da student bude u polozenim i nepolozenim predmetima
            {
                s.SubjectsP.Remove(sb);
                //ovo je za kada obrisemo spredmet da se obrise iz pomocne liste polozenih predmeta ko studenta

            }

            else if (s.Subjects.Contains(sb))
            {
                s.Subjects.Remove(sb);
            }
            StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id); //brisemo i vezu
            if (removedStudentSubject is null)
            {
                continue;
            }
        }
        foreach (Grade g in _gradesDao.GetAllGrades()) //promenimo u oceni studenta
        {
            if (g.subject.Id == sb.Id)
            {
                gr.Add(g); //pomocna lista iz koje cemo brisati
            }
        }

        foreach (Grade g in gr)
        {
            Grade? removedgrade = _gradesDao.RemoveGrade(g.Id);
            if (removedgrade is null)
            { continue; }
        }
        Subject? removedsb = _subjectsDao.RemoveSubject(id); //tek kad smo obrisali sve veze obrisemo i predmet
        if (removedsb is null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }

        System.Console.WriteLine("Subject removed");

    }
    // kada budes brisala za subject ne zaboravi da izbrises iz obe liste kod studenta taj subject
    // kod profesora mora da se obrise i iz liste ali i ako nema sefa cela katedra

    // ------------------------------------------CHAIR--------------------------------------------//

    public void SubscribeChair(IObserver observer)
    {
        _chairsDao.ChairObserverSub.Subscribe(observer);
    }

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

    //-----------------------------------------PROFESSOR-----------------------------------------//


   /* public void SubscribeProfessor(IObserver observer)
    {
        _professorsDao.ProfessorObserverSub.Subscribe(observer);

    }*/

    public void AddProfessorHead(Professor prof)
    {
        _professorsDao.AddProfessor(prof);
       // observerSub.NotifyObservers();
    }

    public List<Professor> GetAllProfessorsHead()
    {
       return  _professorsDao.GetAllProfessors();
    }

    public Boolean RemoveProfessorHead(int id)
    {

        Professor? pr = _professorsDao.GetProfessorById(id);
        if (pr is null)
        {
            System.Console.WriteLine("Professor not found");
            return false;
        }
        // List<Chair> chr = new List<Chair>(); //sluzi da bismo stavili sve sto treba za brisanje

        foreach (Chair ch in _chairsDao.GetAllChairs())
        {
            if (ch.Professors.Contains(pr))//moze da profesor bude u listi katedri
            {
                // ch.Professors.Remove(pr);
                //ovo je za kada obrisemo studenta da se obrise iz liste predmeta
                return false;
            }

            

            /*  StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id); //brisemo i vezu
              if (removedStudentSubject is null)
              {
                  continue ;
              }*/
        }

        foreach (Chair ch in _chairsDao.GetAllChairs())
        {
            if (ch.Chief.Id == pr.Id)
            {
                return false;
            }
        }

        
            
        foreach (Subject sb in _subjectsDao.GetAllSubjects())
        {
                if (sb.ProfessorSb.Id == pr.Id)
                {
                    return false;
                }
        }


          /*   foreach (Grade g in gr)
             {
                 Grade? removedgrade = _gradesDao.RemoveGrade(g.Id);
                 if (removedgrade is null)
                 { continue; }
             }*/

        Professor? removedpr = _professorsDao.RemoveProfessor(id); //tek kad smo obrisali sve veze obrisemo i studenta
            if (removedpr is null)
            {
                System.Console.WriteLine("Student not found");
                return false;
            }

            System.Console.WriteLine("Student removed");
       // observerSub.NotifyObservers();
        return true;
        

    }

    // ----------------------------------------GRADE--------------------------------------------//

    public void SubscribeGrade(IObserver observer)
    {
        _gradesDao.GradeObserverSub.Subscribe(observer);
    }


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
