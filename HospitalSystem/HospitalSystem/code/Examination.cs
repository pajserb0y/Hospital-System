/***********************************************************************
 * Module:  Pregled.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.Pregled
 ***********************************************************************/

using HospitalSystem.code;
using System.Collections.ObjectModel;

public class Examination : Appointment
{
    private Anamnesis anamnesis;

    public Anamnesis Anamnesis
    {
        get { return anamnesis; }
        set
        {
            if(value != anamnesis)
            {
                value = anamnesis;
                OnPropertyChanged("Anamnesis");
            }
        }
    }

    private ObservableCollection<Prescription> prescriptions;

    public ObservableCollection<Prescription> Prescriptions
    {
        get { return prescriptions; }
        set
        {
            if(prescriptions != value)
            {
                prescriptions = value;
                OnPropertyChanged("Prescriptions");
            }
        }
    }
    public Examination() 
    {
        this.anamnesis = new Anamnesis();
        this.prescriptions = new ObservableCollection<Prescription>();
    }

    public Examination(Anamnesis anamnesis, ObservableCollection<Prescription> prescriptions)
    {
        this.anamnesis = anamnesis;
        this.prescriptions = prescriptions;
    }
}