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
    /// Interaction logic for AddProfessor.xaml
    /// </summary>
    public partial class AddProfessor : Window
    {
        private HeadDao controller;
        public ProfessorDTO professorDTO {  get; set; }
        public AdressDTO adressDTO { get; set; }
        public AddProfessor(HeadDao cnt)
        {
            InitializeComponent();
            controller = cnt;
            DataContext = this;
            adressDTO = new AdressDTO();
            professorDTO = new ProfessorDTO(adressDTO);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (professorDTO.IsValid && adressDTO.IsValid)
            {
                controller.AddProfessorHead(professorDTO.ToProfessor());
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
            MessageBox.Show("Professor is not added");
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}
