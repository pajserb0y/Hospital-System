using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HospitalSystem.code.Model
{
    public interface IRoomStorage
    {
        public Room GetOne(int Id);

        public ObservableCollection<Room> GetAll();

        public void Edit(Room room);

        public void Delete(Room room);

        public void Add(Room room);

        public int GenerateNewID();

        public void serialize();

        public ObservableCollection<Room> deserialize();
    }
}
}
