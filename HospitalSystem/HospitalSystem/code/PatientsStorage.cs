/***********************************************************************
 * Module:  PatientsStorage.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.PatientsStorage
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

public class PatientsStorage
{
    private static PatientsStorage instance = null;
    public static PatientsStorage getInstance()
    {
        if (instance == null)
        {
            instance = new PatientsStorage();
        }
        return instance;
    }


    private ObservableCollection<Patient> patients;
    public ObservableCollection<Patient> Patients
    {
        get { return patients; }
        set { patients = value; }
    }

    private String FileLocation = "../../../Resource/Pacijenti.json";


    public PatientsStorage()
    {
        this.patients = deserialize();
    }

    public Patient GetOne(int patientID)
    {
        foreach (Patient p in patients)
            if (patientID == p.Id)
                return p;
        return null;
    }

    public ObservableCollection<Patient> GetAll()
    {
        return patients;
    }

    public void Edit(Patient patient)
    {
        foreach (Patient P in this.patients)
            if (patient.Id == P.Id)
            {
                P.Jmbg = patient.Jmbg;
                P.FirstName = patient.FirstName;
                P.LastName = patient.LastName;
                P.Adress = patient.Adress;
                P.Telephone = patient.Telephone;
                P.Email = patient.Email;
                P.Gender = patient.Gender;
                P.Guest = patient.Guest;
                P.Username = patient.Username;
                P.Password = patient.Password;
                P.MarriageStatus = patient.MarriageStatus;
                P.SocNumber = patient.SocNumber;
                P.Country = patient.Country;
                P.City = patient.City;
                P.BirthDate = patient.BirthDate;
            }
    }

    public void Delete(Patient patient)
    {
        foreach (Patient p in patients)
            if (patient.Id == p.Id)
            {
                this.patients.Remove(p);
                break;
            }

        deletePatientExaminations(patient);
        deletePatientAppointments(patient);
    }

    private static void deletePatientAppointments(Patient patient)
    {
        ObservableCollection<Appointment> appointments = AppointmentStorage.getInstance().GetAll();
        List<Appointment> newAppointmentList = new List<Appointment>(AppointmentStorage.getInstance().GetAll());
        if (appointments != null)
            foreach (Appointment ap in appointments)
                if (ap.Patient == patient)
                    appointments.Remove(ap);
    }

    private static void deletePatientExaminations(Patient patient)
    {
        ObservableCollection<Examination> examinations = ExaminationStorage.getInstance().GetAll();
        List<Examination> newExaminationList = new List<Examination>(ExaminationStorage.getInstance().GetAll());
        if (examinations != null)
            foreach (Examination ex in newExaminationList)
                if (ex.Patient == patient)
                    examinations.Remove(ex);
    }

    public void Add(Patient patient)
    {
        this.patients.Add(patient);
    }

    public int GenerateNewID()
    {
        return ((patients.Count - 1) == -1) ? 1 : patients[patients.Count - 1].Id + 1;
    }

    public void serialize()
    {
        var JSONresult = JsonConvert.SerializeObject(patients);
        using (StreamWriter sw = new StreamWriter(FileLocation))
        {
            sw.Write(JSONresult);
            sw.Close();
        }
    }

    public ObservableCollection<Patient> deserialize()
    {
        ObservableCollection<Patient> list = new ObservableCollection<Patient>();
        using (StreamReader sr = new StreamReader(FileLocation))
        {
            list = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(sr.ReadToEnd());
            sr.Close();
        }
        return list;
    }

}