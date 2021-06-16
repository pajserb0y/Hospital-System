using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code
{
    public class Prescription
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }

        }

        private DateTime dateOfPrescription;
        public DateTime DateOfPrescription
        {
            get { return dateOfPrescription; }
            set
            {
                if (dateOfPrescription != value)
                {
                    dateOfPrescription = value;
                    OnPropertyChanged("DateOfPrescription");
                }
            }

        }

        private int interval;
        public int Interval
        {
            get { return interval; }
            set
            {
                if (interval != value)
                {
                    interval = value;
                    OnPropertyChanged("Interval");
                }
            }
        }

        private string taking;
        public string Taking
        {
            get { return taking; }
            set
            {
                if (taking != value)
                {
                    taking = value;
                    OnPropertyChanged("Taking");
                }
            }

        }

        private Drug drug;
        public Drug Drug
        {
            get { return drug; }
            set
            {
                if (drug != value)
                {
                    drug = value;
                    OnPropertyChanged("Drug");
                }
            }
        }

        private DateTime timeOfPrescription;
        public DateTime TimeOfPrescription
        {
            get { return timeOfPrescription; }
            set
            {
                if (timeOfPrescription != value)
                {
                    timeOfPrescription = value;
                    OnPropertyChanged("TimeOfPerscription");
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

        public Prescription(int prescID, Drug drug, string taking,int interval, DateTime date,DateTime time)
        {
            this.id = prescID;
            this.drug = drug;
            this.taking = taking;
            this.dateOfPrescription = date;
            this.timeOfPrescription = time;
            this.interval = interval;
        }
    }
}