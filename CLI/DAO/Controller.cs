using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO;

public class HeadDao
{
    private readonly StudentDao _studentsDao;
    private readonly ProfessorDao _professorsDao;
    private readonly SubjectDao _subjectsDao;
    private readonly ChairDao _chairsDao;
    private readonly GradeDao _gradesDao;
    private readonly StudentSubjectDao _studentsubjectsDao;
    private readonly ChairProfessorDao _chairprofessorDao;
    
    public ObserverSub observerSub ;


    public HeadDao()
    {
        _studentsDao= new StudentDao();
        _professorsDao= new ProfessorDao();
        _subjectsDao = new SubjectDao() ;
        _chairsDao = new ChairDao();
        _gradesDao = new GradeDao();
        _studentsubjectsDao = new StudentSubjectDao();
        _chairprofessorDao= new ChairProfessorDao();
         observerSub = new ObserverSub();
    }

    public HeadDao(StudentDao studentsDao, ProfessorDao professorsDao, SubjectDao subjectsDao, ChairDao chairsDao, GradeDao gradesDao, StudentSubjectDao studentsubjectDao, ChairProfessorDao chairprofessorDao) //konstruktor sa parametrima
    {
        _studentsDao = studentsDao;
        _professorsDao = professorsDao;
        _subjectsDao = subjectsDao;
        _chairsDao = chairsDao;
        _gradesDao = gradesDao;
        _studentsubjectsDao = studentsubjectDao;
        _chairprofessorDao = chairprofessorDao;
        observerSub = new ObserverSub();
    }

    public void SubscribeStudent(IObserver observer)
    {
        _studentsDao.StudentObserverSub.Subscribe(observer);
    }

    public void AddStudentHead(Student st)
    {
        _studentsDao.AddStudent(st);

        


        observerSub.NotifyObservers();
    }

   

    public List<Student> GetAllStudentsHead()
    {
        return _studentsDao.GetAllStudents();
        
    }

    public List<Grade> getGradesForStudent(Student st) 
    {
        List<Grade> grades = new List<Grade>();
        foreach(Grade g in _gradesDao.GetAllGrades())
            {
            if(g.student.Id == st.Id)
            {
                grades.Add(g);
            }
        }
        observerSub.NotifyObservers();
        return grades;
        

    }

    public List<Subject> getFailedSubjects(Student student) // id studenta za kog gledamo nepolozene predmete
    {
        List<Subject> failedSubjects = new List<Subject>();

        if (student != null)
        {
            List<StudentSubject> studentSubjects = _studentsubjectsDao.GetAllStudentSubjects();
            foreach (Subject subject in _subjectsDao.GetAllSubjects()) //prvo ubacimo sve predmete iz liste
            {
                foreach(StudentSubject sb in studentSubjects) {

                    if (sb != null && sb.SubjectId == subject.Id && sb.StudentId == student.Id)
                        failedSubjects.Add(subject);
                }
               
                
            }

            List<Subject> list2 = new List<Subject>();

            foreach (Subject subject in failedSubjects)
            {
                list2.Add(subject); //treba nam kopija liste
            }



            foreach (Grade grade in getGradesForStudent(student)) //zatim izbacimo one koji imaju trenutno ocenu
           {
               foreach(Subject sub in list2) 
                {
                    if(sub.Id == grade.subject.Id)
                        failedSubjects.Remove(sub);
                }
            }
        }

        observerSub.NotifyObservers();
        return failedSubjects;
    }

    public List<Subject> getSubjectsForStudent(Student student)  //studentu dodjeljuje predmet koji moze da slusa a koji vec ne slusa
    {
        List<Subject> subjectsForStudent = new List<Subject>();


        List<StudentSubject> studentSubjects = _studentsubjectsDao.GetAllStudentSubjects();

        foreach (Subject subject in _subjectsDao.GetAllSubjects())
        {
            foreach (StudentSubject sb in studentSubjects)
            {

                if ( sb.SubjectId != subject.Id && sb.StudentId != student.Id && student.StYear>=subject.SYear)
                {
                    subjectsForStudent.Add(subject);
                    break;
                }
                    
                    
            }
            //kada dodas studenta u predmet dodaj i vezu student pohadja predmet

            /* foreach (Grade grade in getGradesForStudent(student))
                    {
                        if (subject.Id != grade.subject.Id && student.Id!=grade.student.Id)
                        {
                            subjectsForStudent.Add(subject);
                          //  StudentSubject studentSubject = new StudentSubject(student.Id, subject.Id); //dodajem novu vezu
                            //studentSubjects.Add(studentSubject);
                        }
                    }*/

        }

        List<Subject> studentSubjectsCp = new List<Subject>();
        foreach (Subject subject in subjectsForStudent)
        {
            studentSubjectsCp.Add(subject); //uklonila sam i one predmete koji se nalaze u listi nepolozenih
        }

        foreach (Subject sb in getFailedSubjects(student))
        {
            foreach (Subject sub in studentSubjectsCp)
            {
                if (sub.Id == sb.Id)
                    subjectsForStudent.Remove(sub);
            }
        }


        List<Subject> studentSubjectsCopy = new List<Subject>();
        foreach (Subject subject in subjectsForStudent)
        {
            studentSubjectsCopy.Add(subject); //treba nam kopija liste
        }

        foreach (Grade grade in getGradesForStudent(student))
        { 
            foreach (Subject sub in studentSubjectsCopy)
            {
                if (sub.Id == grade.subject.Id && student.Id==grade.student.Id)
                    subjectsForStudent.Remove(sub);
            }
        }

        observerSub.NotifyObservers();
        return subjectsForStudent;

    }
    public List<Student> studentsfrombothSubjects(Subject sub1, Subject sub2)
    {
        List<Student> students = new List<Student>();

        foreach (StudentSubject ss1 in _studentsubjectsDao.GetAllStudentSubjects())
        {
            if (ss1.SubjectId == sub1.Id)
            {

                foreach (StudentSubject ss2 in _studentsubjectsDao.GetAllStudentSubjects())
                {
                    if(ss2.SubjectId== sub2.Id && ss2.StudentId==ss1.StudentId)
                        students.Add(_studentsDao.GetStudentById(ss1.StudentId));
                }
            }
        }

        List<Student> _studentsList = new List<Student>();
        _studentsList = students.Distinct().ToList();






        return _studentsList;


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
        observerSub.NotifyObservers();
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
                gr.Add(g); //pomocna lista iz koje cemo brisati
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

        observerSub.NotifyObservers();

    }
    //*****************STUDENTI KOJI SLUSAJU PREDMET KOD PROFESORA********************//
    public List<Student> ProfessorTeachesStudents(Professor professor)
    {
        List<Student> _students = new List<Student>();
        foreach (StudentSubject studentSubject in _studentsubjectsDao.GetAllStudentSubjects())
        {
            Student student = new Student();
            student = _studentsDao.GetStudentById(studentSubject.StudentId);
            Subject subject= new Subject();
            subject= _subjectsDao.GetSubjectById(studentSubject.SubjectId);
            if (subject != null && student != null)
            {

                foreach(Grade grade in _gradesDao.GetAllGrades())
                {

                    if (grade.subject.Id != subject.Id)   //necemo da nam se prikazuju ucenici koji imaju ocjenu iz tog predmeta, jer ako su ga polozili vise ga ne slusaju
                    {

                        if (subject.idProf == professor.Id)
                        {
                            _students.Add(student);
                        }
                    }
                }


                
            }
        }

       

        //nisam dodala provjeru toga da li je student polozio taj predmet jer ako ga je polozio onda ga ni ne slusa kod tog profesora


        List<Student> _studentsList = new List<Student>();
        _studentsList= _students.Distinct().ToList(); //funckija koja uklanja duplikate

        return _studentsList;

    }

    public void SubscribeSubject(IObserver observer)
    {
        _subjectsDao.SubjectObserverSub.Subscribe(observer);
    }

    public void AddSubjectHead(Subject sb)
    {
        _subjectsDao.AddSubject(sb);            
        observerSub.NotifyObservers();

    }

    public List<Subject> GetAllSubjectsHead()
    {
        return _subjectsDao.GetAllSubjects();
        
    }

    public Subject getSubjectById(int id)
    {
        return _subjectsDao.GetSubjectById(id);
    }

    public void UpdateSubjectHead(Subject sb)
    {
        Subject? olds = _subjectsDao.UpdateSubject(sb);
        if (olds == null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }

        System.Console.WriteLine("Subject updated");
        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.SubjectsP.Contains(olds))//moze da subject bude u polozenim i nepolozenim predmetima kod studenta
            {
                s.SubjectsP.Remove(olds); //izbaci ga iz liste(stare vrednosti)
                s.SubjectsP.Add(sb); // ubaci ponovo kad je odradjen upgrade

            }

            else if (s.Subjects.Contains(olds))
            {
                s.Subjects.Remove(olds);
                s.Subjects.Add(sb);
            }
        }
        foreach (Grade g in _gradesDao.GetAllGrades()) //promijenimo u ocjeni predmet
        {
            if (g.subject.Id == olds.Id)
            {
                g.subject = sb;
                _gradesDao.UpdateGrade(g); //ovo mora da bi odradio i u fajlu
            }
        }
        observerSub.NotifyObservers();

    }
    public void RemoveSubjectHead(int id)
    {
        Subject? sb = _subjectsDao.GetSubjectById(id);
        if (sb is null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }
        List<Grade> gr = new List<Grade>(); //sluzi da bismo stavili sve sto treba za brisanje

        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.SubjectsP.Contains(sb))//moze da student bude u polozenim i nepolozenim predmetima
            {
                s.SubjectsP.Remove(sb);
                //ovo je za kada obrisemo spredmet da se obrise iz pomocne liste polozenih predmeta ko studenta

            }

            else if (s.Subjects.Contains(sb))
            {
                s.Subjects.Remove(sb);
            }
            StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id); //brisemo i vezu
            if (removedStudentSubject is null)
            {
                continue;
            }
        }
        foreach (Grade g in _gradesDao.GetAllGrades()) //promenimo u oceni studenta
        {
            if (g.subject.Id == sb.Id)
            {
                gr.Add(g); //pomocna lista iz koje cemo brisati
            }
        }

        foreach (Grade g in gr)
        {
            Grade? removedgrade = _gradesDao.RemoveGrade(g.Id);
            if (removedgrade is null)
            { continue; }
        }
        Subject? removedsb = _subjectsDao.RemoveSubject(id); //tek kad smo obrisali sve veze obrisemo i predmet
        if (removedsb is null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }
        observerSub.NotifyObservers();
        System.Console.WriteLine("Subject removed");

    }

    public void setProfessor(Professor p, Subject sb)
    {
        _subjectsDao.setProfessor(p, sb);
    }

    public List<Subject> getSubjectsForProfessor(Professor prof) //videcemo da li treba
    {
        List<Subject> subjects = new List<Subject>();
        foreach (Subject s in _subjectsDao.GetAllSubjects())
        {
            if(s.idProf != -1) {
                if (s.idProf == prof.Id)
                {
                    subjects.Add(s);
                }
            }
            
        }
        observerSub.NotifyObservers();
        return subjects;


    }

    public List<Subject> getSubjectsWithoutProfessor(Professor prof) 
    {
        List<Subject> subjects = new List<Subject>();
        foreach (Subject s in _subjectsDao.GetAllSubjects())
        {
            if (s.idProf != -1)
            {
                if (s.idProf != prof.Id)
                {
                    subjects.Add(s);
                }
            }
            else
            {
                subjects.Add(s);
            }

        }
        observerSub.NotifyObservers();
        return subjects;


    }



    // kada budes brisala za subject ne zaboravi da izbrises iz obe liste kod studenta taj subject
    // kod profesora mora da se obrise i iz liste ali i ako nema sefa cela katedra

    // ------------------------------------------CHAIR--------------------------------------------//

    public void SubscribeChair(IObserver observer)
    {
        _chairsDao.ChairObserverSub.Subscribe(observer);
    }

    public List<Chair> GetAllChairsHead()
    {
        return _chairsDao.GetAllChairs();
    }

    public void AddChairHead(Chair ch)
    {
        _chairsDao.AddChair(ch);
    }

    public void UpdateChairHead(Chair ch)
    {
        Chair? oldchair = _chairsDao.UpdateChair(ch);
        if (oldchair == null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair updated");

        foreach (Professor s in _professorsDao.GetAllProfessors())
        {
            if (s.chairs.Contains(oldchair))
            {
                s.chairs.Remove(oldchair); 
                s.chairs.Add(ch); 

            }
        }
    }

    public void RemoveChairHead(int id)
    {
        Chair? ch = _chairsDao.GetChairById(id);
        if (ch is null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }
        foreach (Professor s in _professorsDao.GetAllProfessors())
        {
            if (s.chairs.Contains(ch))
            {
                s.chairs.Remove(ch);

            }
        }

        ChairProfessor? removedChairProfessor = _chairprofessorDao.RemoveChairProfessor(id); //brisemo i vezu
        if (removedChairProfessor is null)
        {
            //continue;
        }

        Chair? removedChair = _chairsDao.RemoveChair(id);
        if (removedChair is null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair removed");
    }

    //-----------------------------------------PROFESSOR-----------------------------------------//


    public void SubscribeProfessor(IObserver observer)
    {
        _professorsDao.ProfessorObserverSub.Subscribe(observer);

    }

    public Professor getProfessorById(int id)
    {
        return _professorsDao.GetProfessorById(id);
    }

    public void AddProfessorHead(Professor prof)
    {
        _professorsDao.AddProfessor(prof);
        observerSub.NotifyObservers();
    }

    public List<Professor> GetAllProfessorsHead()
    {
       return  _professorsDao.GetAllProfessors();
    }
    public void UpdateProfessorHead(Professor pr)
    {
        Professor? olds = _professorsDao.UpdateProfessor(pr);
        if (olds == null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }

        System.Console.WriteLine("Professor updated");
        foreach (Chair ch in _chairsDao.GetAllChairs())
        {
            if (ch.Professors.Contains(olds)) //moze da student bude u polozenim i nepolozenim predmetima
            {
                ch.Professors.Remove(olds); //izbaci ga iz liste(stare vrednosti)
                ch.Professors.Add(pr);  // ubaci ponovo kad je odradjen upgrade

            }

            
        }

        foreach (Chair ch in _chairsDao.GetAllChairs()) //promenimo u oceni studenta
        {
            if(ch.Chief != null)
            { 
            if (ch.Chief.Id == olds.Id)
            {
                ch.Chief = pr;
                _chairsDao.UpdateChair(ch); //ovo mora da bi odradio i u fajlu
            }
            }
        }
        observerSub.NotifyObservers();
    }

    public Boolean RemoveProfessorHead(int id)
    {

        Professor? pr = _professorsDao.GetProfessorById(id);
        if (pr is null)
        {
            System.Console.WriteLine("Professor not found");
            return false;
        }
        // List<Chair> chr = new List<Chair>(); //sluzi da bismo stavili sve sto treba za brisanje

        foreach (Chair ch in _chairsDao.GetAllChairs())
        {
            if (ch.Professors.Contains(pr))//moze da profesor bude u listi katedri
            {
                // ch.Professors.Remove(pr);
                //ovo je za kada obrisemo studenta da se obrise iz liste predmeta
                return false;
            }

            

            /*  StudentSubject? removedStudentSubject = _studentsubjectsDao.RemoveStudentSubject(id); //brisemo i vezu
              if (removedStudentSubject is null)
              {
                  continue ;
              }*/
        }

        foreach (Chair ch in _chairsDao.GetAllChairs())
        {
            if (ch.Chief.Id == pr.Id)
            {
                return false;
            }
        }

        
            
        foreach (Subject sb in _subjectsDao.GetAllSubjects())
        {
            if (sb.ProfessorSb != null)
            {


                if (sb.ProfessorSb.Id == pr.Id)
                {
                    return false;
                }
            }
        }


          /*   foreach (Grade g in gr)
             {
                 Grade? removedgrade = _gradesDao.RemoveGrade(g.Id);
                 if (removedgrade is null)
                 { continue; }
             }*/

        Professor? removedpr = _professorsDao.RemoveProfessor(id); //tek kad smo obrisali sve veze obrisemo i studenta
            if (removedpr is null)
            {
                System.Console.WriteLine("Student not found");
                return false;
            }

            System.Console.WriteLine("Student removed");
        observerSub.NotifyObservers();
        return true;


        
        

    }

    public void AddProfessorToSubject(Subject subject, Professor professor)
    {
        if(subject!= null && professor != null) {
           Subject sb = _subjectsDao.setProfessor(professor, subject); //stari
           
        }
        observerSub.NotifyObservers();
        
    }

    public void RemoveProfessorFromSubject(Subject subject)
    {
        if (subject != null)
        {
            Subject sb = _subjectsDao.setProfessor(null, subject); //stari

        }
        observerSub.NotifyObservers();

    }
    public List<Subject> anotherSubjects(Subject sub)
    {

        /*List <Subject> anotherSubjects = _subjectsDao.GetAllSubjects().Where(item => !item.Equals(sub)).ToList();
        observerSub.NotifyObservers();
        return anotherSubjects;*/
        /* List<Subject> newList = new List<Subject>(_subjectsDao.GetAllSubjects());
         newList.Remove(sub);
         return newList;*/
        /* List<Subject> list = new List<Subject>();
         list = _subjectsDao.GetAllSubjects();
         list.RemoveAll(item => item.Equals(sub));
         List<Subject> list2 = list.RemoveAll(item => item.Equals(sub));
         list.RemoveAll(item => item.Equals(sub));
         return list;*/
        List<Subject> list = new List<Subject>();
        foreach (Subject subject in _subjectsDao.GetAllSubjects())
        {
            if(sub.Id!=subject.Id)
            {
                list.Add(subject);
            }
        }

        return list;

    }
   // List<T> newList = originalList.Where(item => !item.Equals(elementToRemove)).ToList();

    // ----------------------------------------GRADE--------------------------------------------//

    public void SubscribeGrade(IObserver observer)
    {
        _gradesDao.GradeObserverSub.Subscribe(observer);
    }

    public List<Grade> GetAllGradesHead()
    {
        return _gradesDao.GetAllGrades();



    }



    public void AddGradeHead(Grade gd) // ali ne znam da li ce nam trebati
    {
        Subject sb = gd.subject;
        Student st = gd.student;
        if (sb.StudentsF.Contains(st)) //kada dodamo ocenu za neki predmet student se prebacuje iz u listu studenata polozenih predmeta
        {
            sb.StudentsF.Remove(st);
            sb.StudentsP.Add(st);

        }
        if(st.Subjects.Contains(sb)) // dodala sam pomocnu listu u kojoj cuvamo polozene predmete studenta
        {
            st.Subjects.Remove(sb);
            st.SubjectsP.Add(sb); //*************************
          
        }


        StudentSubject studentSubject = new StudentSubject(st.Id,sb.Id);
        _studentsubjectsDao.AddStudentSubjuect(studentSubject); //brisemo i vezu?? ja sam stavila da dodajem vezu
        

        _gradesDao.AddGrade(gd);

        observerSub.NotifyObservers();

    }
    public void MakeNewGradeHead(Grade gd, Subject sb, Student st) // ali ne znam da li ce nam trebati
    {

        //  StudentSubject studentSubject = new StudentSubject(st.Id, sb.Id);
        //  _studentsubjectsDao.AddStudentSubjuect(studentSubject); //ovo ne treba jer veza vec postoji


        

        _gradesDao.MakeNewGrade(gd, sb, st);

        observerSub.NotifyObservers();

    }

    public void UpdateGreadHead(Grade g)
    {
        Grade? oldg = _gradesDao.UpdateGrade(g);
        if (oldg == null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }

        System.Console.WriteLine("Grade updated");

        foreach (Student s in _studentsDao.GetAllStudents()) //update za ocenu u studentu
        {
            if (s.Grades.Contains(oldg))
            {
                s.Grades.Remove(oldg); 
                s.Grades.Add(oldg); 

            }
        }
    }


    public void RemoveGradeHead(int id) //treba dodati da vraca u listu nepolozenih predmeta - dodato :)
    {
        Grade? gr = _gradesDao.GetGradeById(id);
        Subject sb = gr.subject;
        Student st = gr.student;

        

        if (gr is null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }
        foreach (Student s in _studentsDao.GetAllStudents())
        {
            if (s.Grades.Contains(gr))
            {
                s.Grades.Remove(gr);

            }
        }

       // StudentSubject studentSubject = new StudentSubject(st.Id, sb.Id);
       // _studentsubjectsDao.AddStudentSubjuect(studentSubject); //ovo ne treba jer veza vec postoji



        Grade? removedGrade = _gradesDao.RemoveGrade(id);
        if (removedGrade is null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }


        System.Console.WriteLine("Grade removed");

      
        observerSub.NotifyObservers();
    }

    //StudentSubject
    //treba mi funkcija koja mi vraca da li postoji veza Student Subject 

    public StudentSubject getStudentSubjectByIdHead(int id) 
    {
        return _studentsubjectsDao.GetStudentSubjectByIdSt(id);
    }
    public void AddStudentSubjectHead(StudentSubject studentSubject)
    {
        _studentsubjectsDao.AddStudentSubjuect(studentSubject);
        observerSub.NotifyObservers();
    }
    public void RemoveStudentSubjectHead(int idSu, int idSb)
    {
        _studentsubjectsDao.RemoveStudentSubject2(idSu, idSb);
        observerSub.NotifyObservers();
    }

    public void RemoveProfessorFromSubject(int idSb)
    {
        Subject? sub = _subjectsDao.GetSubjectById(idSb);
        if (sub != null) {
            _subjectsDao.setProfessor(null, sub);
        }
        observerSub.NotifyObservers();
    }
    //***********************FUNKCIJA ZA SETOVANJE IDPROFESORA U SUBJECTU**********************//




    //*************************FUNKCIJA ZA TRAZENJE PROFESORA SA KATEDRE SA USLOVIMA**************

    public List<Professor> GetAllChairChef(int id)
    {
        List<Professor> professor = new List<Professor>();
        Chair? chair = _chairsDao.GetChairById(id);
        if(chair != null) {
            foreach (Professor p in _professorsDao.GetAllProfessors())
            {
                foreach (ChairProfessor ch in _chairprofessorDao.GetAllChairProfessor())
                {
                    if (ch.ChairId == chair.Id && ch.ProfessorId == p.Id)
                    {
                        professor.Add(p);
                    }
                }
            }
        }
        observerSub.NotifyObservers();
        return professor;
            
    }

    public bool AddProfessorToChair(int id, Professor p)
    {
        Chair? ch = _chairsDao.GetChairById(id);
        if(ch != null && p!= null && p.YearS > 5) {
            if (p.Title == "redovni" || p.Title == "vanredni")
            {
                Chair chair =_chairsDao.setProfessor(p, ch);
                observerSub.NotifyObservers();
                return true;
            }
        }

        return false;
    }

    public List<Subject> GetAllSubjectsForChair(int id) //trazi predmete za sve profesore neke katedre
    {
        List<Subject> subjects = new List<Subject>();
        Chair? chair = _chairsDao.GetChairById(id);
        if(chair!= null)
        {
            foreach (Professor p in _professorsDao.GetAllProfessors())
            {
                foreach (ChairProfessor ch in _chairprofessorDao.GetAllChairProfessor())
                {
                    if (ch.ChairId == chair.Id && ch.ProfessorId == p.Id) //nasli smo sve profesore
                    {
                        foreach(Subject s in _subjectsDao.GetAllSubjects())
                        {
                            if(s.Id == p.Id)
                                subjects.Add(s);
                        }
                    }
                }
            }
        }

        return subjects;

    }


    //////////////**********************AVERAGE ++  COUNT ESPB ***************//////////////////////////
    
    public double getAverageForStudent(Student student)
    {
        int sum = 0;
        int count = 0;
        foreach (Grade g in  _gradesDao.GetAllGrades())
        {
            if(g.student.Id == student.Id)
            {
                sum = sum + g.grade;
                count++;
            }
        }
        if (count == 0)
            return 0;

        double av = (double)sum / count;
        return av;
    }

    public int getCountEspbForStudent(Student student)
    {
       
        int sum = 0;
        foreach (Grade g in _gradesDao.GetAllGrades())
        {
            if (g.student.Id == student.Id)
            {
                sum = sum + g.subject.NumEspb;
                
            }
        }
        return sum;

       
    }

}
