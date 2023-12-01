using CLI.Model;
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
    { }

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
        Student? updatedStudent = _studentsDao.UpdateStudent(st);
        if (updatedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student updated");
    }
    public void RemoveStudentHead(int id)
    {
        Student? removedStudent = _studentsDao.RemoveStudent(id);
        if (removedStudent is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student removed");
        Student? st = _studentsDao.GetStudentById(id);

        foreach (Subject s in _subjectsDao.GetAllSubjects())
        {
            if (s.StudentsP.Contains(st))
            {
                s.StudentsP.Remove(st);
                //ovo je za kada obrisemo studenta da se obrise iz liste predmeta i da obrisemo vezu u StudentSubject
                
            }

            else if (s.StudentsF.Contains(st))
            {
                s.StudentsF.Remove(st);
            }

            StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id);
            if (removedStudentSubject is null)
            {
                break;
            }
        }

        

    }
}
