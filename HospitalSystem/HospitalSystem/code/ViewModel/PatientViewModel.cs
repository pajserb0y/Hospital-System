using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {

        private RelayCommand addCommand;
        private RelayCommand deleteCommand;

        private ObservableCollection<Patient> items;
        private Patient selectedItem;
        private Window window;


        public PatientViewModel(Window window)
        {
            Load();
            this.window = window;
        }


        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => Add()));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
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
            Items = new ObservableCollection<Patient>(PatientsStorage.getInstance().GetAll());
        }

        public void Add()
        {
            NewPatient np = new NewPatient(this);
            np.ShowDialog();
        }

        public void Delete()
        {
            PatientsStorage.getInstance().Delete(selectedItem);
            Load();
        }

        

     

        public bool CanDelete()
        {
            return selectedItem != null;
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
