﻿using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
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
    

    public HeadDao()
    { 
       
    }

    public HeadDao(StudentDao studentsDao, ProfessorDao professorsDao, SubjectDao subjectsDao, ChairDao chairsDao, GradeDao gradesDao, StudentSubjectDao studentsubjectDao) //konstruktor sa parametrima
    {
        _studentsDao = studentsDao;
        _professorsDao = professorsDao;
        _subjectsDao = subjectsDao;
        _chairsDao = chairsDao;
        _gradesDao = gradesDao;
        _studentsubjectsDao = studentsubjectDao;
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
                gr.Add(g);
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
}
