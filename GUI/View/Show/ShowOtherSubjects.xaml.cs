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
    /// Interaction logic for ShowOtherSubjects.xaml
    /// </summary>
    public partial class ShowOtherSubjects : Window
    {

        HeadDao headDao;
        SubjectDTO subjectDTO;
        public ObservableCollection<SubjectDTO> Subjects { get; set; }

        public ShowOtherSubjects(HeadDao cnt, SubjectDTO subDTO)
        {
            InitializeComponent();

            headDao = cnt;
            subjectDTO = subDTO;
            Subjects = new ObservableCollection<SubjectDTO>();
            dataGridAnotherSubjects.ItemsSource = Subjects;

            
            UpdateSubjects();


        }
        public void UpdateSubjects()
        { 
        
             Subjects.Clear();
             foreach (Subject subject in headDao.anotherSubjects(subjectDTO.ToSubject())) Subjects.Add(new SubjectDTO(subject));
          
        }

        private void Button_ClickShowStudents_1(object sender, RoutedEventArgs e)
        {
            SubjectDTO subject2DTO = dataGridAnotherSubjects.SelectedItem as SubjectDTO;
            ShowStudents1 showStudents1 = new ShowStudents1(headDao,subjectDTO, subject2DTO);
            showStudents1.ShowDialog();
        }

        private void Button_ClickShowStudents_2(object sender, RoutedEventArgs e)
        {

            SubjectDTO subject2DTO = dataGridAnotherSubjects.SelectedItem as SubjectDTO;
            ShowStudents2 showStudents2= new ShowStudents2(headDao,subjectDTO, subject2DTO);
            showStudents2.ShowDialog();


        }
    }
}
