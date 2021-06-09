using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    class StepTwoViewModel : INotifyPropertyChanged
    {
        private RelayCommand nextCommand;
        private RelayCommand finishCommand;
        private RelayCommand backCommand;
        private Window window;
        private Doctor selectedDoctor;


        public StepTwoViewModel(Doctor selectedDoctor, Window window)
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
            StepThree st = new StepThree(selectedDoctor);
            Close();
            st.ShowDialog();
        }
        public void Finish()
        {
            Close();
        }
        public void Back()
        {
            StepOne so = new StepOne(selectedDoctor);
            Close();
            so.ShowDialog();
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
