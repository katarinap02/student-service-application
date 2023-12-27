using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO
{
    public class SubjectDao
    {
        private readonly List<Subject> subjects;
        private readonly Storage<Subject> _storage;

        public ObserverSub SubjectObserverSub;
        public SubjectDao()
        {
            _storage = new Storage<Subject>("subject.txt");
            subjects = _storage.Load();
            SubjectObserverSub = new ObserverSub();
        }

        private int GenerateId()
        {
            if (subjects.Count == 0) return 0;
            return subjects[^1].Id + 1;
        }

        public Subject AddSubject(Subject sub)
        {
            sub.Id = GenerateId(); //generisi id za svaki predmet
            subjects.Add(sub);
            _storage.Save(subjects);
            SubjectObserverSub.NotifyObservers();
            return sub;
        }

        public Subject? UpdateSubject(Subject sub)
        {
            Subject? oldsub = GetSubjectById(sub.Id); // sa istim id treba da unesemo nove podatke koji su u sub
            if (oldsub is null) return null;

            oldsub.Name = sub.Name;
            oldsub.SYear = sub.SYear;
            oldsub.NumEspb= sub.NumEspb;    

            _storage.Save(subjects);
            SubjectObserverSub.NotifyObservers();
            return oldsub;
        }

        public Subject? RemoveSubject(int id)
        {
            Subject? subject = GetSubjectById(id);
            if (subject == null) return null;

            subjects.Remove(subject);
            _storage.Save(subjects);
            SubjectObserverSub.NotifyObservers();
            return subject;
        }

        public Subject? GetSubjectById(int id)
        {
            return subjects.Find(v => v.Id == id);
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }

        public Model.Subject FindSubjectById(List<Subject> subjects, int tId)
        {
            return subjects.Find(subjects => subjects.Id == tId);
        }

        public void setProfessor(Professor professor, Subject sb)
        {
            sb.ProfessorSb = professor;
        }
    }
}
