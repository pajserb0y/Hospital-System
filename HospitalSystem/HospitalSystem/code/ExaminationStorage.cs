/***********************************************************************
 * Module:  ExaminationStorage.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.ExaminationStorage
 ***********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    public ExaminationStorage()
    {
        this.Examinations = new List<Examination>();
    }

    public Examination GetOne(String examinationID)
   {
      // TODO: implement
      return null;
   }
   
   public List<Examination> GetAll()
   {
      // TODO: implement
      return this.Examinations;
   }
   
   public void Delete(Examination exam)
   {
        // TODO: implement
        Examinations.Remove(exam);
   }
   
   public void Edit(Examination exam)
   {
      // TODO: implement
   }
   
   public void Save(Examination exam)
   {
        Examinations.Add(exam);
        string JSONresult = JsonConvert.SerializeObject(exam);
        string path = @"C:\Users\Marko\Desktop\Hospital-System\HospitalSystem\HospitalSystem\Examination.json";
        using (var tw = new StreamWriter(path, true))
        {
            tw.WriteLine(JSONresult.ToString());
            tw.Close();
        }
    }

   private String FileLocation;
    private List<Examination> Examinations;
}