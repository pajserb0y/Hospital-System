using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code.Model
{
    class HospitalizationDTO
    {
        private int roomId;
        public int RoomId
        {
            get { return roomId; }
            set
            {
                if (roomId != value)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        private int bedId;
        public int BedId
        {
            get { return bedId; }
            set
            {
                if (bedId != value)
                {
                    bedId = value;
                    OnPropertyChanged("BedId");
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
        private String firstname;
        public String FirstName
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                {
                    firstname = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        private String lastname;
        public String LastName
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged("Lastname");
                }
            }
        }
        private DateTime dateIn;
        public DateTime DateIn
        {
            get { return dateIn; }
            set
            {
                if (dateIn != value)
                {
                    dateIn = value;
                    OnPropertyChanged("DateIn");
                }
            }
        }

        private DateTime dateOut;
        public DateTime DateOut
        {
            get { return dateOut; }
            set
            {
                if (dateOut != value)
                {
                    dateOut = value;
                    OnPropertyChanged("DateOut");
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

        public HospitalizationDTO(int BedId, DateTime dateInn ,DateTime dateOutt,Patient patient,Room room)
        {
            roomId = room.Id;
            bedId = BedId;
            patientId = patient.Id;
            firstname = patient.FirstName;
            lastname = patient.LastName;
            dateIn = dateInn;
            dateOut = dateOutt;
        }
    }
}
