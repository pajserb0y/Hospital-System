using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (txtIme.Text == "" || txtPrezime.Text == "" || txtJmbg.Text == "" || txtTel.Text == "")
                MessageBox.Show("First name, Last name, JMBG and Telephone fields are required.");
            else
                try
                {
                    if (Convert.ToInt64(txtJmbg.Text) < 100000000000 || Convert.ToInt64(txtJmbg.Text) > 9999999999999 || txtJmbg.Text.Length != 13)
                        errorJmbg.Content = "JMBG must contain 13 digits";
                    else
                    {
                        long jmbg = Convert.ToInt64(txtJmbg.Text);
                        long tel = Convert.ToInt64(txtTel.Text);
                        Patient patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), txtIme.Text, txtPrezime.Text, jmbg,
                            (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdresa.Text, tel, txtEmail.Text, false,
                            txtUsername.Text, txtPassword.Text, default(DateTime), "", 0, "", "", default, default);
                        PatientsStorage.getInstance().Add(patient);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("JMBG and Telephone field must contain only digits!");
                }
        }
    }
}
