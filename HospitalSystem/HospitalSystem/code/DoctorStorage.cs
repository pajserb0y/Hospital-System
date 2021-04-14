/***********************************************************************
 * Module:  DoctorStorage.cs
 * Author:  Nikola
 * Purpose: Definition of the Class DoctorStorage
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class DoctorStorage
{
   public void GetOne(String patientID)
   {
      // TODO: implement
   }
   
   public List<Doctor> GetAll()
   {
      // TODO: implement
      return null;
   }
   
   public void Edit(Doctor doctor)
   {
      // TODO: implement
   }
   
   public void Delete(Doctor doctor)
   {
      // TODO: implement
   }
   
   public void Add(Doctor doctor)
   {
      // TODO: implement
   }
   
   public int GenerateNewID()
   {
      // TODO: implement
      return 0;
   }
   
   public void Serialize()
   {
      // TODO: implement
   }
   
   public ObservableCollection<Doctor> Deserialize()
   {
      // TODO: implement
      return null;
   }

   private ObservableCollection<Doctor> Doctors;
   private String FileLocation;

   public ObservableCollection<Doctor> _Doctors
   {
      get
      {
         return Doctors;
      }
      set
      {
         if (this.Doctors != value)
            this.Doctors = value;
      }
   }
   
   public String _FileLocation
   {
      get
      {
         return FileLocation;
      }
      set
      {
         if (this.FileLocation != value)
            this.FileLocation = value;
      }
   }

}