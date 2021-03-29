/***********************************************************************
 * Module:  Room.cs
 * Author:  Hope
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;

public class Room
{

   public long GetId()
   {
      // TODO: implement
      return 0;
   }
   
   public long SetId(long id)
   {
      // TODO: implement
      return 0;
   }
   
   public String GetName()
   {
      // TODO: implement
      return null;
   }
   
   public String SetName(String name)
   {
      // TODO: implement
      return null;
   }

   public System.Collections.ArrayList equipment;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetEquipment()
   {
      if (equipment == null)
         equipment = new System.Collections.ArrayList();
      return equipment;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetEquipment(System.Collections.ArrayList newEquipment)
   {
      RemoveAllEquipment();
      foreach (Equipment oEquipment in newEquipment)
         AddEquipment(oEquipment);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddEquipment(Equipment newEquipment)
   {
      if (newEquipment == null)
         return;
      if (this.equipment == null)
         this.equipment = new System.Collections.ArrayList();
      if (!this.equipment.Contains(newEquipment))
         this.equipment.Add(newEquipment);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveEquipment(Equipment oldEquipment)
   {
      if (oldEquipment == null)
         return;
      if (this.equipment != null)
         if (this.equipment.Contains(oldEquipment))
            this.equipment.Remove(oldEquipment);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllEquipment()
   {
      if (equipment != null)
         equipment.Clear();
   }
   public RoomType roomType;

    public Room()
    {
    }

    public Room(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public  long Id { get; set; }
   public String Name { get; set; }

}