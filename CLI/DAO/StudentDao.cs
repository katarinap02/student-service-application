using CLI.Model;
using CLI.Observer;
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

    public ObserverSub StudentObserverSub;

    public StudentDao()
    {
        _storage = new Storage<Student>("student.txt");
        students = _storage.Load();
        StudentObserverSub = new ObserverSub();
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
        StudentObserverSub.NotifyObservers();
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
        oldst.AdressSt = st.AdressSt;
        oldst.PhoneNumber = st.PhoneNumber;
        oldst.IndexNm = st.IndexNm;
        oldst.StYear = st.StYear;
        oldst.StudentStatus = st.StudentStatus;

        _storage.Save(students);
        StudentObserverSub.NotifyObservers();
        return oldst;
    }

    public Student? RemoveStudent(int id)
    {
        Student? student = GetStudentById(id);
        if (student == null) return null;

        StudentObserverSub.NotifyObservers();
        students.Remove(student);
        _storage.Save(students);
        StudentObserverSub.NotifyObservers();

        return student;
    }

    public Student? GetStudentById(int id)
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

    public List<Student> GetAllStudents(int page, string sortCriteria, int sortDirection)
    {
        IEnumerable<Student> _students = students;
        int pageSize = 16;

        // sortiraj vehicles ukoliko je sortCriteria naveden
        switch (sortCriteria)
        {
            
            case "Name":
                _students = students.OrderBy(x => x.Name);
                break;
            case "Surname":
                _students = students.OrderBy(x => x.Surname);
                break;
            case "Index":
                _students = students.OrderBy(x => x.IndexNm);
                break;
            case "StYear":
                _students = students.OrderBy(x => x.StYear);
                break;
            case "StudentStatus":
                _students = students.OrderBy(x => x.StudentStatus);
                break;
            case "AverageNm":
                _students = students.OrderBy(x => x.AverageNm);
                break;
        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection != 0)
            _students = _students.Reverse();

        // paginacija
        _students = _students.Skip((page - 1) * pageSize).Take(pageSize);

        return _students.ToList();
    }




}
