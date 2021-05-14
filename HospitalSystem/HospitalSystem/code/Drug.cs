using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code
{
    public class Drug : INotifyPropertyChanged
    { 
         public Drug(int v1, string v2)
         {
             id = v1;
            name = v2;
         }
    
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
        private string report;
        public string Report
        {
            get { return report; }
            set
            {
                if (report != value)
                {
                    report = value;
                    OnPropertyChanged("Report");
                }
            }
        }

        public enum STATUS
        {
            Verified,
            InProgress,
            Unverified
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

        private string name;

        public string Name
        {
            get { return name; }
            set 
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set 
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }
        private ObservableCollection<Ingridient> ingridients;
        public ObservableCollection<Ingridient> Ingridients
        {
            get { return ingridients; }
            set 
            {
                if (ingridients != value)
                {
                    ingridients = value;
                    OnPropertyChanged("Ingridients");
                }
            }
        }
        public override string ToString()
        {
            return id + " - " + name;
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
