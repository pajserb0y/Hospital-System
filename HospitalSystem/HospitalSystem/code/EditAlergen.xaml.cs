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
        private Alergen currentAlergen;
        public EditAlergen(Alergen selectedAlergen)
        {
            currentAlergen = selectedAlergen;
            InitializeComponent();
            this.Closed += new EventHandler(Window_Closed);
            txtSubstance.Text = selectedAlergen.Substance;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Alergen alergen = new Alergen(currentAlergen.No, txtSubstance.Text, currentAlergen.PatientID);
            AlergenStorage.getInstance().Edit(alergen);
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            AlergenStorage.getInstance().serialize();
            this.Close();
        }
    }
}
