using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HospitalSystem.code
{
    public class Ingridient : INotifyPropertyChanged
    {
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
            set {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
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

        public override string ToString()
        {
            return Name;
        }
    }
}
