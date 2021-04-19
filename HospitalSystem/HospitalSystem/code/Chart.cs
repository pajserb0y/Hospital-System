/***********************************************************************
 * Module:  File.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.File
 ***********************************************************************/

using System;
using System.Collections.Generic;

public class Chart
{
    public System.Collections.ArrayList job;
    private int SocialSecurityNumber;
    private String LastName;
    private String ParentName;
    private String FirstName;
    private int Jmbg;
    private char Gender;
    private DateTime Birth;
    private String Adress;
    private String City;
    private int Telephone;
    private String MarriageStatus;
    private List<Job> JobList;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetJob()
   {
      if (job == null)
         job = new System.Collections.ArrayList();
      return job;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetJob(System.Collections.ArrayList newJob)
   {
      RemoveAllJob();
      foreach (Job oJob in newJob)
         AddJob(oJob);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddJob(Job newJob)
   {
      if (newJob == null)
         return;
      if (this.job == null)
         this.job = new System.Collections.ArrayList();
      if (!this.job.Contains(newJob))
         this.job.Add(newJob);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveJob(Job oldJob)
   {
      if (oldJob == null)
         return;
      if (this.job != null)
         if (this.job.Contains(oldJob))
            this.job.Remove(oldJob);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllJob()
   {
      if (job != null)
         job.Clear();
   }

   

}