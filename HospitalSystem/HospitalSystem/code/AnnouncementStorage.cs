using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class AnnouncementStorage
    {
        private static AnnouncementStorage instance = null;
        public static AnnouncementStorage getInstance()
        {
            if (instance == null)
            {
                instance = new AnnouncementStorage();
            }
            return instance;
        }


        private ObservableCollection<Announcement> announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get { return announcements; }
            set { announcements = value; }
        }

        private String FileLocation = "../../../Resource/Announcements.json";


        public AnnouncementStorage()
        {
            this.announcements = deserialize();
        }

        public ObservableCollection<Announcement> GetAll()
        {
            return announcements;
        }

        
        public void Delete(Announcement announcement)
        {
            foreach (Announcement a in announcements)
                if (announcement == a)
                {
                    this.announcements.Remove(a);
                    break;
                }       
        }

        public void Add(Announcement announcement)
        {
            announcements.Add(announcement);
            serialize();
        }


        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(announcements);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Announcement> deserialize()
        {
            ObservableCollection<Announcement> list = new ObservableCollection<Announcement>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Announcement>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
