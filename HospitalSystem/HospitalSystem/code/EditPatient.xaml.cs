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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        private Patient p;
        public EditPatient(Patient selectedPatient)
        {
            InitializeComponent();
            p = selectedPatient;

            txtID.Text = selectedPatient.Id.ToString();
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdress.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
            txtEmail.Text = selectedPatient.Email;
            _ = selectedPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = selectedPatient.Guest == true ? cbGuest.IsChecked = true : cbGuest.IsChecked = false;
            txtUsername.Text = selectedPatient.Username;
            txtPassword.Text = selectedPatient.Password;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(p.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, cbGuest.IsChecked == true,
                txtUsername.Text, txtPassword.Text));
            this.Close();
        }
    }
}
