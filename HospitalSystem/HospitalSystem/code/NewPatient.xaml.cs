using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for NewPatient.xaml
    /// </summary>
    public partial class NewPatient : Window
    {
        public NewPatient()
        {
            InitializeComponent();
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Patient patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdresa.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, cbGuest.IsChecked == true,
                txtUsername.Text, txtPassword.Text, default(DateTime), "", 0, "", "");
            PatientsStorage.getInstance().Save(patient);
            this.Close();
        }
    }
}
