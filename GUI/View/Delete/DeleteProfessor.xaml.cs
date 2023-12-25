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
        ProfessorDTO _professorDTO;
        HeadDao _headDao;
        

        public DeleteProfessor(HeadDao cnt,   ProfessorDTO profDTO)
        {
            InitializeComponent();
            _headDao = cnt;
            _professorDTO = profDTO;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            

            Close();
            MessageBox.Show("Professor is not deleted!");
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if(_headDao.RemoveProfessorHead(_professorDTO.Id))
            {
                MessageBox.Show("Professor is  deleted!");
                Close();
            }
            else
            {
                MessageBox.Show("Professor can't be deleted!");
                Close();
            }
             // MessageBox.Show("Professor is  deleted!");
        }
    }
    
}
