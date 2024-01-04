using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for InsertSubject.xaml
    /// </summary>
    public partial class InsertSubject : Window
    {
        SubjectDTO subjectDTO;
        HeadDao headDao;
        public InsertSubject(HeadDao contr, SubjectDTO sub)
        {
            InitializeComponent();
            
            headDao = contr;
            subjectDTO = new SubjectDTO(sub);

            DataContext = subjectDTO;
            if (sub.Semester == CLI.Model.Subject.Semester.Summer)
                comboBoxSemester.SelectedItem = SemesterSummer;
            else
                comboBoxSemester.SelectedItem =  SemesterWinter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (comboBoxSemester.SelectedItem == SemesterSummer)
            {
                subjectDTO.Semester = CLI.Model.Subject.Semester.Summer;
            }
            else
            {
                subjectDTO.Semester = CLI.Model.Subject.Semester.Winter;
            }
            //subjectDTO.Semester = Subject.Semester.Winter;
            headDao.UpdateSubjectHead(subjectDTO.ToSubject());
                Close();
                MessageBox.Show("Subject updated!");


            
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Subject is not updated!");
        }
    }
}
