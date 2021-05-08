using System;
using System.ComponentModel;

namespace HospitalSystem.code
{
    public class Alergen : INotifyPropertyChanged
    {
        private int no;
        public int No
        {
            get { return no; }
            set
            {
                if (no != value)
                {
                    no = value;
                    OnPropertyChanged("No");
                }
            }
        }
        private String substance;
        public String Substance
        {
            get { return substance; }
            set
            {
                if (substance != value)
                {
                    substance = value;
                    OnPropertyChanged("Substance");
                }
            }
        }
        private int patientID;
        public int PatientID
        {
            get { return patientID; }
            set
            {
                if (patientID != value)
                {
                    patientID = value;
                    OnPropertyChanged("PatientID");
                }
            }
        }

        public Alergen(int no, string substance, int patientID)
        {
            No = no;
            Substance = substance;
            PatientID = patientID;
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