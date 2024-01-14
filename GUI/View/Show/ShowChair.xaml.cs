using CLI.DAO;
using CLI.Observer;
using GUI.DTO;
using GUI.View.Insert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GUI.View.Show
{
    /// <summary>
    /// Interaction logic for ShowChair.xaml
    /// </summary>
    public partial class ShowChair : Window
    {
        public static RoutedCommand NewCommand = new RoutedCommand();

        public ObservableCollection<ChairDTO> Chairs { get; set; }

        ChairDTO chairDTO;
        HeadDao headDao;
        public ShowChair(HeadDao contr)
        {
            InitializeComponent();

            headDao = contr;
            Chairs = new ObservableCollection<ChairDTO>();
            chairDTO = new ChairDTO();
        }



        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Subject is not updated!");
        }

        private void Button_AddProfessortoChair(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
