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

namespace GUI
{
    /// <summary>
    /// Interaction logic for DeleteProfessor.xaml
    /// </summary>
    public partial class DeleteProfessor : Window
    {

        private HeadDao contoller;
        public ProfessorDTO professorDTO { get; set; }

        public DeleteProfessor(HeadDao cnt,   ProfessorDTO profDTO)
        {
            InitializeComponent();
            this.contoller = cnt;
            DataContext = this;
            professorDTO = profDTO;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Professor is not deleted!");
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor is  deleted!");
        }
    }
    
}
