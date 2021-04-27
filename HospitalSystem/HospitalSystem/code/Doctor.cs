/***********************************************************************
 * Module:  Lekar.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Lekar
 ***********************************************************************/

using System;
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

    public Doctor(int id, string firstName, string lastName, long jmbg, string adress, long telephone, string specialization)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        Telephone = telephone;
        Specialization = specialization;
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
        return Id.ToString() + " " + FirstName + " " + LastName + Specialization + Jmbg.ToString() + " " + Adress + " " + Telephone.ToString();
    }
}