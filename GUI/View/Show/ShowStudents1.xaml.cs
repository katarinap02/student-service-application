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

namespace GUI.View.Show
{
    /// <summary>
    /// Interaction logic for ShowStudents1.xaml
    /// </summary>
    /// 

    public partial class ShowStudents1 : Window
    {

        HeadDao headDao;
        SubjectDTO subject1DTO;
        SubjectDTO subject2DTO;
        StudentDTO studentDTO;
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ShowStudents1(HeadDao cnt,SubjectDTO sub1, SubjectDTO sub2)
        {
            InitializeComponent();

            headDao = cnt;
            subject1DTO = sub1;
            subject2DTO = sub2;
            studentDTO = new StudentDTO();
            Students = new ObservableCollection<StudentDTO>();
            dataGridShowStudents1.ItemsSource = Students;
            UpdateStudents();



        }
        public void UpdateStudents()
        {

             Students.Clear();
           // foreach (Subject subject in headDao.anotherSubjects(subjectDTO.ToSubject())) Subjects.Add(new SubjectDTO(subject));
             foreach (Student student in headDao.studentsfrombothSubjects(subject1DTO.ToSubject(), subject2DTO.ToSubject())) Students.Add(new StudentDTO(student));
        }
    }
}
