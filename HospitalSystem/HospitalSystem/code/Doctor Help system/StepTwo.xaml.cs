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
    /// Interaction logic for StepTwo.xaml
    /// </summary>
    public partial class StepTwo : Window
    {
        Doctor selectedDoctor;
        public StepTwo(Doctor doc)
        {
            InitializeComponent();
            selectedDoctor = doc;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DoctorInitialWindow dw = new DoctorInitialWindow(selectedDoctor);
            dw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StepOne so = new StepOne(selectedDoctor);
            so.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StepThree st = new StepThree(selectedDoctor);
            st.Show();
            this.Close();
        }
    }
}
