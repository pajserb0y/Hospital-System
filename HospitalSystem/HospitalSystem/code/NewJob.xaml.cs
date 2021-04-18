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
    /// Interaction logic for NewJob.xaml
    /// </summary>
    public partial class NewJob : Window
    {
        int patientID;
        public NewJob(int pid)
        {
            patientID = pid;
            InitializeComponent();
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Job job = new Job(JobStorage.getInstance().GenerateNewID(patientID), txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text, patientID);
            JobStorage.getInstance().Save(job);
            this.Close();
        }
    }
}
