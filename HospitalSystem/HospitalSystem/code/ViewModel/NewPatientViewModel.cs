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
        private RelayCommand mCheckedCommand;
        private RelayCommand fCheckedCommand;
        private Window window;
        private string errorJmbg;
        private Patient patientItem;
        private PatientViewModel viewModel;
        public PatientViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != value)
                {
                    viewModel = value;
                    OnPropertyChanged("ViewModel");
                }
            }
        }

        private String firstname;
        public String FirstName
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                {
                    firstname = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        private String lastname;
        public String LastName
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged("Lastname");
                }
            }
        }
        private string jmbg;
        public string Jmbg
        {
            get { return jmbg; }
            set
            {
                if (jmbg != value)
                {
                    jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }
        private char gender;
        public char Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }
        private String adress;
        public String Adress
        {
            get { return adress; }
            set
            {
                if (adress != value)
                {
                    adress = value;
                    OnPropertyChanged("Adress");
                }
            }
        }
        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                if (telephone != value)
                {
                    telephone = value;
                    OnPropertyChanged("Telephone");
                }
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private String username;
        public String Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        private String password;
        public String Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }


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
        public RelayCommand MCheckedCommand
        {
            get
            {
                return mCheckedCommand ?? (mCheckedCommand = new RelayCommand(param => MChecked()));
            }

        }
        public RelayCommand FCheckedCommand
        {
            get
            {
                return fCheckedCommand ?? (fCheckedCommand = new RelayCommand(param => FChecked()));
            }

        }

        public void Add()
        {
            if (FirstName == null || LastName == null || Jmbg == null || Telephone == null)
                MessageBox.Show("First name, Last name, JMBG and Telephone fields are required.");
            else
                try
                {
                    if (Convert.ToInt64(Jmbg) < 100000000000 || Convert.ToInt64(Jmbg) > 9999999999999 || Jmbg.Length != 13)
                        ErrorJmbg = "JMBG must contain 13 digits";
                    else
                    {
                        long jmbg = Convert.ToInt64(Jmbg);
                        long tel = Convert.ToInt64(Telephone);
                        Patient patient = new Patient(PatientsStorage.getInstance().GenerateNewID(), FirstName, LastName, jmbg, Gender, Adress, tel,
                            Email, false, Username, Password, default(DateTime), "", 0, "", "", default, default);
                        PatientsStorage.getInstance().Add(patient);
                        ViewModel.Load();
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("JMBG and Telephone field must contain only digits!");
                }
        }
        public void MChecked()
        {
            Gender = 'M';
        }
        public void FChecked()
        {
            Gender = 'F';
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
