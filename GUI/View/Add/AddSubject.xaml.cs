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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View.Add
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    
    public partial class AddSubject : Window
    {

        public DTO.SubjectDTO subjectDTO {  get; set; }
        private CLI.DAO.HeadDao controller;

        public AddSubject(CLI.DAO.HeadDao cnt)
        {
            InitializeComponent();
            DataContext = this;
            controller = cnt;
            subjectDTO = new SubjectDTO();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

           // subjectDTO.Semester = Subject.Semester.Winter;
            if (subjectDTO.IsValid)
            {
                controller.AddSubjectHead(subjectDTO.ToSubject());
                MessageBox.Show("Subject added!");
                Close();
            }
            else
            {
                MessageBox.Show("Student can not be created. Not all fields are valid.");
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Subject not added!");
            
        }


    }
}
