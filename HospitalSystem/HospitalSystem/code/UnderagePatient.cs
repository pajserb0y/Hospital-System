/***********************************************************************
 * Module:  UnderagePatient.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.UnderagePatient
 ***********************************************************************/

using System;

public class UnderagePatient : Patient
{
    private String ParentFirstName;
    private String ParentLastName;
    private int ParentJMBG;

    public UnderagePatient(string id, string firstName, string lastName, int jmbg, string adress, int telephone) : base(id, firstName, lastName, jmbg, adress, telephone)
    {

    }
}