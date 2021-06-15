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
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(String name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
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