/***********************************************************************
 * Module:  Patient.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Patient
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;   
public class Patient : INotifyPropertyChanged
{
    //public File file;
    private int id;
    public int Id
    {
        get { return id; }
        set {
            if (id != value)
            {
                id = value;
                OnPropertyChanged("Id");
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

    
    private String adress;
    public String Adress
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
    {   get { return telephone; }
        set
        {
            if (telephone != value)
            {
                telephone = value;
                OnPropertyChanged("Telephone");
            }
        }
    }

    public Patient(int id, string firstName, string lastName, long jmbg, string adress, long telephone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        Telephone = telephone;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged(String name)
    {
        if(PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));  
        }
    }

    public override string ToString()
    {
        return Id.ToString() + " " + FirstName + " " + LastName + Jmbg.ToString() + " " + Adress + " " + Telephone.ToString();
    }
}