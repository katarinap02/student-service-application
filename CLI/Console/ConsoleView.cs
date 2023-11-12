
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO; //dodala 
using CLI.Model; //dodala



namespace CLI.Console;

public class ConsoleView
{

    private readonly StudentDao _studentsDao;
    private readonly ProfessorDao _professorsDao;
    private readonly SubjectDao _subjectsDao;
    private readonly ChairDao _chairsDao;
    private readonly GradeDao _gradesDao;
  
    public ConsoleView(StudentDao studentsDao, ProfessorDao professorsDao, SubjectDao subjectsDao, ChairDao chairsDao, GradeDao gradesDao) //konstruktor sa parametrima
    {
        _studentsDao = studentsDao;
        _professorsDao = professorsDao;
        _subjectsDao = subjectsDao;
        _chairsDao = chairsDao;
        _gradesDao = gradesDao;
    }


    // System.Console.WriteLine("Enter a number ");
    // 2: skacemo sa 1) 
    private void HandleMenuInput(string input) 
    {
        switch (input)
        {
            case "1":
               RunMenuStudent();
                break;
            case "2":
               RunMenuProfessor();
                break;
            case "3":
               RunMenuSubject();
                break;
            case "4":
                RunMenuChair();
                break;
            case "5":
                 RunMenuGrade();
                break;
        }
    }
    
    private void Show()
    {
        System.Console.WriteLine("\nChoose an option to work with : ");
        System.Console.WriteLine("1: Student ");
        System.Console.WriteLine("2: Professor ");
        System.Console.WriteLine("3: Subject ");
        System.Console.WriteLine("4: Chair ");
        System.Console.WriteLine("5: Grade ");
        System.Console.WriteLine("0: Close");
    }
    // 1) prvo se poziva ova metoda:
    public void Run()
    {
        while (true)
        {
            Show();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "1" && userInput != "2" && userInput != "3" && userInput!="4" && userInput!="5")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuInput(userInput);
        }
    }


    ///****** STUDENT *******
    private void HandleMenuStudent(string input) 
    {
        switch (input)
        {
            case "11":
                ShowAllStudents();
                break;
            case "12":
                AddStudents();
                break;
            case "13":
                UpdateStudent();
                break;
            case "14":
                RemoveStudent();
                break;
        }
    }
    // 3: ovje skacemo sa 2) ****STUDENT****
    public void RunMenuStudent()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "11" && userInput != "12" && userInput != "13" && userInput != "14")
                System.Console.WriteLine("Choose an option again ");
            else
            HandleMenuStudent(userInput);
        }
    }
    private void ShowMenu() //bice pozvana u RunMenuStudent
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("11: Show All ");
        System.Console.WriteLine("12: Add ");
        System.Console.WriteLine("13: Update ");
        System.Console.WriteLine("14: Remove ");
        System.Console.WriteLine("0: Close");
    }

    //**********Metode za studenta***********************************************

    private void ShowAllStudents() //ispisi studente
    {
        PrintStudents(_studentsDao.GetAllStudents());
    }

    private void PrintStudents(List<Student> students) //ispisi studente/studenta
    {
        System.Console.WriteLine("STUDENTS: ");
        string header = $"ID {"",6} | Name {"",15} | Surname {"",15} | Birthdate {"",10} | Adress {"", 30} | Phone number{"",12} | Email{"",20} | Index {"", 12}| Current school year {"", 4} | Current student's status {"", 2} |";
        System.Console.WriteLine(header);
        foreach (Student v in students)
        {
            System.Console.WriteLine(v);
        }
    }

    private void AddStudents() //dodaj studenta
    {
        Student student1 = InputStudent();
        _studentsDao.AddStudent(student1);
        System.Console.WriteLine("Student added");
    }

    private Student InputStudent()
    {
        System.Console.WriteLine("Enter student's name: ");
        string name = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's surname: ");
        string surname = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's birthdate: ");
        DateOnly birthdate = ConsoleViewDate.SafeInputDate();

        System.Console.WriteLine("Enter student's name of street: ");
        string adressA = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's number of street: ");
        int adressB = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter student's city: ");
        string adressC = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's country: ");
        string adressD = ConsoleViewEmpty.SafeInputEmpty();

        Model.Adress adress = new Model.Adress(adressA, adressB, adressC, adressD);

        System.Console.WriteLine("Enter student's phone numer: ");
        int phonenumber = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter student' s email: ");
        string email = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's index course: ");
        string indexc = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter student's index number: ");
        int indexn = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter student's index year: ");
        int indexy = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter current school year: ");
        int year = ConsoleViewUtils.SafeInputInt();


        Model.Index index = new Model.Index(indexc, indexn, indexy);

         System.Console.WriteLine("Enter student's current school status (S or B): ");
         Student.Status status = ConsoleViewEnum.SafeInputEnum2();

        return new Student(name, surname, birthdate, adress, phonenumber, email, index, year, status);
    }

    private void UpdateStudent() //azuriraj studente
    {
        int id = InputId();
        Student student = InputStudent();
        student.Id = id;
        Student? updatedStudent = _studentsDao.UpdateStudent(student);
        if (updatedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student updated");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter student's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveStudent() //ukloni studenta
    {
        int id = InputId();
        Student? removedStudent = _studentsDao.RemoveStudent(id);
        if (removedStudent is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student removed");
    }

    //***********************************************************************

    //****************************PROFESSOR***********************************

    private void HandleMenuProfessor(string input)
    {
        switch (input)
        {
            case "21":
                ShowAllProfessors();
                break;
            case "22":
                AddProfessors();
                break;
            case "23":
                UpdateProfessor();
                break;
            case "24":
                RemoveProfessor();
                break;
        }
    }
    public void RunMenuProfessor()
    {
        while (true)
        {
            ShowMenuP();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "21" && userInput != "22" && userInput != "23" && userInput != "24")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuProfessor(userInput);
        }
    }

    private void ShowMenuP() 
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("21: Show All ");
        System.Console.WriteLine("22: Add ");
        System.Console.WriteLine("23: Update ");
        System.Console.WriteLine("24: Remove ");
        System.Console.WriteLine("0: Close");
    }

    private void ShowAllProfessors() //ispisi profesore
    {
        PrintProfessors(_professorsDao.GetAllProfessors());
    }

    private void PrintProfessors(List<Professor> professors) //ispisi studente/studenta
    {
        System.Console.WriteLine("PROFESSOR: ");
        string header = $"ID: {"",6} | Name: {"",21} | Surname: {"",21} | Birthdate: {"",10} | Adress: {"",21} | Phone number: {"",12} | Email: {"",30} | Title: {"",14} | Years of service: {"",3} |";
        System.Console.WriteLine(header);
        foreach (Professor v1 in professors)
        {
            System.Console.WriteLine(v1);
        }
    }

    private void AddProfessors() //dodaj profesora
    {
        Professor professor1 = InputProfessor();
        _professorsDao.AddProfessor(professor1);
        System.Console.WriteLine("Professor added");
    }

    private Professor InputProfessor()
    {
        System.Console.WriteLine("Enter professor's name: ");
        string name = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter professor's surname: ");
        string surname = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter professor's birthdate: ");
        DateOnly birthdate = ConsoleViewDate.SafeInputDate();

        System.Console.WriteLine("Enter professor's adress: ");
        int adress = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter professor's phone numer: ");
        int phonenumber = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter professor's email: ");
        string email = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter professor's title: ");
        string title = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter professor's years of service: ");
        int year = ConsoleViewUtils.SafeInputInt();

        return new Professor(name, surname, birthdate, adress, phonenumber, email, title, year);
    }

    private void UpdateProfessor() //azuriraj profesore
    {
        int id = InputIdP();
        Professor professor = InputProfessor();
        professor.Id = id;
        Professor? updatedProfessor = _professorsDao.UpdateProfessor(professor);
        if (updatedProfessor == null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }

        System.Console.WriteLine("Professor updated");
    }

    private int InputIdP()
    {
        System.Console.WriteLine("Enter professor's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveProfessor() //ukloni profesora
    {
        int id = InputIdP();
        Professor? removedProfessor = _professorsDao.RemoveProfessor(id);
        if (removedProfessor is null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }

        System.Console.WriteLine("Professor removed");
    }

    //***********************************************************************


    //****************************SUBJECT***********************************

    private void HandleMenuSubject(string input)
    {
        switch (input)
        {
            case "31":
                ShowAllSubjects();
                break;
            case "32":
                AddSubjects();
                break;
            case "33":
                UpdateSubject();
                break;
            case "34":
                RemoveSubject();
                break;
        }
    }
    public void RunMenuSubject()
    {
        while (true)
        {
            ShowMenuS();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "31" && userInput != "32" && userInput != "33" && userInput != "34")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuSubject(userInput);
        }
    }

    private void ShowMenuS()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("31: Show All ");
        System.Console.WriteLine("32: Add ");
        System.Console.WriteLine("33: Update ");
        System.Console.WriteLine("34: Remove ");
        System.Console.WriteLine("0: Close");
    }

    private void ShowAllSubjects() 
    {
        PrintSubjects(_subjectsDao.GetAllSubjects());
    }

    private void PrintSubjects(List<Subject> subjects) 
    {
        System.Console.WriteLine("SUBJECT: ");
        string header = $"ID: {"",6} | Name: {"",21} | Semester: {"",21} | Year: {"",10} | ESPB: {"",5} |";
        System.Console.WriteLine(header);
        foreach (Subject v2 in subjects)
        {
            System.Console.WriteLine(v2);
        }
    }

    private void AddSubjects() //dodaj profesora
    {
        Subject subject1 = InputSubject();
        _subjectsDao.AddSubject(subject1);
        System.Console.WriteLine("Subject added");
    }

    private Subject InputSubject()
    {
        System.Console.WriteLine("Enter subject's name: ");
        string name = ConsoleViewEmpty.SafeInputEmpty();

        System.Console.WriteLine("Enter subject's semester: Summer or Winter ");
        Subject.Semester semester = Console.ConsoleViewEnum.SafeInputEnum1();

        System.Console.WriteLine("Enter subject's year: ");
        int year = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter subject's espb: ");
        int espb = ConsoleViewUtils.SafeInputInt();

        return new Subject(name, semester, year, espb);
    }

    private void UpdateSubject() //azuriraj profesore
    {
        int id = InputIdS();
        Subject subject = InputSubject();
        subject.Id = id;
        Subject? updatedSubject = _subjectsDao.UpdateSubject(subject);
        if (updatedSubject == null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }

        System.Console.WriteLine("Subject updated");
    }

    private int InputIdS()
    {
        System.Console.WriteLine("Enter subject's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveSubject() //ukloni profesora
    {
        int id = InputIdP();
        Subject? removedSubject = _subjectsDao.RemoveSubject(id);
        if (removedSubject is null)
        {
            System.Console.WriteLine("Subject not found");
            return;
        }

        System.Console.WriteLine("Subject removed");
    }

    //***********************************************************************

    //********************CHAIRS******************************************

    private void HandleMenuChair(string input)
    {
        switch (input)
        {
            case "41":
                ShowAllChairs();
                break;
            case "42":
                AddChairs();
                break;
            case "43":
                UpdateChair();
                break;
            case "44":
                RemoveChairs();
                break;
        }
    }

    private void RemoveChairs()
    {
        throw new NotImplementedException();
    }

    public void RunMenuChair()
    {
        while (true)
        {
            ShowMenuC();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "41" && userInput != "42" && userInput != "43" && userInput != "44")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuChair(userInput);
        }
    }

    private void ShowMenuC()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("41: Show All ");
        System.Console.WriteLine("42: Add ");
        System.Console.WriteLine("43: Update ");
        System.Console.WriteLine("44: Remove ");
        System.Console.WriteLine("0: Close");
    }

    private void ShowAllChairs()
    {
        PrintChairs(_chairsDao.GetAllChairs());
    }

    private void PrintChairs(List<Chair> chairs)
    {
        System.Console.WriteLine("CHAIR: ");
        string header = $"ID: {"",6} | Name: {"",21} |"; //ZA SADA mi ispisuje samo ovo 
        System.Console.WriteLine(header);
        foreach (Chair v2 in chairs)
        {
            System.Console.WriteLine(v2);
        }
    }

    private void AddChairs() //dodaj katedru
    {
        Chair chair1 = InputChair();
        _chairsDao.AddChair(chair1);
        System.Console.WriteLine("Chair added");
    }

    private Chair InputChair()
    {
        System.Console.WriteLine("Enter Chair's name: ");
        string name = ConsoleViewEmpty.SafeInputEmpty();

        //treba popuniti


        return new Chair(name);
    }

    private void UpdateChair() //azuriraj katedru (promijeni ime)
    {
        int id = InputIdC();
        Chair chair = InputChair();
        chair.Id = id;
        Chair? updatedChair = _chairsDao.UpdateChair(chair);
        if (updatedChair == null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair updated");
    }

    private int InputIdC()
    {
        System.Console.WriteLine("Enter chair's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveChair() //ukloni katedra
    {
        int id = InputIdC();
        Chair? removedChair = _chairsDao.RemoveChair(id);
        if (removedChair is null)
        {
            System.Console.WriteLine("Chair not found");
            return;
        }

        System.Console.WriteLine("Chair removed");
    }



//********************GRADE******************************************

    private void HandleMenuGrade(string input)
    {
        switch (input)
        {
            case "51":
                ShowAllGrades();
                break;
            case "52":
                AddGrades();
                break;
            case "53":
                UpdateGrade();
                break;
            case "54":
                RemoveGrade();
                break;
        }
    }

   

    public void RunMenuGrade()
    {
        while (true)
        {
            ShowMenuG();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            if (userInput != "51" && userInput != "52" && userInput != "53" && userInput != "54")
                System.Console.WriteLine("Choose an option again ");
            else
                HandleMenuGrade(userInput);
        }
    }

    private void ShowMenuG()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("51: Show All ");
        System.Console.WriteLine("52: Add ");
        System.Console.WriteLine("53: Update ");
        System.Console.WriteLine("54: Remove ");
        System.Console.WriteLine("0: Close");
    }

    private void ShowAllGrades()
    {
        PrintGrades(_gradesDao.GetAllGrades());
    }

    private void PrintGrades(List<Grade> grades)
    {
        System.Console.WriteLine("GRADE: ");
        string header = $"ID: {"",6} | Student's Name: {"",10}| Student's Surname: {"",10}| Subject's Name: {"",10}| Grade: {"",2} | Date: {"", 12}| "; //ZA SADA mi ispisuje samo ovo 
        System.Console.WriteLine(header);
        foreach (Grade v2 in grades)
        {
            System.Console.WriteLine(v2);
        }
    }

    private void AddGrades() //dodaj katedru
    {
        Grade grade1 = InputGrade();
        _gradesDao.AddGrade(grade1);
        System.Console.WriteLine("Grade added");
    }

    private Grade InputGrade()
    {

        System.Console.WriteLine("Enter Student's ID: ");
        int Idst = ConsoleViewUtils.SafeInputInt();
        
        while ((_studentsDao.FindStudentById(_studentsDao.GetAllStudents(), Idst)) == null)
        {
            System.Console.WriteLine("Student not found, try again ");
             Idst = ConsoleViewUtils.SafeInputInt();
        }

        System.Console.WriteLine("Enter Subjects's ID: ");
        int Idsu = ConsoleViewUtils.SafeInputInt();

        while ((_subjectsDao.FindSubjectById(_subjectsDao.GetAllSubjects(), Idsu)) == null)
        {
            System.Console.WriteLine("Subject not found, try again ");
            Idsu = ConsoleViewUtils.SafeInputInt();
        }

        System.Console.WriteLine("Enter Grade's value: ");
        int grade = ConsoleViewGrade.SafeInputGrade();

        System.Console.WriteLine("Enter Grade's date: ");
        DateOnly date = ConsoleViewDate.SafeInputDate();
        //treba popuniti

        Model.Student student = _studentsDao.FindStudentById(_studentsDao.GetAllStudents(), Idst);
        Model.Subject subject = _subjectsDao.FindSubjectById(_subjectsDao.GetAllSubjects(), Idsu);

            return new Grade(student, subject, grade, date);
        
        
        

        
    }

    private void UpdateGrade() //azuriraj katedru (promijeni ime)
    {
        int id = InputIdG();
       Grade grade= InputGrade();
        grade.Id = id;
        Grade? updatedGrade = _gradesDao.UpdateGrade(grade);
        if (updatedGrade == null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }

        System.Console.WriteLine("Grade updated");
    }

    private int InputIdG()
    {
        System.Console.WriteLine("Enter grades's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveGrade() //ukloni katedra
    {
        int id = InputIdG();
        Grade? removedGrade = _gradesDao.RemoveGrade(id);
        if (removedGrade is null)
        {
            System.Console.WriteLine("Grade not found");
            return;
        }

        System.Console.WriteLine("Grade removed");
    }


}
