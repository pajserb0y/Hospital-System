/***********************************************************************
 * Module:  AppointmentStorage.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.AppointmentStorage
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class AppointmentStorage
{
   public Appointment GetOne(int appointmentID)
   {
      // TODO: implement
      return null;
   }
   
   public List<Appointment> GetAll()
   {
      // TODO: implement
      return null;
   }
   
   public void Edit(Appointment a)
   {
      // TODO: implement
   }
   
   public void Cancel(Appointment a)
   {
      // TODO: implement
   }
   
   public void Add(Appointment a)
   {
      // TODO: implement
   }

   private ObservableCollection<Appointment> Appointments;
   private String FileLocation;

   public ObservableCollection<Appointment> _Appointments
   {
      get
      {
         return Appointments;
      }
      set
      {
         if (this.Appointments != value)
            this.Appointments = value;
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