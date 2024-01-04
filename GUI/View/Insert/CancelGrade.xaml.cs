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
    /// Interaction logic for CancelGrade.xaml
    /// </summary>
    public partial class CancelGrade : Window
    {
       // StudentDTO _studentDTO;
        HeadDao _headDao;
        GradeDTO _gradeDTO;
        //SubjectDTO _subjectDTO;
        public CancelGrade(HeadDao cnt, GradeDTO gradeDto)
        {
            InitializeComponent();
            _headDao = cnt;
            _gradeDTO = gradeDto;
            
        }



        private void YesButton_Click(object sender, RoutedEventArgs e)
        {

          //  SubjectDTO subjectDTO = new SubjectDTO(_gradeDTO.GradeC);
          //dodaj vezu  u Student Subject ako vec nisi, subjecta i studenta cija je bila ocjena jer ce mi inace stajati taj predmet kao moguci za dodavanje u onoj opciji AddStudentForSubject

            _headDao.RemoveGradeHead(_gradeDTO.Id);
            MessageBox.Show("Grade is canceled!");
            Close();

            //  YesButton.Background = Brushes.Blue; 
            // NoButton.Background = Brushes.Gray;  
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Grade is not canceled!");
            Close();


            // Change color when No button is clicked
            //  NoButton.Background = Brushes.Blue;   
            // YesButton.Background = Brushes.Gray; 

        }

    }
}
