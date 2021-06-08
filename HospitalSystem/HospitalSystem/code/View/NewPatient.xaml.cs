using HospitalSystem.code.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public NewPatient(PatientViewModel patientViewModel)
        {
            InitializeComponent();
            DataContext = new NewPatientViewModel(patientViewModel, this);
        }
    }
}
