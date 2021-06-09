using HospitalSystem.code.ViewModel;
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
        public NewJob(Patient selectedPatient)
        {
            InitializeComponent();
            DataContext = new NewJobViewModel(selectedPatient, this, 1);
        }

        public NewJob(Patient selectedPatient, Job selectedJob)
        {
            InitializeComponent();
            DataContext = new NewJobViewModel(selectedPatient, this, 2, selectedJob);
        }        
    }
}
