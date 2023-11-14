using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO;

public class StudentSubjectDao
{
    private readonly List<StudentSubject> studentsubjects;
    private readonly Storage<StudentSubject> _storage;

    private readonly List<Student> students;
    private readonly List<Subject> subjects;


    public StudentSubjectDao()
    {
        _storage = new Storage<StudentSubject>("studentsubject.txt");
        studentsubjects = _storage.Load();
    }

  

   
    private Subject? GetSubjectById(int id)
    {
        return subjects.Find(v => v.Id == id);
    }

    private Student? GetStudentById(int id)
    {
        return students.Find(v => v.Id == id);
    }

    public List<Subject> GetAllSubjects()
    {
        return subjects;
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    /*public Model.Subject FindSubjectById(List<Subject> subjects, int tId)
    {
        return subjects.Find(subjects => subjects.Id == tId);
    }*/
}
