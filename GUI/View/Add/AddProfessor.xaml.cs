using CLI.DAO;
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

namespace GUI.View.Add
{
    /// <summary>
    /// Interaction logic for AddProfessor.xaml
    /// </summary>
    public partial class AddProfessor : Window
    {
        private HeadDao contoller;
        public DTO.ProfessorDTO professorDTO {  get; set; }
        public DTO.AdressDTO adressDTO { get; set; }
        public AddProfessor(HeadDao cnt)
        {
            InitializeComponent();
            this.contoller = cnt;
            DataContext = this;
            adressDTO = new DTO.AdressDTO();
            professorDTO = new DTO.ProfessorDTO(adressDTO);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            contoller.AddProfessorHead(professorDTO.ToProfessor());
            MessageBox.Show("Professor added!");
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
