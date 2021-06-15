/***********************************************************************
 * Module:  Room.cs
 * Author:  Hope
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
public class Bed
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
    private ObservableCollection<Tuple<DateTime, DateTime, int>> interval;
    //private ObservableCollection<(DateTime, DateTime)> interval;
    public ObservableCollection<Tuple<DateTime, DateTime, int>> Interval
    //public ObservableCollection<(DateTime, DateTime)> Interval
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
        return "Bed" + Id.ToString();
    }
}
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

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(String name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
    //DODAO SAM DATE I INTERVAL ZATO STO MI TREBA OD KOG DATUMA I KOLIKO DUGO JE ZAUZETA SOBA
    private ObservableCollection<Bed> beds;
    public ObservableCollection<Bed> Beds
    {
        get { return beds; }
        set
        {
            if (beds != value)
            {
                beds = value;
                OnPropertyChanged("Beds");
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

