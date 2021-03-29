using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
            InitializeComponent();

            ////string jsonString = File.ReadAllText(@"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json");
            //List<string> lines = new List<string>();
            //using (StreamReader reader = File.OpenText(filename))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        lines.Add(reader.ReadLine());
            //    }
            //}
           // List<Patient> list = JsonConvert.DeserializeObject < List<Patient>>(File.ReadAllText(@"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json"));
            //var list = JsonConvert.DeserializeObject<string>(File.ReadAllText(@"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json"));
         
          
           
            //string jsonString  = File.ReadAllText("HospitalSystem.json");
            //List <Patient> list = JsonSerializer.Deserialize<List<Patient>>(jsonString);

            //foreach (Patient p in list)
            //{
            //    txtList.Text = p.ToString();
            //}

           // string JSONresult = JsonConvert.DeserializeObject(jsonString);
            string path = @"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json";
            using (var tr = new StreamReader(path, true))
            {
                
               // while(!tr.EndOfStream)
                txtList.Text = tr.ReadToEnd().ToString();

                tr.Close();
            }
            //  FileLocation = 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewPatient np = new NewPatient();
            np.Show();
            //this.Close();
        }
    }
}
