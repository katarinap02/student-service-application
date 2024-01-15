using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CLI.Model;
using GUI.View.Add;

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for InsertStudent.xaml
    /// </summary>
    public partial class InsertStudent : Window
    {
        StudentDTO studentDTO;
        SubjectDTO subjectDTO;
        HeadDao headDao;
        StudentDTO pomStd;
        
       // GradeDTO gradeDTO;
        public static RoutedCommand NewCommand = new RoutedCommand();
        public ObservableCollection<GradeDTO> Grades { get; set; }
        public ObservableCollection<SubjectDTO> Subjects { get; set; }

        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public InsertStudent(HeadDao contr, StudentDTO std)
        {
            InitializeComponent();
          
            headDao = contr;
            pomStd = std;
            
            studentDTO = new StudentDTO(std);
            subjectDTO = new SubjectDTO();
            DataContext= studentDTO;
          
            Grades = new ObservableCollection<GradeDTO>();
            Subjects = new ObservableCollection<SubjectDTO>();
            Professors = new ObservableCollection<ProfessorDTO>();
            //headDao.observerSub.Subscribe(this);

            dataGridPassed.ItemsSource = Grades;
            dataGridFiled.ItemsSource = Subjects;
            dataGridProf.ItemsSource = Professors;

            if (std.StudentStatus == Student.Status.B)
                comboBoxStatus.SelectedItem = StatusB;
            else
                comboBoxStatus.SelectedItem = StatusS;

            UpdateGrade(std.ToStudent());
            UpdateSubject(std.ToStudent());
            UpdateSubjectProfessor(std.ToStudent());
            
            
        }

        public void UpdateSubjectProfessor(Student std)
        {
            Professors.Clear();
            foreach (Professor professor in headDao.getProfessorForStudent(std)) Professors.Add(new ProfessorDTO(professor));
        }

        public void UpdateGrade(Student std)
        {
            Grades.Clear();
            foreach (Grade grade in headDao.getGradesForStudent(std)) Grades.Add(new GradeDTO(grade, grade.subject));
        }

        public void UpdateSubject(Student std)
        {
            Subjects.Clear();
           // foreach (Subject subject in headDao.GetAllSubjectsHead()) Subjects.Add(new SubjectDTO(subject));
            foreach (Subject subject in headDao.getFailedSubjects(std)) Subjects.Add(new SubjectDTO(subject));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            if (studentDTO.IsValid)
            {
                    if(comboBoxStatus.SelectedItem == StatusB)
                    {
                        studentDTO.StudentStatus = Student.Status.B;
                    }
                    else
                    {
                        studentDTO.StudentStatus = Student.Status.S;
                    }
                    headDao.UpdateStudentHead(studentDTO.ToStudent());
                    Close();
                    MessageBox.Show("Student updated!");
            }
            else
            {
                    MessageBox.Show("Student can not be created. Not all fields are valid.");
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student is not updated!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           // SubjectDTO subjectDTO = new SubjectDTO(gradeDTO.subject);

            GradeDTO gradeDTO = dataGridPassed.SelectedItem as GradeDTO;   //obrisemo ocenu
            



            if (gradeDTO != null)
            {
                 // SubjectDTO subjectDTO= new SubjectDTO(gradeDTO.Subject);  //OVAJ DIO MI UOPSTE NIJE TREBAO JER BRISANJEM OCJENE, U 72.LINIJI KODA  SAMO CE SE POKUOITI veza STUDENTSUBJECT KOJA NEMA OCJENU :(
                 // Subjects.Add(subjectDTO);
                 CancelGrade cancelGrade = new CancelGrade(headDao, gradeDTO);
                if (subjectDTO != null)
                {
                    Subjects.Add(subjectDTO);
                }
                 cancelGrade.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didnt select grade to cancel!");
            }

        }
        
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudentToSubject addStudentToSubject = new AddStudentToSubject(headDao, studentDTO, Subjects);
            addStudentToSubject.ShowDialog();
        }


        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            SubjectDTO subjectDTO = dataGridFiled.SelectedItem as SubjectDTO;
            DeleteStudentFromSubject deleteStudentFromSubject = new DeleteStudentFromSubject(headDao, subjectDTO, studentDTO);
            deleteStudentFromSubject.ShowDialog();
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {

            SubjectDTO subjectDto = dataGridFiled.SelectedItem as SubjectDTO;  

            if (subjectDto != null)
            {
               // AddGrade addGrade = new AddGrade(headDao, studentDTO, subjectDto);
               // addGrade.ShowDialog();
               AddGradeForStudent addGradeForStudent= new AddGradeForStudent(headDao, studentDTO, subjectDto);
                addGradeForStudent.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didnt select subject to pass!");
            }


        }




    }
}
