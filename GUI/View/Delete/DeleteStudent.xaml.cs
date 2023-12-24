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

namespace GUI.View.Delete
{
    /// <summary>
    /// Interaction logic for DeleteStudent.xaml
    /// </summary>
    public partial class DeleteStudent : Window
    {
        public DeleteStudent(StudentDTO studentDTO)
        {
            InitializeComponent();
        }

       

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Professor is not deleted!");
          //  YesButton.Background = Brushes.Blue; 
           // NoButton.Background = Brushes.Gray;  
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor is not deleted!");
            // Change color when No button is clicked
          //  NoButton.Background = Brushes.Blue;   
           // YesButton.Background = Brushes.Gray; 
        }

    }
}
