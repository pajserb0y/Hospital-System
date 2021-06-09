using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HospitalSystem.code.ViewModel
{
    class NewJobViewModel : INotifyPropertyChanged
    {
        private RelayCommand okCommand;
        private Window window;
        private Patient currentPatient;
        public Patient CurrentPatient
        {
            get { return currentPatient; }
            set
            {
                if (currentPatient != value)
                {
                    currentPatient = value;
                    OnPropertyChanged("ViewModel");
                }
            }
        }
        private int calledFromConstructor;
        public int CalledFromConstructor
        {
            get { return calledFromConstructor; }
            set
            {
                if (calledFromConstructor != value)
                {
                    calledFromConstructor = value;
                    OnPropertyChanged("CalledFromConstructor");
                }
            }
        }
        private Job currentJob;
        public Job CurrentJob
        {
            get { return currentJob; }
            set
            {
                if (currentJob != value)
                {
                    currentJob = value;
                    OnPropertyChanged("CurrentJob");
                }
            }
        }


        public NewJobViewModel(Patient selectedPatient, Window window, int calledFromConstructor)
        {
            this.currentPatient = selectedPatient;
            this.window = window;
            this.calledFromConstructor = calledFromConstructor;
        }
        public NewJobViewModel(Patient selectedPatient, Window window, int calledFromConstructor, Job selectedJob)
        {
            this.currentPatient = selectedPatient;
            this.window = window;
            this.calledFromConstructor = calledFromConstructor;
            this.currentJob = selectedJob;
            CompanyName = selectedJob.CompanyName;
            JobPosition = selectedJob.Position;
            RegisterCode = selectedJob.RegisterNumber.ToString();
            ActivityCode = selectedJob.ActivityCode;
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
            if (ActivityCode == null || CompanyName == null || JobPosition == null || RegisterCode == null)
                MessageBox.Show("Each field is required.");
            else
            {
                try
                {
                    if (Convert.ToInt64(RegisterCode) < 1000 || Convert.ToInt64(RegisterCode) > 9999 || RegisterCode.Length != 4)
                        ErrorRegister = "Register number must contain 4 digits";
                    else if (ActivityCode.Length != 4)
                        ErrorActivity = "Activity code must contain 4 characters";
                    else
                    {
                        if (CalledFromConstructor == 1)
                        {
                            currentPatient.WorkHistory.Add(new Job(CompanyName, JobPosition, Convert.ToInt32(RegisterCode), ActivityCode));
                            this.Close();
                        }
                        else
                        {
                            int indexOfJob = currentPatient.WorkHistory.IndexOf(currentJob);
                            currentPatient.WorkHistory.RemoveAt(indexOfJob);
                            currentPatient.WorkHistory.Insert(indexOfJob, new Job(CompanyName, JobPosition, Convert.ToInt32(RegisterCode), ActivityCode));
                            this.Close();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Register number field must contain only digits!");
                }
            }
        }


        public void Close()
        {
            window.Close();
        }



        private string errorRegister;
        public string ErrorRegister
        {
            get { return errorRegister; }
            set
            {
                errorRegister = value;
                OnPropertyChanged(nameof(ErrorRegister));
            }
        }
        private string errorActivity;
        public string ErrorActivity
        {
            get { return errorActivity; }
            set
            {
                errorActivity = value;
                OnPropertyChanged(nameof(ErrorActivity));
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (companyName != value)
                {
                    companyName = value;
                    OnPropertyChanged("CompanyName");
                }
            }
        }
        private string jobPosition;
        public string JobPosition
        {
            get { return jobPosition; }
            set
            {
                if (jobPosition != value)
                {
                    jobPosition = value;
                    OnPropertyChanged("JobPosition");
                }
            }
        }
        private string registerCode;
        public string RegisterCode
        {
            get { return registerCode; }
            set
            {
                if (registerCode != value)
                {
                    registerCode = value;
                    OnPropertyChanged("RegisterCode");
                }
            }
        }
        private string activityCode;
        public string ActivityCode
        {
            get { return activityCode; }
            set
            {
                if (activityCode != value)
                {
                    activityCode = value;
                    OnPropertyChanged("ActivityCode");
                }
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
