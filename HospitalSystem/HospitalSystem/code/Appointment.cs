/***********************************************************************
 * Module:  Appointment.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Appointment
 ***********************************************************************/

using System;

public class Appointment
{
   public String AppointmentID { get; set; }
    public DateTime Time { get; set; }
    public DateTime Date { get; set; }

    public Patient Patient { get; set; }
    public Room Room { get; set; }
    public Doctor Doctor { get; set; }

}