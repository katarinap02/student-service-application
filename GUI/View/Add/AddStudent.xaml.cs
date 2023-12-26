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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        private HeadDao controller;
        public StudentDTO studentDTO {  get; set; }
        public AdressDTO adressDTO { get; set; }  
        
        public IndexDTO indexDTO { get; set; }
        
            
        public AddStudent(HeadDao cnt)
        {
            InitializeComponent();
            DataContext = this;
            controller = cnt;
            adressDTO = new AdressDTO();
            indexDTO = new IndexDTO();
            studentDTO = new StudentDTO(adressDTO, indexDTO);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
             //if (comboBoxFinancing.SelectedItem.Equals("B"))
            studentDTO.StudentStatus = Student.Status.B;
            // else
            // studentDTO.StudentStatus = Student.Status.S;
            if (studentDTO.IsValid && adressDTO.IsValid && indexDTO.IsValid)
            {
                controller.AddStudentHead(studentDTO.ToStudent());
                MessageBox.Show("Student added!");
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
            MessageBox.Show("Student deleted!");
        }









    }
}
