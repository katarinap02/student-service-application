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

    public StudentSubject? RemoveStudentSubject(int id)
    {
        StudentSubject? studentsubject = GetStudentSubjectByIdSt(id);
        if (studentsubject == null) return null;

        studentsubjects.Remove(studentsubject);
        _storage.Save(studentsubjects);
        return studentsubject;
    }

    public StudentSubject? GetStudentSubjectByIdSt(int id)
    {
        return studentsubjects.Find(v => v.StudentId == id);
    }

}
