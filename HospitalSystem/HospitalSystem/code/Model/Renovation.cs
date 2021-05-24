/***********************************************************************
 * Module:  Renovation.cs
 * Author:  Hope
 * Purpose: Definition of the Class Renovation
 ***********************************************************************/

using System;

public class Renovation
{
   public long GetId()
   {
      // TODO: implement
      return 0;
   }
   
   public void SetId(long id)
   {
      // TODO: implement
   }
   
   public String GetDescription()
   {
      // TODO: implement
      return null;
   }
   
   public void SetDescription(String description)
   {
      // TODO: implement
   }
   
   public RenovationStatus GetRenovationStatus()
   {
      // TODO: implement
      return null;
   }
   
   public void SetRenovationStatus(RenovationStatus renovationStatus)
   {
      // TODO: implement
   }
   
   //public DateTime GetStartDate()
   //{
   //   // TODO: implement
   //   return null;
   //}
   
   public void SetStartDate(DateTime startDate)
   {
      // TODO: implement
   }
   
   //public DateTime GetEndDate()
   //{
   //   // TODO: implement
   //   return null;
   //}
   
   public void SetEndDate(DateTime endDate)
   {
      // TODO: implement
   }

   public System.Collections.ArrayList room;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetRoom()
   {
      if (room == null)
         room = new System.Collections.ArrayList();
      return room;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetRoom(System.Collections.ArrayList newRoom)
   {
      RemoveAllRoom();
      foreach (Room oRoom in newRoom)
         AddRoom(oRoom);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddRoom(Room newRoom)
   {
      if (newRoom == null)
         return;
      if (this.room == null)
         this.room = new System.Collections.ArrayList();
      if (!this.room.Contains(newRoom))
         this.room.Add(newRoom);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveRoom(Room oldRoom)
   {
      if (oldRoom == null)
         return;
      if (this.room != null)
         if (this.room.Contains(oldRoom))
            this.room.Remove(oldRoom);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllRoom()
   {
      if (room != null)
         room.Clear();
   }

   private long Id;
   private String Description;
   private RenovationStatus RenovationStatus;
   private DateTime StartDate;
   private DateTime EndDate;

}