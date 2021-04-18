using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace HospitalSystem.code
{
    class JobStorage
    {
        private static JobStorage instance = null;
        public static JobStorage getInstance()
        {
            if (instance == null)
            {
                instance = new JobStorage();
            }
            return instance;
        }

        private ObservableCollection<Job> jobs = new ObservableCollection<Job>();
        public ObservableCollection<Job> Jobs
        {
            get { return jobs; }
            set { jobs = value; }
        }

        private String FileLocation = "../../../Resource/Jobs.json";

        public JobStorage()
        {
            this.jobs = deserialize();
        }

        public Job GetOne(int jobID)
        {
            foreach (Job p in jobs)
                if (jobID == p.No)
                    return p;
            return null;
        }

        public ObservableCollection<Job> GetAll()
        {
            return jobs;
        }

        public void Edit(Job job)
        {
            foreach (Job P in this.jobs)
                if (job.No == P.No)
                {
                    P.Position = job.Position;
                    P.CompanyName = job.CompanyName;
                    P.RegisterNumber = job.RegisterNumber;
                    P.ActivityCode = job.ActivityCode;
                }
        }

        public void Delete(Job job)
        {
            foreach (Job p in jobs)
                if (job.No == p.No)
                {
                    this.jobs.Remove(p);
                    break;
                }
        }

        public void Save(Job job)
        {
            this.jobs.Add(job);
            serialize();
        }

        public int GenerateNewID(int pid)
        {
            int max = 0;
            foreach (Job j in jobs)
                if (j.PID == pid && j.No > max)
                    max = j.No;
            return max + 1;
        }

        public void serialize()
        {
            var JSONresult = JsonConvert.SerializeObject(jobs);
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                sw.Write(JSONresult);
                sw.Close();
            }
        }

        public ObservableCollection<Job> deserialize()
        {
            ObservableCollection<Job> list = new ObservableCollection<Job>();
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<Job>>(sr.ReadToEnd());
                sr.Close();
            }
            return list;
        }
    }
}
