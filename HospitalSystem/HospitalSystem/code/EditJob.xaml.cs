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
        private Job p;
        public EditJob(Job selectedJob)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            p = selectedJob;

            txtNo.Text = selectedJob.No.ToString();
            txtName.Text = selectedJob.CompanyName;
            txtJob.Text = selectedJob.Position;
            txtReg.Text = selectedJob.RegisterNumber.ToString();
            txtAct.Text = selectedJob.ActivityCode;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            JobStorage.getInstance().serialize();
            this.Close();
        }
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            JobStorage.getInstance().Edit(new Job(p.No, txtName.Text, txtJob.Text, Convert.ToInt32(txtReg.Text), txtAct.Text, p.PID));
            this.Close();
        }
    }
}
