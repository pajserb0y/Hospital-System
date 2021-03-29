/***********************************************************************
 * Module:  PatientsStorage.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.PatientsStorage
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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


    private List<Patient> patients;
    private String FileLocation;

    public PatientsStorage()
    {
        this.patients = new List<Patient>();
    }

    public Patient GetOne(String patientID)
    {
        foreach (Patient p in patients)
            if (patientID == p.Id)
                return p;
        return null;
    }

    public List<Patient> GetAll()
    {
        return patients;
    }

    public void Edit(Patient patient)
    {
        foreach (Patient p in patients)
            if (patient.Id == p.Id)
            {
                p.Jmbg = patient.Jmbg;
                p.FirstName = patient.FirstName;
                p.LastName = patient.LastName;
                p.Adress = patient.Adress;
                p.Telephone = patient.Telephone;
            }
    }

    public void Delete(Patient patient)
    {
        foreach (Patient p in patients)
            if (patient.Id == p.Id)
                patients.Remove(p);            
    }

    public void Save(Patient patient)
    {
        this.patients.Add(patient);
 

        string JSONresult = JsonConvert.SerializeObject(patient);
        string path = @"E:\Fax\Projekti\SIMS\Hospital-System\HospitalSystem.json";
        using (var tw = new StreamWriter(path, true))
        {
            tw.WriteLine(JSONresult.ToString());
            tw.Close();
        }
    }

    public void TransferPatient(Patient patient, String newLocation)
    {
        // TODO: implement
    }



}