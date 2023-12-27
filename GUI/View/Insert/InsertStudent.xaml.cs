using CLI.DAO;
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
    /// Interaction logic for InsertStudent.xaml
    /// </summary>
    public partial class InsertStudent : Window
    {
        StudentDTO studentDTO;
        HeadDao headDao;
        public InsertStudent(HeadDao contr, StudentDTO std)
        {
            InitializeComponent();
            headDao = contr;
            studentDTO = new StudentDTO(std);
            DataContext= studentDTO;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            headDao.UpdateStudentHead(studentDTO.ToStudent());
            Close();
            MessageBox.Show("Student updated!");
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student is not updated!");
        }
    }
}
