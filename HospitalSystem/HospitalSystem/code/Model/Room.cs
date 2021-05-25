/***********************************************************************
 * Module:  Room.cs
 * Author:  Hope
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;
using System.ComponentModel;

public class Room : INotifyPropertyChanged
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

    private String name;
    public String Name
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
    private bool isOccupied;

    public bool IsOccupied
    {
        get { return isOccupied; }
        set
        {
            if (isOccupied != value)
            {
                isOccupied = value;
                OnPropertyChanged("IsOccupied");
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
    //DODAO SAM DATE I INTERVAL ZATO STO MI TREBA OD KOG DATUMA I KOLIKO DUGO JE ZAUZETA SOBA

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

    private int interval;
    public int Interval
    {
        get { return interval; }
        set
        {
            if (interval != value)
            {
                interval = value;
                OnPropertyChanged("Interval");
            }
        }
    }
    public Room(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public override string ToString()
    {
        return Id.ToString() + " " + Name;
    }

}