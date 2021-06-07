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
            Doctor doctor = new Doctor(DoctorStorage.getInstance().GenerateNewID(), txtFirstName.Text, txtLastName.Text, Convert.ToInt64(txtJmbg.Text), txtAdress.Text, 
                Convert.ToInt64(txtTelephone.Text), txtSpecialization.Text, default);
            DoctorStorage.getInstance().Add(doctor);
            this.Close();
        }
    }
}
