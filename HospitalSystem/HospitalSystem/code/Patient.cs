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
    private char gender;
    public char Gender
    {
        get { return gender; }
        set
        {
            if (gender != value)
            {
                gender = value;
                OnPropertyChanged("Gender");
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
    private string email;
    public string Email
    {
        get { return email; }
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
    }
    private bool guest;
    public bool Guest
    {
        get { return guest; }
        set
        {
            if (guest != value)
            {
                guest = value;
                OnPropertyChanged("Guest");
            }
        }
    }
    private String username;
    public String Username
    {
        get { return username; }
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
    }
    private String password;
    public String Password
    {
        get { return password; }
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
    }


    public Patient() { }

    public Patient(int id, string firstName, string lastName, long jmbg, char gender, string adress, long telephone, string email, bool guest, String username, String password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Gender = gender;
        Adress = adress;
        Telephone = telephone;
        Email = email;
        Guest = guest;
        Username = username;
        Password = password;
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
        return Id.ToString() + " " + FirstName + " " + LastName + Jmbg.ToString() + " " + Adress + " " + Telephone.ToString();
    }
}