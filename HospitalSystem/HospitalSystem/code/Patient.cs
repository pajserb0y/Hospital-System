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

    [JsonProperty] private String Id { get; set; }
    [JsonProperty] private String FirstName { get; set; }
    [JsonProperty] private String LastName { get; set; }
    [JsonProperty] private int Jmbg { get; set; }
    [JsonProperty] private String Adress { get; set; }
    [JsonProperty] private int Telephone { get; set; }

    public Patient(string id, string firstName, string lastName, int jmbg, string adress, int telephone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Jmbg = jmbg;
        Adress = adress;
        Telephone = telephone;
    }

    public Patient(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public Patient()
    {
    }

    public override string ToString()
    {
        return Id + " " + FirstName + " " + LastName;
    }
}