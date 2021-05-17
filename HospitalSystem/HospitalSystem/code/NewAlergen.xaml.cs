using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for NewAlergen.xaml
    /// </summary>
    public partial class NewAlergen : Window
    {
        Patient currentPatient;
        public NewAlergen(Patient selectedPatient)
        {
            currentPatient = selectedPatient;
            InitializeComponent();
        }
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        { 
            currentPatient.Alergens.Add(txtSubstance.Text);
            this.Close();
        }        
    }
}
