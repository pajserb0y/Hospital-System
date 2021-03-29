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
        // TODO: implement
        return null;
    }

    public List<Patient> GetAll()
    {
        // TODO: implement
        List<Patient> patients = new List<Patient>();


       

        return patients;
    }

    public void Edit(Patient patient)
    {
        // TODO: implement
    }

    public void Delete(Patient patient)
    {
        // TODO: implement
    }

    public void Save(Patient patient)
    {
        // TODO: implement
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