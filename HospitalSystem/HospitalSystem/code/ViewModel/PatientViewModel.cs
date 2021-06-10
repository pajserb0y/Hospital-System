using HospitalSystem.code.Model;
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



       

        public void Load()
        {
            Items = new ObservableCollection<Patient>(PatientService.GetAll());
        }



        public void Add()
        {
            PatientService.openAddPatientWindow(this);
        }

        private void View()
        {
            PatientService.openEditPatientWindow(SelectedItem);
        }

        public void Delete()
        {
            PatientService.deletePatient(SelectedItem);
            Load();
        }

        public void Help()
        {
            TxtHelp = PatientService.hideOrShowHelp(TxtHelp);
        }

        public void CreateUrgent()
        {
            PatientService.openUrgentExaminationWindow();          
        }

        public void CreateAnnouncement()
        {
            PatientService.openAnnouncementWindow();
        }

        public void ShowDoctors()
        {
            PatientService.selectDoctorsMenuItem(HeaderDoctors);
            this.Close();
        }

        public void ShowAppointments()
        {
            PatientService.selectAppointmentsMenuItem(HeaderDoctors);
            this.Close();
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
                Items = PatientService.filterPatients(searchTextChanged);
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
