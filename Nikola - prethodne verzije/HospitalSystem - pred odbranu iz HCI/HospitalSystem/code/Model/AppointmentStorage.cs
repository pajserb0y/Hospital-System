/***********************************************************************
 * Module:  AppointmentStorage.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.AppointmentStorage
 ***********************************************************************/

using HospitalSystem.code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

public class AppointmentStorage
{
    private static AppointmentStorage instance = null;

    public static AppointmentStorage getInstance()
    {
        if (instance == null)
        {
            instance = new AppointmentStorage();
        }
        return instance;
    }

    private ObservableCollection<Appointment> appointments;
    public ObservableCollection<Appointment> Appointments
    {
        get { return appointments; }
        set { appointments = value; }
    }

    private String FileLocation = "../../../Resource/Appointments.json";

    AppointmentStorage()
    {
        this.appointments = deserialize();
    }

    private AppointmentViewModel copyAVM(Appointment a)
    {
        AppointmentViewModel temp = new AppointmentViewModel();

        temp.Id = a.Id;
        temp.DoctorId = a.Doctor.Id;
        temp.PatientId = a.Patient.Id;
        temp.RoomId = a.Room.Id;
        temp.Date = a.Date;
        temp.Time = a.Time;
        temp.IsOperation = a.IsOperation;
        temp.TimesChanged = a.TimesChanged;
        temp.TimeOfCreation = a.TimeOfCreation;

        return temp;
    }

    public void serialize()
    {
        List<AppointmentViewModel> appts = new List<AppointmentViewModel>();
        foreach (Appointment a in appointments)
        {
            AppointmentViewModel temp = copyAVM(a);
            appts.Add(temp);
        }
        var JSONresult = JsonConvert.SerializeObject(appts);

        using (StreamWriter sw = new StreamWriter(FileLocation))
        {
            sw.Write(JSONresult);
            sw.Close();
        }
    }

    public ObservableCollection<Appointment> deserialize()
    {
        List<AppointmentViewModel> apptIDs = new List<AppointmentViewModel>();
  
        using (StreamReader sr = new StreamReader(FileLocation))
        {
            apptIDs = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(sr.ReadToEnd());
            if (apptIDs == null)
                apptIDs = new List<AppointmentViewModel>();
            sr.Close();
        }

        return convertAvmToAppt(apptIDs);
    }

    private ObservableCollection<Appointment> convertAvmToAppt(List<AppointmentViewModel> avms)
    {
        ObservableCollection<Appointment> appts = new ObservableCollection<Appointment>();

        foreach (AppointmentViewModel a in avms)
        {
            Appointment temp = new Appointment(a.Id, DoctorStorage.getInstance().GetOne(a.DoctorId), PatientsStorage.getInstance().GetOne(a.PatientId), RoomStorage.getInstance().GetOne(a.RoomId), a.Date, a.Time, a.IsOperation, a.TimesChanged, a.TimeOfCreation);
            appts.Add(temp);
        }

        return appts;
    }

    public Appointment GetOne(int Id)
   {
        foreach (Appointment e in appointments)
            if (Id == e.Id)
                return e;
        return null;
    }
   
   public ObservableCollection<Appointment> GetAll()
   {
        return appointments;
   }
   
   public void Edit(Appointment appointment)
   {
        foreach (Appointment a in this.appointments)
            if (appointment.Id == a.Id)
            {
                a.Doctor = appointment.Doctor;
                a.Date = appointment.Date;
                a.Time = appointment.Time;
                a.TimesChanged += 1;
            }
        serialize();
    }
   
   public void Delete(Appointment appointment)
   {
        foreach (Appointment a in appointments)
            if (appointment.Id == a.Id)
            {
                this.appointments.Remove(a);
                break;
            }
        serialize();
    }
   
   public void Add(Appointment a)
   {
        this.appointments.Add(a);
        serialize();
    }

    public int GenerateNewID()
    {
        if (appointments == null)
            return 1;
        return (appointments.Count == 0) ? 1 : appointments[appointments.Count - 1].Id + 1;
    }

}

internal class AppointmentViewModel
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
            }
        }
    }
    private DateTime time;
    public DateTime Time
    {
        get { return time; }
        set
        {
            if (time != value)
            {
                time = value;
            }
        }
    }

    private DateTime date;
    public DateTime Date
    {
        get { return date; }
        set
        {
            if (date != value)
            {
                date = value;
            }
        }
    }

    private int patientId;
    public int PatientId
    {
        get { return patientId; }
        set
        {
            if (patientId != value)
            {
                patientId = value;
            }
        }
    }

    private int roomId;
    public int RoomId
    {
        get { return roomId; }
        set
        {
            if (roomId != value)
            {
                roomId = value;
            }
        }
    }

    private int doctorId;
    public int DoctorId
    {
        get { return doctorId; }
        set
        {
            if (doctorId != value)
            {
                doctorId = value;
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
            }
        }
    }
}