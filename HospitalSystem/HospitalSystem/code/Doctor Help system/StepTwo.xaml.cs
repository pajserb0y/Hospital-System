using HospitalSystem.code.ViewModel;
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
        public StepTwo(Doctor doc)
        {
            InitializeComponent();
            DataContext = new StepTwoViewModel(doc, this);
        }
    }
}
