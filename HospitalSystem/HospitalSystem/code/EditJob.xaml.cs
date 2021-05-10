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
    /// Interaction logic for EditJob.xaml
    /// </summary>
    public partial class EditJob : Window
    {
        private Job currentJob;
        private Patient currentPatient;
        public EditJob(Patient selectedPatient, Job selectedJob)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            currentPatient = selectedPatient;
            currentJob = selectedJob;
            initializeSelectedJobDetails(selectedJob);
        }

        private void initializeSelectedJobDetails(Job selectedJob)
        {
            txtName.Text = selectedJob.CompanyName;
            txtJob.Text = selectedJob.Position;
            txtReg.Text = selectedJob.RegisterNumber.ToString();
            txtAct.Text = selectedJob.ActivityCode;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int indexOfJob = currentPatient.WorkHistory.IndexOf(currentJob);
            currentPatient.WorkHistory.RemoveAt(indexOfJob);
            currentPatient.WorkHistory.Insert(indexOfJob, new Job(txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text));
            this.Close();
        }
    }
}
