/***********************************************************************
 * Module:  ExaminationStorage.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.ExaminationStorage
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class ExaminationStorage
{
   public Examination GetOne(String examinationID)
   {
      // TODO: implement
      return null;
   }
   
   public List<Examination> GetAll()
   {
      // TODO: implement
      return null;
   }
   
   public void Edit(Examination exam)
   {
      // TODO: implement
   }
   
   public void Delete(Examination exam)
   {
      // TODO: implement
   }
   
   public void Add(Examination exam)
   {
      // TODO: implement
   }
   
   public int GenerateNewID()
   {
      // TODO: implement
      return 0;
   }
   
   public int Serialize()
   {
      // TODO: implement
      return 0;
   }
   
   public ObservableCollection<Examination> Deserialize()
   {
      // TODO: implement
      return null;
   }

   private ObservableCollection<Examination> Examinations;
   private String FileLocation;

   public ObservableCollection<Examination> _Examinations
   {
      get
      {
         return Examinations;
      }
      set
      {
         if (this.Examinations != value)
            this.Examinations = value;
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