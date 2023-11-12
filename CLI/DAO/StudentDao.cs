using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO;

public class StudentDao
{
    private readonly List<Student> students;
    private readonly Storage<Student> _storage;


    public StudentDao()
    {
        _storage = new Storage<Student>("student.txt");
        students = _storage.Load();
    }

    private int GenerateId()
    {
        if (students.Count == 0) return 0;
        return students[^1].Id + 1;
    }

    public Student AddStudent(Student st)
    {
        st.Id = GenerateId(); //generisi id za svakog studenta
        students.Add(st);
        _storage.Save(students);
        return st;
    }

    public Student? UpdateStudent(Student st)
    {
        Student? oldst = GetStudentById(st.Id); // sa istim id treba da unesemo nove podatke koji su u st
        if (oldst is null) return null;

        oldst.Name = st.Name;
        oldst.Surname = st.Surname;
        oldst.Birthdate = st.Birthdate;
        oldst.Email = st.Email;
        oldst.Adress1 = st.Adress1;
        oldst.PhoneNumber = st.PhoneNumber;
        oldst.IndexNm = st.IndexNm;
        oldst.StYear = st.StYear;

        _storage.Save(students);
        return oldst;
    }

    public Student? RemoveStudent(int id)
    {
        Student? student = GetStudentById(id);
        if (student == null) return null;

        students.Remove(student);
        _storage.Save(students);
        return student;
    }

    private Student? GetStudentById(int id)
    {
        return students.Find(v => v.Id == id);
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Model.Student FindStudentById(List<Student> students, int targetId)
    {
        return students.Find(student => student.Id == targetId);
    }


}
