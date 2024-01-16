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
    /// Interaction logic for ShowStudents2.xaml
    /// </summary>
    public partial class ShowStudents2 : Window
    {
        HeadDao headDao;
        SubjectDTO subjectDTO1;
        SubjectDTO subjectDTO2;
        StudentDTO studentDTO;
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ShowStudents2(HeadDao cnt, SubjectDTO sub1, SubjectDTO sub2)
        {
            InitializeComponent();
            headDao = cnt;
            subjectDTO1 = sub1;
            subjectDTO2 = sub2;
            studentDTO = new StudentDTO();
            Students = new ObservableCollection<StudentDTO>();
            dataGridShowStudents2.ItemsSource = Students;

            UpdateStudents();
            
        }
        public void UpdateStudents()
        {

            Students.Clear();
     
            foreach (Student student in headDao.passedFailedSubjects(subjectDTO1.ToSubject(), subjectDTO2.ToSubject())) Students.Add(new StudentDTO(student));
        }
    }

}
