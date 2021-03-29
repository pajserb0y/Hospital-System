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

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient("p123", txtIme.Text, txtPrezime.Text, Convert.ToInt32(txtJmbg.Text), txtAdresa.Text, Convert.ToInt32(txtTel.Text));

            PatientsStorage.getInstance().Save(patient);

        }
    }
}
