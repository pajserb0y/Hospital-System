/***********************************************************************
 * Module:  Room.cs
 * Author:  Hope
 * Purpose: Definition of the Class Room
 ***********************************************************************/

using System;
using System.ComponentModel;

public class Room : INotifyPropertyChanged
{
    private int id;
    public int Id
    {
        get { return id; }
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
    }

    private String name;
    public String Name
    {
        get { return name; }
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(String name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
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

    public Room(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public override string ToString()
    {
        return Id.ToString() + " " + Name;
    }

}