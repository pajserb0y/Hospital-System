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
            //this.btSave_Click += new EventHandler(DataGridCellEditEndingEventArgs);
            p = selectedPatient;
               
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdresa.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(p.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text), txtAdresa.Text, Convert.ToInt64(txtTel.Text)));
            this.Close();
        }
    }
}
