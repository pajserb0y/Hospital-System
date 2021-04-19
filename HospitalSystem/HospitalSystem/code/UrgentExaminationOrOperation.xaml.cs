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
        ListCollectionView collectionView = new ListCollectionView(DoctorStorage.getInstance().GetAll());
        public UrgentPatient()
        {
            InitializeComponent();
            cbPatient.ItemsSource = PatientsStorage.getInstance().GetAll();
            cbRoom.ItemsSource = RoomStorage.getInstance().GetAll();
            txtTime.Text = DateTime.Now.ToString("HH:mm");


            collectionView.Filter = (e) =>
            {
                Doctor temp = e as Doctor;
                foreach(Examination ex in ExaminationStorage.getInstance().GetAll())
                    if(ex.IsOperation == false && ex.Doctor != temp)
                        return true;
                return false;
            };
            cbDoctor.ItemsSource = collectionView;
            cbDoctor.SelectedIndex = -1;
        }

        private void txbSave_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Patient patient = new Patient();
            if (cbPatient.SelectedItem == null)
            {
                patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), "", "", 0, default, "", 0, "", true, "", "", default(DateTime), "", 0, "", "");
                PatientsStorage.getInstance().Add(patient);
            }
            else
                patient = (Patient)cbPatient.SelectedItem;

            Examination exam = new Examination();
            exam.Id = ExaminationStorage.getInstance().GenerateNewID();
            exam.Patient = patient;
            exam.Doctor = (Doctor)cbDoctor.SelectedItem;
            exam.Room = (Room)cbRoom.SelectedItem;
            exam.Date = (DateTime)dpTime.SelectedDate;
            exam.Time = Convert.ToDateTime(txtTime.Text);
            ExaminationStorage.getInstance().checkExamination(exam);
            ExaminationStorage.getInstance().Add(exam);

            this.Close();
        }
    }
}
