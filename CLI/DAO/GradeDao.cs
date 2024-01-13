using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class GradeDao
{
    private readonly List<Grade> grades;
    private readonly Storage<Grade> _storage;

    public ObserverSub GradeObserverSub;
    public GradeDao()
    {
        _storage = new Storage<Grade>("grade.txt");
        grades = _storage.Load();
        GradeObserverSub = new ObserverSub();
    }



    private int GenerateId()
    {
        if (grades.Count == 0) return 0;
        return grades[^1].Id + 1;
    }

    public Grade AddGrade(Grade gr)
    {
        gr.Id = GenerateId(); //generisi id za svaku katedru
        grades.Add(gr);
        _storage.Save(grades);
        GradeObserverSub.NotifyObservers();
        return gr;
    }
    public Grade MakeNewGrade(Grade gr, Subject sb, Student st)
    {
        gr.Id = GenerateId();
        gr.subject = sb;
        gr.student = st;
        grades.Add(gr);
        _storage.Save(grades);
        GradeObserverSub.NotifyObservers();
        return gr;
    }
    public Grade? UpdateGrade(Grade gr)
    {
        Grade? oldgr = GetGradeById(gr.Id); // sa istim id treba da unesemo nove podatke koji su u st
        if (oldgr is null) return null;

        oldgr.student = gr.student;
        oldgr.subject = gr.subject;
        oldgr.grade = gr.grade; 
        oldgr.date = gr.date;

        _storage.Save(grades);
        GradeObserverSub.NotifyObservers();
        return oldgr;
    }

    public Grade? RemoveGrade(int id)
    {
        Grade? grade = GetGradeById(id);
        if (grade == null) return null;
        GradeObserverSub.NotifyObservers();

        grades.Remove(grade);
        _storage.Save(grades);
        GradeObserverSub.NotifyObservers();
        return grade;
    }

    public Grade? GetGradeById(int id)
    {
        return grades.Find(v => v.Id == id);
    }

    public List<Grade> GetAllGrades()
    {
        return grades;
    }

    
}

