/***********************************************************************
 * Module:  Patient.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.Patient
 ***********************************************************************/

using Newtonsoft.Json;
using System;

public class Patient
{
    //public File file;

    [JsonProperty] private String Id;
    [JsonProperty] private String FirstName;
    [JsonProperty] private String LastName;
    [JsonProperty] private int Jmbg;
    [JsonProperty] private String Adress;
    [JsonProperty] private int Telephone;

    public Patient(string id, string firstName, string lastName, int jmbg, string adress, int telephone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        Telephone = telephone;
    }

    public override string ToString()
    {
        return Id + " " + FirstName + " " + LastName;
    }
}