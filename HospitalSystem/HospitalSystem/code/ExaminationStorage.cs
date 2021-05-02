/***********************************************************************
 * Module:  ExaminationStorage.cs
 * Author:  Marko
 * Purpose: Definition of the Class Doctor.ExaminationStorage
 ***********************************************************************/

using HospitalSystem.code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

public class ExaminationStorage
{
    private static ExaminationStorage instance = null;
    public static ExaminationStorage getInstance()
    {
        if (instance == null)
        {
            instance = new ExaminationStorage();
        }
        return instance;
    }

    private ObservableCollection<Examination> examinations;
    public ObservableCollection<Examination> Examinations
    {
        get { return examinations; }
        set { examinations = value; }
    }

    private String FileLocation = "../../../Resource/Pregledi.json";


    public ExaminationStorage()
    {
        this.examinations = deserialize();
    }

    public Examination GetOne(int Id)
    {
        foreach (Examination e in examinations)
            if (Id == e.Id)
                return e;
        return null;
    }

    public ObservableCollection<Examination> GetAll()
    {
        return examinations;
    }

    public void Edit(Examination exam)
    {
        foreach (Examination e in this.examinations)
            if (exam.Id == e.Id)
            {
                e.Doctor = exam.Doctor;
                e.Patient = exam.Patient;
                e.Room = exam.Room;
                e.Date = exam.Date;
                e.Time = exam.Time;
            }
    }

    public void Delete(Examination exam)
    {
        foreach (Examination e in examinations)
            if (exam.Id == e.Id)
            {
                this.examinations.Remove(e);
                break;
            }
    }

    public void Add(Examination exam)
    {
        this.examinations.Add(exam);
    }

    public int GenerateNewID()
    {
        if(examinations == null)
            return 1;
        return ((examinations.Count - 1) == -1) ? 1 : examinations[examinations.Count - 1].Id + 1;
    }

    public void serialize()
    {
        List<ExaminationViewModel> exams = new List<ExaminationViewModel>();
        foreach (Examination e in examinations)
        {
            ExaminationViewModel temp = new ExaminationViewModel();
            temp.Id = e.Id;
            temp.DoctorId = e.Doctor.Id;
            temp.PatientId = e.Patient.Id;
            temp.RoomId = e.Room.Id;
            temp.Date = e.Date;
            temp.Time = e.Time;
            temp.IsOperation = e.IsOperation;
            exams.Add(temp);
        }
        var JSONresult = JsonConvert.SerializeObject(exams);
        using (StreamWriter sw = new StreamWriter(FileLocation))
        {
            sw.Write(JSONresult);
            sw.Close();
        }
    }

    public ObservableCollection<Examination> deserialize()
    {
        List<ExaminationViewModel> examsIds = new List<ExaminationViewModel>();
        ObservableCollection<Examination> exams = new ObservableCollection<Examination>();
        using (StreamReader sr = new StreamReader(FileLocation))
        {
            
            examsIds = JsonConvert.DeserializeObject<List<ExaminationViewModel>>(sr.ReadToEnd());
            if (examsIds == null)
                 examsIds = new List<ExaminationViewModel>();
            sr.Close();
        }
        foreach (ExaminationViewModel e in examsIds)
        {
            Examination temp = new Examination();
            temp.Id = e.Id;
            temp.Doctor = DoctorStorage.getInstance().GetOne(e.DoctorId);
            temp.Patient = PatientsStorage.getInstance().GetOne(e.PatientId);
            temp.Room = RoomStorage.getInstance().GetOne(e.RoomId);
            temp.Date = e.Date;
            temp.Time = e.Time;
            temp.IsOperation = e.IsOperation;
            exams.Add(temp);
        }
        return exams;
    }
}

internal class ExaminationViewModel
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
            }
        }
    }
    private DateTime time;
    public DateTime Time {
        get { return time; }
        set
        {
            if (time != value)
            {
                time = value;
            }
        }
    }

    private DateTime date;
    public DateTime Date {
        get { return date; }
        set
        {
            if (date != value)
            {
                date = value;
            }
        }
    }

    private int patientId;
    public int PatientId {
        get { return patientId; }
        set
        {
            if (patientId != value)
            {
                patientId = value;
            }
        }
    }

    private int roomId;
    public int RoomId {
        get { return roomId; }
        set
        {
            if (roomId != value)
            {
                roomId = value;
            }
        }
    }

    private int doctorId;
    public int DoctorId {
        get { return doctorId; }
        set
        {
            if (doctorId != value)
            {
                doctorId = value;
            }
        }
    }
    private bool isOperation;
    public bool IsOperation
    {
        get { return isOperation; }
        set
        {
            if (isOperation != value)
            {
                isOperation = value;
            }
        }
    }
}