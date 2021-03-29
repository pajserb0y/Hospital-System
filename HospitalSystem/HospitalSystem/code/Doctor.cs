/***********************************************************************
 * Module:  Lekar.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Lekar
 ***********************************************************************/

using System;

public class Doctor
{
    public Doctor(string id, string firstName, string lastName, int jmbg, string adress, int phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        PhoneNumber = phoneNumber;
    }

    public Doctor(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public void GenerateReport()
   {
      // TODO: implement
   }
   
   public void ViewPatientsChart()
   {
      // TODO: implement
   }
   
   public void GenerateReferralLetter()
   {
      // TODO: implement
   }
   
   public void CancelExamination()
   {
      // TODO: implement
   }
   
   public void GeneratePrescription()
   {
      // TODO: implement
   }
   
   public Appointment NewApointment()
   {
      // TODO: implement
      return null;
   }

    public override string ToString()
    {
        return Id + " " + FirstName + " " + LastName;
    }

    public String Id { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public int Jmbg { get; set; }
    public String Adress { get; set; }
    public int PhoneNumber { get; set; }
    public String specialization { get; set; }
    public DateTime WorkHour { get; set; }

}