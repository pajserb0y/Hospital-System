using HospitalSystem.code.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for SecretarInitialWindow.xaml
    /// </summary>
    public partial class SecretarInitialWindow : Window
    {
        public SecretarInitialWindow()
        {
            //this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            buttonHelp.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/help.jpg"))));

            DataContext = new PatientViewModel(this);
        }
    }
}
