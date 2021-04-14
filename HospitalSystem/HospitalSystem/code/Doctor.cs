/***********************************************************************
 * Module:  Lekar.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Lekar
 ***********************************************************************/

using System;

public class Doctor
{
   private String Id;
   private String Username;
   private String Password;
   private String FirstName;
   private String LastName;
   private int Jmbg;
   private String Adress;
   private int PhoneNumber;
   private String specialization;
   private DateTime WorkHour;
   private Char Gender;

   public String _Username
   {
      get
      {
         return Username;
      }
      set
      {
         if (this.Username != value)
            this.Username = value;
      }
   }
   
   public String _Password
   {
      get
      {
         return Password;
      }
      set
      {
         if (this.Password != value)
            this.Password = value;
      }
   }
   
   public String _FirstName
   {
      get
      {
         return FirstName;
      }
      set
      {
         if (this.FirstName != value)
            this.FirstName = value;
      }
   }
   
   public String _LastName
   {
      get
      {
         return LastName;
      }
      set
      {
         if (this.LastName != value)
            this.LastName = value;
      }
   }
   
   public int _Jmbg
   {
      get
      {
         return Jmbg;
      }
      set
      {
         if (this.Jmbg != value)
            this.Jmbg = value;
      }
   }
   
   public String _Adress
   {
      get
      {
         return Adress;
      }
      set
      {
         if (this.Adress != value)
            this.Adress = value;
      }
   }
   
   public int _PhoneNumber
   {
      get
      {
         return PhoneNumber;
      }
      set
      {
         if (this.PhoneNumber != value)
            this.PhoneNumber = value;
      }
   }
   
   public String Specialization
   {
      get
      {
         return specialization;
      }
      set
      {
         if (this.specialization != value)
            this.specialization = value;
      }
   }
   
   public DateTime _WorkHour
   {
      get
      {
         return WorkHour;
      }
      set
      {
         if (this.WorkHour != value)
            this.WorkHour = value;
      }
   }
   
   public Char _Gender
   {
      get
      {
         return Gender;
      }
      set
      {
         if (this.Gender != value)
            this.Gender = value;
      }
   }
   
   public String _Id
   {
      get
      {
         return Id;
      }
      set
      {
         if (this.Id != value)
            this.Id = value;
      }
   }

}