using HospitalSystem.code.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class RoomStorage : IRoomStorage
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
            foreach (Room r in this.rooms)
                if (room.Id == r.Id)
                { 
                    r.Name = room.Name;
                    r.Beds = room.Beds;
                }
            RoomStorage.getInstance().serialize();
        }

        public void Delete(Room room)
        {
            foreach (Room r in rooms)
                if (room.Id == r.Id)
                {
                    this.rooms.Remove(r);
                    break;
                }
            RoomStorage.getInstance().serialize();
        }

        public void Add(Room room)
        {
            this.rooms.Add(room);
            RoomStorage.getInstance().serialize();
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
