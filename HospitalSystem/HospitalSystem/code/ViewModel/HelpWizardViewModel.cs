using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    class HelpWizardViewModel : INotifyPropertyChanged
    {
        private RelayCommand nextCommand;
        private RelayCommand finishCommand;
        private HelpWizard window;
        private Doctor selectedDoctor;


        public HelpWizardViewModel(Doctor selectedDoctor, HelpWizard window)
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
       
        public void Next()
        {
            StepOne so = new StepOne(selectedDoctor);
            Close();
            so.ShowDialog();
            
        }
        public void Finish()
        {
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
