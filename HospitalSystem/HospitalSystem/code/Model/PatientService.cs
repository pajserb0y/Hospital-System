using HospitalSystem.code.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace HospitalSystem.code.Model
{
    public class PatientService : DeleteChecker
    {
        public static ObservableCollection<Patient> GetAll()
        {
            return PatientCRUDMenager.getInstance().GetAll();
        }

        public static void Edit(Patient patient)
        {
            PatientCRUDMenager.getInstance().Edit(patient);
        }

        public static void Delete(Patient patient)
        {
            PatientCRUDMenager.getInstance().Delete(patient);
        }
        public static void Add(Patient patient)
        {
            PatientCRUDMenager.getInstance().Add(patient);
        }

        public static int GenerateNewID()
        {
            ObservableCollection<Patient> patients = PatientCRUDMenager.getInstance().GetAll();
            return ((patients.Count - 1) == -1) ? 1 : patients[patients.Count - 1].Id + 1;
        }

        public static void openAddPatientWindow(PatientViewModel viewModel)
        {
            NewPatient np = new NewPatient(viewModel);
            np.ShowDialog();
        }

        public static void openEditPatientWindow(Patient SelectedItem)
        {
            EditPatient editPatient = new EditPatient(SelectedItem);
            editPatient.ShowDialog();
        }

        public static void deletePatient(Patient SelectedItem)
        {
            DeleteChecker foo = new PatientService();
            foo.surelyDeleting(SelectedItem);
        }
        void DeleteChecker.deleteObject(object selectedItem)
        {
            PatientCRUDMenager.getInstance().Delete((Patient)selectedItem);
        }

        public static Visibility hideOrShowHelp(Visibility TxtHelp)
        {
            if (TxtHelp == Visibility.Visible)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public static void openUrgentExaminationWindow()
        {
            UrgentPatient urgentPatient = new UrgentPatient();
            urgentPatient.ShowDialog();
        }

        public static void openAnnouncementWindow()
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow();
            announcementWindow.ShowDialog();
        }

        public static void selectDoctorsMenuItem(string HeaderDoctors)
        {
            HeaderDoctors = "< Doctors >";
            DoctorsWindow doctorsWindow = new DoctorsWindow();
            doctorsWindow.Show();
        }

        public static void selectAppointmentsMenuItem(string HeaderAppointments)
        {
            HeaderAppointments = "< Appointments >";
            AppointmentsWindow appointmentsWindow = new AppointmentsWindow();
            appointmentsWindow.Show();
        }

        public static ObservableCollection<Patient> filterPatients(string search)
        {
            ObservableCollection<Patient> newPatientCollection = new ObservableCollection<Patient>();
            ListCollectionView patientCollection = new ListCollectionView(PatientCRUDMenager.getInstance().GetAll());
            patientCollection.Filter = (patient) =>
            {
                Patient tempPatient = patient as Patient;
                if (tempPatient.Adress.ToLower().Contains(search) || tempPatient.City.ToLower().Contains(search) || tempPatient.Country.ToLower().Contains(search) ||
                    tempPatient.Email.ToLower().Contains(search) || tempPatient.FirstName.ToLower().Contains(search) || tempPatient.Jmbg.ToString().ToLower().Contains(search) ||
                    tempPatient.LastName.ToLower().Contains(search) || tempPatient.SocNumber.ToString().ToLower().Contains(search) ||
                    tempPatient.Telephone.ToString().ToLower().Contains(search) || tempPatient.Username.Contains(search))
                {
                    newPatientCollection.Add(tempPatient);
                    return true;
                }
                return false;
            };
            return newPatientCollection;
        }

        public static void saveNewPatient(string FirstName, string LastName, string Jmbg, string Telephone, char Gender, string Adress, string Email, string Username,
                                          string Password, PatientViewModel ViewModel, NewPatientViewModel window)
        {
            long jmbg = Convert.ToInt64(Jmbg);
            long tel = Convert.ToInt64(Telephone);
            Patient patient = new Patient(GenerateNewID(), FirstName, LastName, jmbg, Gender, Adress, tel,
                Email, false, Username, Password, default(DateTime), "", 0, "", "", default, default);
            PatientCRUDMenager.getInstance().Add(patient);
            ViewModel.Load();
            window.Close();
        }
    }
}
