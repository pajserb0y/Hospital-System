using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code
{
    public class Announcement : INotifyPropertyChanged
    {
        private List<int> doctorIDs;
        public List<int> DoctorIDs
        {
            get { return doctorIDs; }
            set
            {
                if (doctorIDs != value)
                {
                    doctorIDs = value;
                    OnPropertyChanged("DoctorIDs");
                }
            }
        }
        private List<int> patientIDs;
        public List<int> PatientIDs
        {
            get { return patientIDs; }
            set
            {
                if (patientIDs != value)
                {
                    patientIDs = value;
                    OnPropertyChanged("PatientIDs");
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
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        public Announcement(List<int> doctors, List<int> patients, DateTime date, DateTime time, string title, string content)
        {
            DoctorIDs = doctors;
            PatientIDs = patients;
            Date = date;
            Time = time;
            Title = title;
            Content = content;
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
