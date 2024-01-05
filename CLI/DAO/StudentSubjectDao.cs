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
    public StudentSubject AddStudentSubjuect(StudentSubject ss)
    {
        //ss.Id = GenerateId(); //generisi id za svakog profesora
        studentsubjects.Add(ss);
        _storage.Save(studentsubjects);
        //ProfessorObserverSub.NotifyObservers();
        return ss;
    }

    public StudentSubject? RemoveStudentSubject(int id)
    {
        StudentSubject? studentsubject = GetStudentSubjectByIdSt(id);
        if (studentsubject == null) return null;

        studentsubjects.Remove(studentsubject);
        _storage.Save(studentsubjects);
        return studentsubject;
    }
    public StudentSubject? RemoveStudentSubject2(int idSt, int idSb)
    {
        StudentSubject? studentsubject = GetStudentSubjectByIds(idSt, idSb);
        if (studentsubject == null) return null;

        studentsubjects.Remove(studentsubject);
        _storage.Save(studentsubjects);
        return studentsubject;
    }

    public StudentSubject? GetStudentSubjectByIds(int studentId, int subjectId)
    {
        return studentsubjects.Find(v => v.StudentId == studentId && v.SubjectId == subjectId);
    }

    public StudentSubject? GetStudentSubjectByIdSt(int id)
    {
        return studentsubjects.Find(v => v.StudentId == id);
    }

}
