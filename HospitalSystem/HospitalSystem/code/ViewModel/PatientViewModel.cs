using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace HospitalSystem.code.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {

        private RelayCommand addCommand;
        private RelayCommand deleteCommand;
        private RelayCommand viewCommand;
        private RelayCommand createUrgentCommand;
        private RelayCommand createAnnouncementCommand;
        private RelayCommand logOutCommand;
        private RelayCommand helpCommand;
        private RelayCommand showDoctorsCommand;
        private RelayCommand showAppointmentsCommand;


        private ObservableCollection<Patient> items;
        private Patient selectedItem;
        private Visibility txtHelp;
        private Visibility labelSearchVisibility;
        private string searchTextChanged;
        private string headerDoctors;
        private string headerAppointments;
        private Window window;


        public PatientViewModel(Window window)
        {
            Load();
            this.window = window;
            txtHelp = Visibility.Collapsed;
            labelSearchVisibility = Visibility.Visible;
            headerDoctors = "Doctors";
            headerAppointments = "Appointments";
        }


        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => Add()));
            }
        }
        public RelayCommand ViewCommand
        {
            get
            {
                return viewCommand ?? (viewCommand = new RelayCommand(param => View(), param => CanView()));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }
        public RelayCommand LogOutCommand
        {
            get
            {
                return logOutCommand ?? (logOutCommand = new RelayCommand(param => LogOut()));
            }
        }
        public RelayCommand HelpCommand
        {
            get
            {
                return helpCommand ?? (helpCommand = new RelayCommand(param => Help()));
            }
        }
        public RelayCommand CreateUrgentCommand
        {
            get
            {
                return createUrgentCommand ?? (createUrgentCommand = new RelayCommand(param => CreateUrgent()));
            }
        }
        public RelayCommand CreateAnnouncementCommand
        {
            get
            {
                return createAnnouncementCommand ?? (createAnnouncementCommand = new RelayCommand(param => CreateAnnouncement()));
            }
        }
        public RelayCommand ShowDoctorsCommand
        {
            get
            {
                return showDoctorsCommand ?? (showDoctorsCommand = new RelayCommand(param => ShowDoctors()));
            }
        }
        public RelayCommand ShowAppointmentsCommand
        {
            get
            {
                return showAppointmentsCommand ?? (showAppointmentsCommand = new RelayCommand(param => ShowAppointments()));
            }
        }


        public ObservableCollection<Patient> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public Patient SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }



        private void filterPatients(string search)
        {
            ObservableCollection<Patient> newPatientCollection = new ObservableCollection<Patient>();
            ListCollectionView patientCollection = new ListCollectionView(PatientsStorage.getInstance().GetAll());
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
            Items = newPatientCollection;
        }

        public void Load()
        {
            Items = new ObservableCollection<Patient>(PatientsStorage.getInstance().GetAll());
        }

        public void Add()
        {
            NewPatient np = new NewPatient(this);
            np.ShowDialog();
        }

        private void View()
        {
            EditPatient editPatient = new EditPatient(SelectedItem);
            editPatient.ShowDialog();
        }

        public void Delete()
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to delete this patient?", "Permanently deleting", btnMessageBox, icnMessageBox);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    {
                        PatientsStorage.getInstance().Delete(SelectedItem);
                        Load();
                        break;
                    }

                case MessageBoxResult.No:
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }
        }

        public void LogOut()
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure that you want to log out from your account?", "Loging out", btnMessageBox, icnMessageBox);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        PatientsStorage.getInstance().serialize();
                        Close();
                        break;
                    }

                case MessageBoxResult.No:
                    /* ... */
                    break;

                case MessageBoxResult.Cancel:
                    /* ... */
                    break;
            }
        }

        public void Help()
        {
            if (TxtHelp == Visibility.Visible)
                TxtHelp = Visibility.Collapsed;
            else
                TxtHelp = Visibility.Visible;
        }

        public void CreateUrgent()
        {
            UrgentPatient urgentPatient = new UrgentPatient();
            urgentPatient.ShowDialog();
        }

        public void CreateAnnouncement()
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow();
            announcementWindow.ShowDialog();
        }

        public void ShowDoctors()
        {
            HeaderDoctors = "< Doctors >";
            DoctorsWindow doctorsWindow = new DoctorsWindow();
            doctorsWindow.Show();
            this.Close();
        }

        public void ShowAppointments()
        {
            HeaderAppointments = "< Appointments >";
            AppointmentsWindow appointmentsWindow = new AppointmentsWindow();
            appointmentsWindow.Show();
            this.Close();
        }





        public bool CanView()
        {
            if (selectedItem == null)
                MessageBox.Show("You have to select patient first.");
            return selectedItem != null;
        }
        public bool CanDelete()
        {
            if (selectedItem == null)
                MessageBox.Show("You have to select patient first.");
            return selectedItem != null;
        }

        public void Close()
        {
            window.Close();
            PatientsStorage.getInstance().serialize();
        }




        public Visibility TxtHelp
        {
            get { return txtHelp; }
            set
            {
                txtHelp = value;
                OnPropertyChanged(nameof(TxtHelp));
            }
        }
        
        public Visibility LabelSearchVisibility
        {
            get { return labelSearchVisibility; }
            set
            {
                labelSearchVisibility = value;
                OnPropertyChanged(nameof(LabelSearchVisibility));
            }
        }
        public string SearchTextChanged
        {
            get { return searchTextChanged; }
            set
            {
                searchTextChanged = value;
                filterPatients(searchTextChanged);
                OnPropertyChanged(nameof(SearchTextChanged));
            }
        }
        public string HeaderDoctors
        {
            get { return headerDoctors; }
            set
            {
                headerDoctors = value;
                OnPropertyChanged(nameof(HeaderDoctors));
            }
        }
        public string HeaderAppointments
        {
            get { return headerAppointments; }
            set
            {
                headerAppointments = value;
                OnPropertyChanged(nameof(HeaderAppointments));
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
