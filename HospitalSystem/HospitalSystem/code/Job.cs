/***********************************************************************
 * Module:  Job.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Job
 ***********************************************************************/

using System;
using System.ComponentModel;

public class Job : INotifyPropertyChanged
{
    private String companyName;
    public String CompanyName
    {
        get { return companyName; }
        set
        {
            if (companyName != value)
            {
                companyName = value;
                OnPropertyChanged("CompanyName");
            }
        }
    }
    private String position;
    public String Position
    {
        get { return position; }
        set
        {
            if (position != value)
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
    }
    private int registerNumber;
    public int RegisterNumber
    {
        get { return registerNumber; }
        set
        {
            if (registerNumber != value)
            {
                registerNumber = value;
                OnPropertyChanged("RegisterNumber");
            }
        }
    }
    private String activityCode;
    public String ActivityCode
    {
        get { return activityCode; }
        set
        {
            if (activityCode != value)
            {
                activityCode = value;
                OnPropertyChanged("ActivityCode");
            }
        }
    }

    public Job(string companyName, string position, int registerNumber, string activityCode)
    {
        this.companyName = companyName;
        this.position = position;
        this.registerNumber = registerNumber;
        this.activityCode = activityCode;
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