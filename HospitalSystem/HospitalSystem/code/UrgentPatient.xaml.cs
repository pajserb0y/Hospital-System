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
    /// Interaction logic for UrgentPatient.xaml
    /// </summary>
    public partial class UrgentPatient : Window
    {
        private long jmbg = 0;
        private long tel = 0;
        public UrgentPatient()
        {
            InitializeComponent();

            cbDoctor.ItemsSource = DoctorStorage.getInstance().GetAll();
            cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();
            txtTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(txtJmbg.Text != "")
                jmbg = Convert.ToInt64(txtJmbg.Text);
            if (txtTel.Text != "")
                tel = Convert.ToInt64(txtTel.Text);


            Patient patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), txtIme.Text, txtPrezime.Text, jmbg,
                (char)((bool)rbF.IsChecked ? Convert.ToChar(rbF.Content) : Convert.ToChar(rbM.Content)), txtAdresa.Text, tel, txtEmail.Text, cbGuest.IsChecked == true,
                "", "");
            PatientsStorage.getInstance().Save(patient);


            Examination exam = new Examination();
            exam.Id = ExaminationStorage.getInstance().GenerateNewID();
            exam.Patient = patient;
            exam.Doctor = (Doctor)cbDoctor.SelectedItem;
            exam.Room = (Room)cbRoom.SelectedItem;
            exam.Date = (DateTime)dp1.SelectedDate;
            exam.Time = Convert.ToDateTime(txtTime.Text);
            ExaminationStorage.getInstance().Add(exam);
            this.Close();
        }
    }
}
