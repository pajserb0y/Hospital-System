using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Window
    {
        private Doctor currentDoctor;
        public EditDoctor(Doctor selectedDoctor)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();

            currentDoctor = selectedDoctor;
            initializeSelectedDoctorDetails(selectedDoctor);
        }

        private void initializeSelectedDoctorDetails(Doctor selectedDoctor)
        {
            txtID.Text = selectedDoctor.Id.ToString();
            txtFirstName.Text = selectedDoctor.FirstName;
            txtLastName.Text = selectedDoctor.LastName;
            txtJmbg.Text = selectedDoctor.Jmbg.ToString();
            txtAdress.Text = selectedDoctor.Adress;
            txtTelephone.Text = selectedDoctor.Telephone.ToString();
            txtSpecialization.Text = selectedDoctor.Specialization;
            //txtUsername.Text = selectedDoctor.Username;
            //txtPassword.Text = selectedDoctor.Password;

            if (selectedDoctor.FreeDays == null)
                selectedDoctor.FreeDays = new ObservableCollection<DateTime>();
            freeDaysList.ItemsSource = selectedDoctor.FreeDays;
        }
        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DoctorStorage.getInstance().Edit(new Doctor(currentDoctor.Id, txtFirstName.Text, txtLastName.Text, Convert.ToInt64(txtJmbg.Text), txtAdress.Text,
                Convert.ToInt64(txtTelephone.Text), txtSpecialization.Text, currentDoctor.FreeDays));
            DoctorStorage.getInstance().serialize();
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            //JobStorage.getInstance().serialize();
            this.Close();
        }
    }
}
