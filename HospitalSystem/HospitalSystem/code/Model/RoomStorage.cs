using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class RoomStorage
    {

        private static RoomStorage instance = null;
        public static RoomStorage getInstance()
        {
            if (instance == null)
            {
                instance = new RoomStorage();
            }
            return instance;
        }

        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private String FileLocation = "../../../Resource/Rooms.json";


        public RoomStorage()
        {
            this.rooms = deserialize();
        }

        public Room GetOne(int Id)
        {
            foreach (Room r in rooms)
                if (Id == r.Id)
                    return r;
            return null;
        }

        public ObservableCollection<Room> GetAll()
        {
            return this.rooms;
        }

        public void Edit(Room room)
        {
            //Patient help = null;
            //foreach (Patient P in this.Patients)
            //    if (patient.Id == P.Id)
            //       help = P;
            //int i = this.Patients.IndexOf(help);
            //this.Patients.Remove(help);
            //this.Patients.Insert(i, patient);
            foreach (Room r in this.rooms)
                if (room.Id == r.Id)
                { 
                    r.Name = room.Name;
                }
        }

        public void Delete(Room room)
        {
            foreach (Room r in rooms)
                if (room.Id == r.Id)
                {
                    this.rooms.Remove(r);
                    break;
                }
        }

        public void Save(Room room)
        {
            this.rooms.Add(room);
        }

        public int GenerateNewID()
        {
            return ((rooms.Count - 1) == -1) ? 1 : rooms[rooms.Count - 1].Id + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(rooms);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Room> deserialize()
        {
            ObservableCollection<Room> list = new ObservableCollection<Room>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Room>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
