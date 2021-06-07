using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for NewDoctor.xaml
    /// </summary>
    public partial class NewDoctor : Window
    {
        public NewDoctor()
        {
            InitializeComponent();
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtJmbg.Text == "" || txtTelephone.Text == "" || txtSpecialization.Text == "" || txtAdress.Text == "")
                MessageBox.Show("Each field is required.");
            else
                try
                {
                    if (Convert.ToInt64(txtJmbg.Text) < 100000000000 || Convert.ToInt64(txtJmbg.Text) > 9999999999999 || txtJmbg.Text.Length != 13)
                        errorJmbg.Content = "JMBG must contain 13 digits";
                    else
                    {
                        Doctor doctor = new Doctor(DoctorStorage.getInstance().GenerateNewID(), txtFirstName.Text, txtLastName.Text, Convert.ToInt64(txtJmbg.Text), txtAdress.Text,
                                                   Convert.ToInt64(txtTelephone.Text), txtSpecialization.Text, default);
                        DoctorStorage.getInstance().Add(doctor);
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
