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

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for AddGradeForStudent.xaml
    /// </summary>
    public partial class AddGradeForStudent : Window
    {
       
        public GradeDTO gradeDTO { get; set; }
        private HeadDao controller;
        public StudentDTO studentDTO { get; set; }
        public SubjectDTO subjectDTO { get; set; }


        public AddGradeForStudent(HeadDao cnt, StudentDTO std, SubjectDTO sub)
        {
            InitializeComponent();

            DataContext = this;
            controller = cnt;
            subjectDTO = new SubjectDTO(sub);
            studentDTO = new StudentDTO(std);
           gradeDTO = new GradeDTO( studentDTO, subjectDTO);


            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
          //  Grade grade= new Grade(studentDTO.ToStudent, subje);
            
            controller.MakeNewGradeHead(gradeDTO.ToGrade(), subjectDTO.ToSubject(), studentDTO.ToStudent());

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
