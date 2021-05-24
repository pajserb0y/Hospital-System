
/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Appointment
 ***********************************************************************/

using System;
using System.ComponentModel;

public class Appointment : INotifyPropertyChanged
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
    private DateTime time;
    public DateTime Time {
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

    private DateTime date;
    public DateTime Date {
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

    private Patient patient;
    public Patient Patient {
        get { return patient; }
        set
        {
            if (patient != value)
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }
    }

    private Room room;
    public Room Room {
        get { return room; }
        set
        {
            if (room != value)
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }
    }

    private Doctor doctor;
    public Doctor Doctor {
        get { return doctor; }
        set
        {
            if (doctor != value)
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
    }
    private bool isOperation;
    public bool IsOperation
    {
        get { return isOperation; }
        set
        {
            if (isOperation != value)
            {
                isOperation = value;
                OnPropertyChanged("IsOperation");
            }
        }
    }

    private int timesChanged;

    public int TimesChanged
    {
        get { return timesChanged; }
        set
        {
            if (timesChanged != value)
            {
                timesChanged = value;
                OnPropertyChanged("TimesChanged");
            }
        }
    }

    private DateTime timeOfCreation;

    public DateTime TimeOfCreation
    {
        get { return timeOfCreation; }
        set
        {
            if (timeOfCreation != value)
            {
                timeOfCreation = value;
                OnPropertyChanged("TimeOfCreation");
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
}