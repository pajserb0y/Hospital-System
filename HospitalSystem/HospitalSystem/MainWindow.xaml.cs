using HospitalSystem.code;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSekretar_Click(object sender, RoutedEventArgs e)
        {
            SecretarInitialWindow sek = new SecretarInitialWindow();
            sek.Show();
            this.Close();
        }

        private void btnUpravnik_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLekar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPacijent_Click(object sender, RoutedEventArgs e)
        {
            PatientInitialWindow patientWindow = new PatientInitialWindow();
            patientWindow.Show();
            this.Close();
        }

        private void btnDoctor_Click(object sender, RoutedEventArgs e)
        {
            HelpWizard hw = new HelpWizard();
            hw.Show();
            this.Close();
            //DoctorInitialWindow dw = new DoctorInitialWindow();
            //dw.Show();
            //this.Close();
        }
    }
}
