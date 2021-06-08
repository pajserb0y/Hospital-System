using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    class StepOneViewModel : INotifyPropertyChanged
    {
        private RelayCommand nextCommand;
        private RelayCommand finishCommand;
        private RelayCommand backCommand;
        private Window window;
        private Doctor selectedDoctor;


        public StepOneViewModel(Doctor selectedDoctor, Window window)
        {
            this.window = window;
            this.selectedDoctor = selectedDoctor;
        }

        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ?? (nextCommand = new RelayCommand(param => Next()));
            }

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

        public void Next()
        {
            StepTwo st = new StepTwo(selectedDoctor);
            st.Show();
            Close();
        }
        public void Finish()
        {
            DoctorInitialWindow dw = new DoctorInitialWindow(selectedDoctor);
            dw.Show();
            Close();
        }
        public void Back()
        {
            HelpWizard hw = new HelpWizard(selectedDoctor);
            hw.Show();
            Close();
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
