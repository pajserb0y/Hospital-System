using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    class StepThreeViewModel : INotifyPropertyChanged
    {
        private RelayCommand finishCommand;
        private RelayCommand backCommand;
        private Window window;
        private Doctor selectedDoctor;


        public StepThreeViewModel(Doctor selectedDoctor, Window window)
        {
            this.window = window;
            this.selectedDoctor = selectedDoctor;
        }

        public RelayCommand FinishCommand
        {
            get
            {
                return finishCommand ?? (finishCommand = new RelayCommand(param => Finish()));
            }

        }

        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => Back()));
            }

        }


        public void Finish()
        {
            Close();
        }
        public void Back()
        {
            StepTwo st = new StepTwo(selectedDoctor);
            Close();
            st.ShowDialog();
            
        }

        public void Close()
        {
            window.Close();
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
