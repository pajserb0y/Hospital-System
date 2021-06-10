using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code
{
    class Refferal
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
        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                if (note != value)
                {
                    note = value;
                    OnPropertyChanged("Note");
                }
            }
        }
        private String specialization;
        public String Specialization
        {
            get { return specialization; }
            set
            {
                if (specialization != value)
                {
                    specialization = value;
                    OnPropertyChanged("Specialization");
                }
            }
        }
        private int patientId;

        public int PatientId
        {
            get { return patientId; }
            set
            {
                if (patientId != value)
                {
                    patientId = value;
                    OnPropertyChanged("PatientId");
                }
            }
        }

        private string patientFirstName;
        public string PatientFirstName
        {
            get { return patientFirstName; }
            set
            {
                if (patientFirstName != value)
                {
                    patientFirstName = value;
                    OnPropertyChanged("PatientFirstName");
                }
            }
        }
        private string patientLastName;
        public string PatientLastName
        {
            get { return patientLastName; }
            set
            {
                if (patientLastName != value)
                {
                    patientLastName = value;
                    OnPropertyChanged("PatientLastName");
                }
            }
        }


        public enum STATUS
        {
            Active,
            Used
        }

        private STATUS status;

        public STATUS Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int doctorId;

        public int DoctorId
        {
            get { return doctorId; }
            set
            {
                if (doctorId != value)
                {
                    doctorId = value;
                    OnPropertyChanged("DoctorId");
                }
            }
        }

        private string doctorFristName;
        public string DoctorFirstName
        {
            get { return doctorFristName; }
            set
            {
                if (doctorFristName != value)
                {
                    doctorFristName = value;
                    OnPropertyChanged("DoctorFristName");
                }
            }
        }
        private string doctorLastName;
        public string DoctorLastName
        {
            get { return doctorLastName; }
            set
            {
                if (doctorLastName != value)
                {
                    doctorLastName = value;
                    OnPropertyChanged("DoctorLastName");
                }
            }
        }
        public Refferal(int id, string note, string specialization, int patientId, string patientFirstName, string patientLastName, STATUS status, int doctorId, string doctorFristName, string doctorLastName)
        {
            this.id = id;
            this.note = note;
            this.specialization = specialization;
            this.patientId = patientId;
            this.patientFirstName = patientFirstName;
            this.patientLastName = patientLastName;
            this.status = status;
            this.doctorId = doctorId;
            this.doctorFristName = doctorFristName;
            this.doctorLastName = doctorLastName;
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
