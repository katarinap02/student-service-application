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
    /// Interaction logic for DeleteStudentFromSubject.xaml
    /// </summary>
    public partial class DeleteStudentFromSubject : Window
    {
        HeadDao _headDao;
        SubjectDTO _subjectDTO;
        StudentDTO _studentDTO;
        public DeleteStudentFromSubject(HeadDao cnt, SubjectDTO subjectDTO, StudentDTO studentDTO)
        {
            InitializeComponent();
            _headDao = cnt;
            _subjectDTO = subjectDTO;
            _studentDTO = studentDTO;

        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _headDao.RemoveStudentSubjectHead(_studentDTO.Id, _subjectDTO.Id );
            MessageBox.Show("Student is  deleted from subject!");
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Student is not  deleted from subject!");
            Close();
        }
    }
}
