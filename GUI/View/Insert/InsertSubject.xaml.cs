using CLI.DAO;
using CLI.Model;
using CLI.Observer;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View.Insert
{
    /// <summary>
    /// Interaction logic for InsertSubject.xaml
    /// </summary>
    public partial class InsertSubject : Window, IObserver
    {
        SubjectDTO subjectDTO;
        HeadDao headDao;
        int pomId;
        public InsertSubject(HeadDao contr, SubjectDTO sub)
        {
            InitializeComponent();
            
            headDao = contr;
            subjectDTO = new SubjectDTO(sub);


            DataContext = subjectDTO;
            headDao.observerSub.Subscribe(this);
            if (sub.Semester == CLI.Model.Subject.Semester.Summer)
                comboBoxSemester.SelectedItem = SemesterSummer;
            else
                comboBoxSemester.SelectedItem =  SemesterWinter;

            pomId = getProfId(subjectDTO.ToSubject());
            UnableButtons(pomId);

           
             textBoxProfessor.Text = getProfName(subjectDTO.ToSubject());
        }

        private void UnableButtons(int id)
        {
            if (id != -1)
            {
                buttonAddProfessor.IsEnabled = false;
                buttonRemoveProfessor.IsEnabled = true;
            }
            else
            {
                buttonAddProfessor.IsEnabled = true;
                buttonRemoveProfessor.IsEnabled = false;
            }
        }

        private string getProfName(Subject sb)
        {
            return headDao.getProfessorNameSurname(sb);
        }

        private int getProfId(Subject sb)
        {
            return headDao.getSubjectProfessorId(sb);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (comboBoxSemester.SelectedItem == SemesterSummer)
            {
                subjectDTO.Semester = CLI.Model.Subject.Semester.Summer;
            }
            else
            {
                subjectDTO.Semester = CLI.Model.Subject.Semester.Winter;
            }
            //subjectDTO.Semester = Subject.Semester.Winter;
            headDao.UpdateSubjectHead(subjectDTO.ToSubject());
                Close();
                MessageBox.Show("Subject updated!");


            
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Subject is not updated!");
        }

        private void Button_AddProfessortoSubject(object sender, RoutedEventArgs e)
        {
            AddProfessorInSubject addProfessorInSubject= new AddProfessorInSubject(headDao, subjectDTO);
            addProfessorInSubject.ShowDialog();
            
        }

        private void Button_RemoveProfesssorfromSubject(object sender, RoutedEventArgs e)
        {
            DeleteProfessorInSubject deleteProfessorInSubject = new DeleteProfessorInSubject(headDao,subjectDTO);
            deleteProfessorInSubject.ShowDialog();
        }

        public void Update()
        {
             textBoxProfessor.Text = getProfName(subjectDTO.ToSubject());
           // headDao.UpdateSubjectHead(subjectDTO.ToSubject());
            pomId = getProfId(subjectDTO.ToSubject());
            UnableButtons(pomId);
        }
    }
}
