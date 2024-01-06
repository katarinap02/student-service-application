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
    /// Interaction logic for AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window
    {
        public DTO.GradeDTO gradeDTO { get; set; }
        private CLI.DAO.HeadDao controller;
        public StudentDTO studentDTO { get; set; }  
        public SubjectDTO subjectDTO { get; set; }
        

        public AddGrade(HeadDao cnt, StudentDTO std, SubjectDTO sb)
        {
            InitializeComponent();
            DataContext = this;
            controller = cnt;
            studentDTO = new StudentDTO(std);
            subjectDTO = new SubjectDTO(sb);
            gradeDTO = new GradeDTO(studentDTO, subjectDTO);
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                //controller.AddGradeHead(GradeDTO.ToGrade());
                MessageBox.Show("Grade added!");
                Close();



        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Grade not added!");
        }

    }
}
