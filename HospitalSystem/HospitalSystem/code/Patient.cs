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

    [JsonProperty] public String Id { get; set; }
    [JsonProperty] public String FirstName{ get; set; }
    [JsonProperty] public String LastName { get; set; }
    [JsonProperty] public int Jmbg { get; set; }
    [JsonProperty] public String Adress { get; set; }
    [JsonProperty] public int Telephone { get; set; }


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