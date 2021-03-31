/***********************************************************************
 * Module:  Pregled.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.Pregled
 ***********************************************************************/

using System;

public class Examination : Appointment
{
    public String ExaminationID 
    { get; set; }

    public Examination()
    {
    }

    public Examination(string examinationID)
    {
        ExaminationID = examinationID;
    }

    //private Report Report;

}