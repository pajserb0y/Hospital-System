/***********************************************************************
 * Module:  Patient.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Patient
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private DateTime birthDate;
    public DateTime BirthDate
    {
        get { return birthDate; }
        set
        {
            if (birthDate != value)
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
    }
    private String marriageStatus;
    public String MarriageStatus
    {
        get { return marriageStatus; }
        set
        {
            if (marriageStatus != value)
            {
                marriageStatus = value;
                OnPropertyChanged("MarriageStatus");
            }
        }
    }
    private long socNumber;
    public long SocNumber
    {
        get { return socNumber; }
        set
        {
            if (socNumber != value)
            {
                socNumber = value;
                OnPropertyChanged("SocNumber");
            }
        }
    }
    private String city;
    public String City
    {
        get { return city; }
        set
        {
            if (city != value)
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
    }
    private String country;
    public String Country
    {
        get { return country; }
        set
        {
            if (country != value)
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }
    }
    private ObservableCollection<string> alergens;
    public ObservableCollection<string> Alergens
    {
        get { return alergens; }
        set
        {
            if (alergens != value)
            {
                alergens = value;
                OnPropertyChanged("Alergens");
            }
        }
    }
    private ObservableCollection<Job> workHistory;
    public ObservableCollection<Job> WorkHistory
    {
        get { return workHistory; }
        set
        {
            if (workHistory != value)
            {
                workHistory = value;
                OnPropertyChanged("WorkHistory");
            }
        }
    }

    public Patient() { }
    public Patient(string firstName) 
    {
        FirstName = firstName;
    }


    public Patient(int id, string firstName, string lastName, long jmbg, char gender, string adress, long telephone, string email, bool guest, string username, string password, DateTime birthDate,
        string marriageStatus, long socNumber, string city, string country, ObservableCollection<string> alergens, ObservableCollection<Job> workHistory)
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
        BirthDate = birthDate;
        MarriageStatus = marriageStatus;
        SocNumber = socNumber;
        City = city;
        Country = country;
        Alergens = alergens;
        WorkHistory = workHistory;
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
        return Id.ToString() + " " + FirstName + " " + LastName;
    }
}