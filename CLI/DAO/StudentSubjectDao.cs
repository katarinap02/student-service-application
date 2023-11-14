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

   


    public StudentSubjectDao()
    {
        _storage = new Storage<StudentSubject>("studentsubjects.txt");
        studentsubjects = _storage.Load();
    }


   

    public List<StudentSubject> GetAllStudentSubjects()
    {
        return studentsubjects;
    }







    /*public Model.Subject FindSubjectById(List<Subject> subjects, int tId)
    {
        return subjects.Find(subjects => subjects.Id == tId);
    }*/
}
