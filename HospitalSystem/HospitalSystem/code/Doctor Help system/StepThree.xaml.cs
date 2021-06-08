﻿using System;
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
    /// Interaction logic for StepThree.xaml
    /// </summary>
    public partial class StepThree : Window
    {
        Doctor selectedDoctor;
        public StepThree(Doctor doc)
        {
            selectedDoctor = doc;
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DoctorInitialWindow dw = new DoctorInitialWindow(selectedDoctor);
            dw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            StepTwo st = new StepTwo(selectedDoctor);
            st.Show();
            this.Close();
        }
    }
}
