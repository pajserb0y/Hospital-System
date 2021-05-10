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
    /// Interaction logic for EditAlergen.xaml
    /// </summary>
    public partial class EditAlergen : Window
    {
        private string currentAlergen;
        private Patient currentPatient;
        public EditAlergen(Patient selectedPatient, string selectedAlergen)
        {
            currentPatient = selectedPatient;
            currentAlergen = selectedAlergen;
            InitializeComponent();
            this.Closed += new EventHandler(Window_Closed);
            txtSubstance.Text = selectedAlergen;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int indexOfAlergen = currentPatient.Alergens.IndexOf(currentAlergen);         
            currentPatient.Alergens.RemoveAt(indexOfAlergen);
            currentPatient.Alergens.Insert(indexOfAlergen, txtSubstance.Text);
            PatientsStorage.getInstance().Edit(currentPatient);
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
