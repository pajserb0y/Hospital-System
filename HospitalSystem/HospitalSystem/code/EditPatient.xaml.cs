using NHibernate.Hql.Ast.ANTLR.Util;
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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        private Patient p;
        ListCollectionView collectionView = new ListCollectionView(JobStorage.getInstance().GetAll());
        ListCollectionView exams = new ListCollectionView(ExaminationStorage.getInstance().GetAll());

        public Action<object, MouseButtonEventArgs> TabControl_SelectionChanged { get; }

        public EditPatient(Patient selectedPatient)
        {
            this.Closed += new EventHandler(Window_Closed);
            InitializeComponent();
            p = selectedPatient;

            txtID.Text = selectedPatient.Id.ToString();
            txtIme.Text = selectedPatient.FirstName;
            txtPrezime.Text = selectedPatient.LastName;
            txtJmbg.Text = selectedPatient.Jmbg.ToString();
            txtAdress.Text = selectedPatient.Adress;
            txtTel.Text = selectedPatient.Telephone.ToString();
            txtEmail.Text = selectedPatient.Email;
            _ = selectedPatient.Gender == 'M' ? rbM.IsChecked = true : rbF.IsChecked = true;
            _ = selectedPatient.Guest == true ? cbGuest.IsChecked = true : cbGuest.IsChecked = false;
            txtUsername.Text = selectedPatient.Username;
            txtPassword.Text = selectedPatient.Password;
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

            //dgJob.ItemsSource = JobStorage.getInstance().GetAll();
            collectionView.Filter = (e) =>
            {
                Job temp = e as Job;
                if (temp.PID == p.Id)
                    return true;
                return false;
            };
            dgJob.ItemsSource = collectionView;

            exams.Filter = (e) =>
            {
                Examination temp = e as Examination;
                if (temp.Patient == p)
                    return true;
                return false;
            };
            dgExam.ItemsSource = exams;
            //(dgExam.ItemContainerGenerator.ContainerFromItem(dgExam.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan exam

            t3.Visibility = Visibility.Collapsed;
            t4.Visibility = Visibility.Collapsed;
            t5.Visibility = Visibility.Collapsed;
            t6.Visibility = Visibility.Collapsed;
            t7.Visibility = Visibility.Collapsed;
            t8.Visibility = Visibility.Collapsed;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PatientsStorage.getInstance().Edit(new Patient(p.Id, txtIme.Text, txtPrezime.Text, Convert.ToInt64(txtJmbg.Text),
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdress.Text, Convert.ToInt64(txtTel.Text), txtEmail.Text, cbGuest.IsChecked == true,
                txtUsername.Text, txtPassword.Text, (DateTime)dpBirth.SelectedDate, cbMarriage.SelectedValue.ToString(), Convert.ToInt64(txtSoc.Text), txtCity.Text, txtCountry.Text));
            this.Close();
        }
        private void txbAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewJob nj = new NewJob(p.Id);
            nj.Show();
        }
        private void txbEdit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditJob ej = new EditJob((Job)dgJob.SelectedItem);
            ej.Show();
            (dgJob.ItemContainerGenerator.ContainerFromItem(dgJob.SelectedItem) as DataGridRow).IsSelected = false;    //da prestane da bude selektovan job
        }
        private void txbDelete_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            JobStorage.getInstance().Delete((Job)dgJob.SelectedItem);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            JobStorage.getInstance().serialize();
            this.Close();
        }
        private void txbStart_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //TabItem tab = new TabItem();
            //tab.Header = dgExam.SelectedItem.ToString();
            //tab.Content = "raf";
            //tabControl.Items.Add(tab);
            //tab.IsSelected = true;
            //StartExamination se = new StartExamination((Examination)dgExam.SelectedItem);
            //se.Show();
            t3.Visibility = Visibility.Visible;
            t4.Visibility = Visibility.Visible;
            t5.Visibility = Visibility.Visible;
            t6.Visibility = Visibility.Visible;
            t7.Visibility = Visibility.Visible;
            t8.Visibility = Visibility.Visible;
        }

    }
}
