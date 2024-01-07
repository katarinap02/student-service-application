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
    /// Interaction logic for DeleteProfessorFromSubject.xaml
    /// </summary>
    public partial class DeleteProfessorFromSubject : Window
    {
        HeadDao _headDao;
        SubjectDTO _subjectDTO;
        ProfessorDTO _professorDTO;
        public DeleteProfessorFromSubject(HeadDao cnt, SubjectDTO subjectDTO, ProfessorDTO professorDTO)
        {
            InitializeComponent();
            _headDao = cnt;
            _subjectDTO = subjectDTO;
            _professorDTO = professorDTO;

        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _headDao.RemoveProfessorFromSubject(_subjectDTO.Id);
            MessageBox.Show("Student is deleted from subject!");
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student is not deleted from subject!");
            Close();
        }
    }
}
