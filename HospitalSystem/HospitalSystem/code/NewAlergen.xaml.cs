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
    /// Interaction logic for NewAlergen.xaml
    /// </summary>
    public partial class NewAlergen : Window
    {
        int patientID;
        public NewAlergen(int pid)
        {
            patientID = pid;
            InitializeComponent();
        }
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Alergen alergen = new Alergen(AlergenStorage.getInstance().GenerateNewID(patientID), txtSubstance.Text, patientID);
            AlergenStorage.getInstance().Save(alergen);
            this.Close();
        }
    }
}
