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
    /// Interaction logic for ChartDetails.xaml
    /// </summary>
    /// 
    public delegate void ChangeTextHandler();

    public partial class ChartDetails : Window
    {
        Patient currentPatient;
        public event ChangeTextHandler ChangeTextEvent;

        public ChartDetails(Patient patient)
        {
            InitializeComponent();
            setButtonImages();
            this.Closed += new EventHandler(Window_Closed);
            currentPatient = patient;
            initializeSelectedPatientDetails(currentPatient);
        }

        private void initializeSelectedPatientDetails(Patient selectedPatient)
        {
            txtID.Text = selectedPatient.Id.ToString();
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdress.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
            txtEmail.Text = selectedPatient.Email;
            _ = selectedPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = selectedPatient.Guest = false;
            dpBirth.SelectedDate = selectedPatient.BirthDate;

            if (selectedPatient.MarriageStatus == "Married")
                cbMarriage.SelectedIndex = 0;
            if (selectedPatient.MarriageStatus == "Unmarried")
                cbMarriage.SelectedIndex = 1;
            if (selectedPatient.MarriageStatus == "Divorced")
                cbMarriage.SelectedIndex = 2;
            if (selectedPatient.MarriageStatus == "Widow(er)")
                cbMarriage.SelectedIndex = 3;

            txtSoc.Text = selectedPatient.SocNumber.ToString();
            txtCity.Text = selectedPatient.City;
            txtCountry.Text = selectedPatient.Country;

            if (selectedPatient.Alergens == null)
                selectedPatient.Alergens = new ObservableCollection<string>();
            listViewAlergens.ItemsSource = selectedPatient.Alergens;

            if (selectedPatient.WorkHistory == default)
                selectedPatient.WorkHistory = new ObservableCollection<Job>();
            dgJob.ItemsSource = selectedPatient.WorkHistory;
        }

        private void setButtonImages()
        {
            buttonAddJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
            buttonEditJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/edit.jpg"))));
            buttonDeleteJob.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/delete.jpg"))));
            buttonAddAlergen.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../Images/add.jpg"))));
        }

        private void Button_Save_Changes(object sender, RoutedEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(currentPatient.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, false,
                "", "", (DateTime)dpBirth.SelectedDate, cbMarriage.SelectedIndex == -1 ? "" : cbMarriage.SelectedValue.ToString(), Convert.ToInt64(txtSoc.Text),
                txtCity.Text, txtCountry.Text, currentPatient.Alergens, currentPatient.WorkHistory));
            PatientsStorage.getInstance().serialize();
            this.Close();
        }
        private void Button_Add_Job(object sender, RoutedEventArgs e)
        {
            NewJob newJob = new NewJob(currentPatient);
            newJob.Show();
        }
        private void Button_Edit_Job(object sender, RoutedEventArgs e)
        {
            NewJob editJob = new NewJob(currentPatient, (Job)dgJob.SelectedItem);
            editJob.Show();
            (dgJob.ItemContainerGenerator.ContainerFromItem(dgJob.SelectedItem) as DataGridRow).IsSelected = false;
        }
        private void Button_Delete_Job(object sender, RoutedEventArgs e)
        {
            currentPatient.WorkHistory.Remove((Job)dgJob.SelectedItem);
        }

        private void Button_Add_Alergen(object sender, RoutedEventArgs e)
        {
            if (txtSubstance.Text != "")
                currentPatient.Alergens.Add(txtSubstance.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StrikeEvent();
        }

        private void StrikeEvent()
        {
            if (ChangeTextEvent != null)
            {
                ChangeTextEvent();
            }
        }
    }
}
