using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code.Model
{
    class WorkingShiftStorage
    {
        private static WorkingShiftStorage instance = null;
        public static WorkingShiftStorage getInstance()
        {
            if (instance == null)
            {
                instance = new WorkingShiftStorage();
            }
            return instance;
        }


        private ObservableCollection<WorkingShift> workingShifts = new ObservableCollection<WorkingShift>();
        public ObservableCollection<WorkingShift> WorkingShifts
        {
            get { return workingShifts; }
            set { workingShifts = value; }
        }

        private String FileLocation = "../../../Resource/Shifts.json";


        public WorkingShiftStorage()
        {
            this.workingShifts = deserialize();
        }

        public WorkingShift GetOne(int id)
        {
            foreach (WorkingShift w in workingShifts)
                if (id == w.Id)
                    return w;
            return null;
        }

        public ObservableCollection<WorkingShift> GetAll()
        {
            return workingShifts;
        }

        public void Edit(WorkingShift shift)
        {
            foreach (WorkingShift w in this.workingShifts)
                if (shift.Id == w.Id)
                {
                    w.StartDate = shift.StartDate;
                    w.EndDate = shift.EndDate;
                    w.DoctorId = shift.DoctorId;
                    w.EndTime = shift.EndTime;
                    w.StartTime = shift.StartTime;
                }
        }

        public void Delete(WorkingShift shift)
        {
            foreach (WorkingShift w in workingShifts)
                if (shift.Id == w.Id)
                {
                    this.workingShifts.Remove(w);
                    break;
                }
        }
  
        public void Add(WorkingShift shift)
        {
            this.workingShifts.Add(shift);
        }

        public int GenerateNewID()
        {
            return ((workingShifts.Count - 1) == -1) ? 1 : workingShifts[workingShifts.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(workingShifts);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<WorkingShift> deserialize()
        {
            ObservableCollection<WorkingShift> list = new ObservableCollection<WorkingShift>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<WorkingShift>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
