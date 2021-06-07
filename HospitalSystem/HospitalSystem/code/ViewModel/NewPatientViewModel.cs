using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    public class NewPatientViewModel : INotifyPropertyChanged
    {
        private RelayCommand okCommand;
        private Window window;

        private Patient patientItem;
        private PatientViewModel viewModel;
        private string errorJmbg;


        public NewPatientViewModel(PatientViewModel viewModel, Window window)
        {
            patientItem = new Patient();
            this.viewModel = viewModel;
            this.window = window;

        }

        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(param => Add()));
            }

        }

        public void Add()
        {
            if (patientItem.FirstName == "" || patientItem.LastName == "" || patientItem.Jmbg == 0 || patientItem.Telephone == 0)
                MessageBox.Show("First name, Last name, JMBG and Telephone fields are required.");
            else
                try
                {
                    if (patientItem.Jmbg < 100000000000 || patientItem.Jmbg > 9999999999999 || patientItem.Jmbg.ToString().Length != 13)
                        ErrorJmbg = "JMBG must contain 13 digits";
                    else
                    {
                        long jmbg = this.patientItem.Jmbg;
                        long tel = this.patientItem.Telephone;
                        Patient patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), PatientItem.FirstName, PatientItem.LastName, jmbg, 'M', PatientItem.Adress, tel,
                            PatientItem.Email, false, PatientItem.Username, PatientItem.Password, default(DateTime), "", 0, "", "", default, default);
                        PatientsStorage.getInstance().Add(patient);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("JMBG and Telephone field must contain only digits!");
                }
        }

        public void Close()
        {
            window.Close();
        }


        public Patient PatientItem
        {
            get { return patientItem; }
            set
            {
                patientItem = value;
                OnPropertyChanged(nameof(PatientItem));
            }
        }
        public string ErrorJmbg
        {
            get { return errorJmbg; }
            set
            {
                errorJmbg = value;
                OnPropertyChanged(nameof(ErrorJmbg));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
