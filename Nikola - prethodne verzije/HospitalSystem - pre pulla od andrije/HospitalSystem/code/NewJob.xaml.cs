using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalSystem.code
{
    /// <summary>
    /// Interaction logic for NewJob.xaml
    /// </summary>
    public partial class NewJob : Window
    {
        private Patient currentPatient;
        private Job currentJob;
        private int calledConstructor;

        public NewJob(Patient selectedPatient)
        {
            currentPatient = selectedPatient;
            InitializeComponent();
            calledConstructor = 1;
        }

        public NewJob(Patient selectedPatient, Job selectedJob)
        {
            InitializeComponent();
            currentPatient = selectedPatient;
            currentJob = selectedJob;
            initializeSelectedJobDetails(selectedJob);
            calledConstructor = 2;
        }

        private void initializeSelectedJobDetails(Job selectedJob)
        {
            txtName.Text = selectedJob.CompanyName;
            txtJob.Text = selectedJob.Position;
            txtReg.Text = selectedJob.RegisterNumber.ToString();
            txtAct.Text = selectedJob.ActivityCode;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtAct.Text == "" || txtName.Text == "" || txtJob.Text == "" || txtReg.Text == "")
                MessageBox.Show("Each field is required.");
            else
            {
                try
                {
                    if (Convert.ToInt64(txtReg.Text) < 1000 || Convert.ToInt64(txtReg.Text) > 9999 || txtReg.Text.Length != 4)
                        errorRegister.Content = "Register number must contain 4 digits";
                    else if (txtAct.Text.Length != 4)
                        errorActivity.Content = "Activity code must contain 4 characters";
                    else
                    {
                        if (calledConstructor == 1)
                        {
                            currentPatient.WorkHistory.Add(new Job(txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text));
                            this.Close();
                        }
                        else
                        {
                            int indexOfJob = currentPatient.WorkHistory.IndexOf(currentJob);
                            currentPatient.WorkHistory.RemoveAt(indexOfJob);
                            currentPatient.WorkHistory.Insert(indexOfJob, new Job(txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text));
                            this.Close();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Register number field must contain only digits!");
                }
            }
        }
    }
}
