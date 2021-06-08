/***********************************************************************
 * Module:  Lekar.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Lekar
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

public class Doctor : INotifyPropertyChanged
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


    private string firstname;
    public string FirstName
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

    private string lastname;
    public string LastName
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

    private long jmbg;
    public long Jmbg
    {
        get { return jmbg; }
        set
        {
            if (jmbg != value)
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }
    }


    private string adress;
    public string Adress
    {
        get { return adress; }
        set
        {
            if (adress != value)
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }
    }
    private long telephone;

    public long Telephone
    {
        get { return telephone; }
        set
        {
            if (telephone != value)
            {
                telephone = value;
                OnPropertyChanged("Telephone");
            }
        }
    }

    private string specialization;
    public string Specialization
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

    private ObservableCollection<DateTime> freeDays;
    public ObservableCollection<DateTime> FreeDays
    {
        get { return freeDays; }
        set
        {
            if (freeDays != value)
            {
                freeDays = value;
                OnPropertyChanged("FreeDays");
            }
        }
    }

    public Doctor(int id, string firstName, string lastName, long jmbg, string adress, long telephone, string specialization, ObservableCollection<DateTime> freeDays)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        Telephone = telephone;
        Specialization = specialization;
        FreeDays = freeDays;
    }
    public Doctor()
    { }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public override string ToString()
    {
        return Specialization;
    }
}