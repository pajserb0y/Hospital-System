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

<<<<<<< HEAD
           // string JSONresult = JsonConvert.DeserializeObject(jsonString);
            string path = @"C:\Users\Marko\Desktop\Hospital-System\HospitalSystem\HospitalSystem\HospitalSystem.json";
            using (var tr = new StreamReader(path, true))
=======
            List<string> lines = new List<string>();
            //string jsonresult = jsonconvert.deserializeobject(jsonstring);
            string path = @"e:\fax\projekti\sims\hospital-system\hospitalsystem.json";
            using (JsonTextReader reader = new JsonTextReader(new StreamReader(path, true)))
>>>>>>> 1154d10372273d6bfcd5652bea75be4900fef252
            {

                // while(!tr.endofstream)
                //txtlist.text = tr.readtoend().tostring();
                while (reader.Read())
                {
                    if (reader.Value != null)
                        lines.Add(reader.TokenType + " " + reader.Value);
                    // console.writeline("token: {0}, value: {1}", reader.tokentype, reader.value);
                    //txtlist.text = "token: " + reader.tokentype + " value: " + reader.value;
                    else
                    {
                        txtList.Text = "token: " + reader.TokenType;
                    }
                }
                txtList.Text = string.Join(Environment.NewLine, lines);
                //    //string JSONresult = JsonConvert.DeserializeObject(tr);

                //    //tr.Close();
            }


            //    using (StreamReader r = new StreamReader(@"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json"))
            //{
            //    string json = r.ReadToEnd();
            //    List<Patient> items = JsonConvert.DeserializeObject<List<Patient>>(json);
            //    r.Close();
            //}

            //DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            //dataGridView.DataSource = dataTable; 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewPatient np = new NewPatient();
            np.Show();
            //this.Close();
        }
    }
}
