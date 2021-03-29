/***********************************************************************
 * Module:  GuestPatient.cs
 * Author:  Nikola
 * Purpose: Definition of the Class Sekretar.GuestPatient
 ***********************************************************************/

public class GuestPatient
{
    private int Jmbg;


    public System.Collections.ArrayList urgentAppointment;

    /// <pdGenerated>default getter</pdGenerated>
    public System.Collections.ArrayList GetUrgentAppointment()
    {
        if (urgentAppointment == null)
            urgentAppointment = new System.Collections.ArrayList();
        return urgentAppointment;
    }

    /// <pdGenerated>default setter</pdGenerated>
    public void SetUrgentAppointment(System.Collections.ArrayList newUrgentAppointment)
    {
        RemoveAllUrgentAppointment();
        foreach (UrgentAppointment oUrgentAppointment in newUrgentAppointment)
            AddUrgentAppointment(oUrgentAppointment);
    }

    /// <pdGenerated>default Add</pdGenerated>
    public void AddUrgentAppointment(UrgentAppointment newUrgentAppointment)
    {
        if (newUrgentAppointment == null)
            return;
        if (this.urgentAppointment == null)
            this.urgentAppointment = new System.Collections.ArrayList();
        if (!this.urgentAppointment.Contains(newUrgentAppointment))
            this.urgentAppointment.Add(newUrgentAppointment);
    }

    /// <pdGenerated>default Remove</pdGenerated>
    public void RemoveUrgentAppointment(UrgentAppointment oldUrgentAppointment)
    {
        if (oldUrgentAppointment == null)
            return;
        if (this.urgentAppointment != null)
            if (this.urgentAppointment.Contains(oldUrgentAppointment))
                this.urgentAppointment.Remove(oldUrgentAppointment);
    }

    /// <pdGenerated>default removeAll</pdGenerated>
    public void RemoveAllUrgentAppointment()
    {
        if (urgentAppointment != null)
            urgentAppointment.Clear();
    }



}