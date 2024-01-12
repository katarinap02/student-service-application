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
    /// Interaction logic for DeleteProfessorInSubject.xaml
    /// </summary>
    public partial class DeleteProfessorInSubject : Window
    {
        HeadDao headDao;
        ProfessorDTO professorDTO;
        SubjectDTO subjectDTO;
        public DeleteProfessorInSubject(HeadDao cnt, SubjectDTO subDTO)
        {
            InitializeComponent();
            headDao = cnt;
            subjectDTO=subDTO;

        }


        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            headDao.RemoveProfessorFromSubject(subjectDTO.ToSubject());
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Professor is not deleted!");
        }
    }
}
