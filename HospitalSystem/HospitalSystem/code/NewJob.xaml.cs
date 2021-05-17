using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NewJob.xaml
    /// </summary>
    public partial class NewJob : Window
    {
        Patient currentPatient;
        public NewJob(Patient selectedPatient)
        {
            currentPatient = selectedPatient;
            InitializeComponent();
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
<<<<<<< HEAD
        {            
=======
        {
>>>>>>> BugFixOperationVol2
            currentPatient.WorkHistory.Add(new Job(txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text));
            this.Close();
        }
    }
}
