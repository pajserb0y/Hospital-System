/***********************************************************************
 * Module:  ExaminationStorage.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.ExaminationStorage
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

public class ExaminationStorage
{
    private static ExaminationStorage instance = null;
    public static ExaminationStorage getInstance()
    {
        if (instance == null)
        {
            instance = new ExaminationStorage();
        }
        return instance;
    }

    private ObservableCollection<Examination> examinations;
    public ObservableCollection<Examination> Examinations
    {
        get { return examinations; }
        set { examinations = value; }
    }

    private String FileLocation = "BAZE\\Pregledi.json";


    public ExaminationStorage()
    {
        this.examinations = deserialize();
    }

    public Examination GetOne(int Id)
    {
        foreach (Examination e in examinations)
            if (Id == e.Id)
                return e;
        return null;
    }

    public ObservableCollection<Examination> GetAll()
    {
        return examinations;
    }

    public void Edit(Examination exam)
    {
        //Patient help = null;
        //foreach (Patient P in this.Patients)
        //    if (patient.Id == P.Id)
        //       help = P;
        //int i = this.Patients.IndexOf(help);
        //this.Patients.Remove(help);
        //this.Patients.Insert(i, patient);
        foreach (Examination e in this.examinations)
            if (exam.Id == e.Id)
            {
                e.Doctor = exam.Doctor;
                e.Patient = exam.Patient;
                e.Room = exam.Room;
            }
    }

    public void Delete(Examination exam)
    {
        foreach (Examination e in examinations)
            if (exam.Id == e.Id)
            {
                this.examinations.Remove(e);
                break;
            }
    }

    public void Save(Examination exam)
    {
        this.examinations.Add(exam);
    }

    public int GenerateNewID()
    {
        if(examinations == null)
            return 1;
        return ((examinations.Count - 1) == -1) ? 1 : examinations[examinations.Count - 1].Id + 1;
    }

    public void serialize()
    {
        var JSONresult = JsonConvert.SerializeObject(examinations);
        using (StreamWriter sw = new StreamWriter(FileLocation))
        {
            sw.Write(JSONresult);
            sw.Close();
        }
    }

    public ObservableCollection<Examination> deserialize()
    {
        ObservableCollection<Examination> list = new ObservableCollection<Examination>();
        using (StreamReader sr = new StreamReader(FileLocation))
        {
            list = JsonConvert.DeserializeObject<ObservableCollection<Examination>>(sr.ReadToEnd());
            sr.Close();
        }
        return list;
    }
}