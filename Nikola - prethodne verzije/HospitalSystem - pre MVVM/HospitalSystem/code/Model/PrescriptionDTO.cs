using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code.Model
{
    class PrescriptionDTO
    {
        private int examId;
        public int ExamId
        {
            get { return examId; }
            set
            {
                if (examId != value)
                {
                    examId = value;
                    OnPropertyChanged("ExamId");
                }
            }
        }

        private int prescriptionId;
        public int PrescriptionId
        {
            get { return prescriptionId; }
            set
            {
                if (prescriptionId != value)
                {
                    prescriptionId = value;
                    OnPropertyChanged("PrescriptionId");
                }
            }

        }
        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
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

        private Patient patient;
        public Patient Patient
        {
            get { return patient; }
            set
            {
                if (patient != value)
                {
                    patient = value;
                    OnPropertyChanged("Patient");
                }
            }
        }

        private Doctor doctor;
        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
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
        public PrescriptionDTO(Prescription Prescription, Examination Examination)
        {
            prescriptionId = Prescription.Id;
            examId = Examination.Id;
            date = Examination.Date;
            time = Examination.Time;
            interval = Prescription.Interval;
            taking = Prescription.Taking;
            drug = Prescription.Drug;
            patient = Examination.Patient;
            doctor = Examination.Doctor;
        }
    }
}
