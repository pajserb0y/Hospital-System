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
    /// Interaction logic for FeedbackForm.xaml
    /// </summary>
    public partial class FeedbackForm : Window
    {
        public FeedbackForm(Examination e, int pid)
        {
            InitializeComponent();

            if (e != null)
            {
                date.Content = e.Date.ToString("dd/MMM/yyyy") + " ?";
                doctor.Content = e.Doctor.FirstName + " " + e.Doctor.LastName + " ?";
            }
            else
            {
                rateExam.Visibility = Visibility.Collapsed;
                rateDoctor.Visibility = Visibility.Collapsed;
                date.Visibility = Visibility.Collapsed;
                doctor.Visibility = Visibility.Collapsed;
                cbRateDoctor.Visibility = Visibility.Collapsed;
                cbRateExam.Visibility = Visibility.Collapsed;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
