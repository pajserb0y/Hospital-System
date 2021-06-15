using HospitalSystem.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void txbSubmit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            checkLogInDetails();
        }

        private void checkLogInDetails()
        {
            if (txtUsername.Text == "admin" && txtPassword.Password == "admin")
            {
                SecretarInitialWindow sek = new SecretarInitialWindow();
                sek.Show();
                this.Close();
            }
            else if (txtPassword.Password == "lekar")
            {
                string[] FullName = txtUsername.Text.ToLower().Split(" ");
                foreach(Doctor doc in DoctorStorage.getInstance().GetAll())
                {
                    if(FullName[0] == doc.FirstName.ToLower() && FullName[1] == doc.LastName.ToLower())
                    {
                        //HelpWizard hw = new HelpWizard(doc);
                        //hw.Show();
                        //this.Close();
                        DoctorInitialWindow dw = new DoctorInitialWindow(doc);
                        dw.Show();
                        this.Close();
                    }
                }


            }
            else if (txtUsername.Text == "pacijent" && txtPassword.Password == "pacijent")
            {
                PatientInitialWindow patientWindow = new PatientInitialWindow();
                patientWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show("Incorrect username or password!");
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (txtPassword.IsFocused)
                if (e.Key == Key.Return)
                    checkLogInDetails();
            if (txtUsername.IsFocused)
                if (e.Key == Key.Return)
                    txtPassword.Focus();        
        }
    }
}
