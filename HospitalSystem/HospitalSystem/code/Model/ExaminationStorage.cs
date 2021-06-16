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

    private String FileLocation = "../../../Resource/Examinations.json";


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
                e.Anamnesis = exam.Anamnesis;
                e.Prescriptions = exam.Prescriptions;
                e.Notes = exam.Notes;
            }
        serialize();
    }

    public void Delete(Examination exam)
    {
        foreach (Examination e in examinations)
            if (exam.Id == e.Id)
            {
                this.examinations.Remove(e);
                break;
            }
        serialize();
    }

    public void Add(Examination exam)
    {
        this.examinations.Add(exam);
        serialize();
    }

    public int GenerateNewID()
    {
        if(examinations == null)
            return 1;
        return (examinations.Count == 0) ? 1 : examinations[examinations.Count - 1].Id + 1;
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
            temp.TimeOfCreation = e.TimeOfCreation;
            temp.TimesChanged = e.TimesChanged;
            temp.Anamnesis = e.Anamnesis;
            temp.Prescriptions = e.Prescriptions;
            temp.Notes = e.Notes;
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
            temp.Patient = PatientCRUDMenager.getInstance().GetOne(e.PatientId);
            temp.Room = RoomStorage.getInstance().GetOne(e.RoomId);
            temp.Date = e.Date;
            temp.Time = e.Time;
            temp.IsOperation = e.IsOperation;
            temp.TimeOfCreation = e.TimeOfCreation;
            temp.TimesChanged = e.TimesChanged;
            temp.Anamnesis = e.Anamnesis;
            temp.Prescriptions = e.Prescriptions;
            temp.Notes = e.Notes;
            exams.Add(temp);
        }
        return exams;
    }

    public int GenerateNewPrescriptionID(Examination currentExam)
    {
        return (currentExam.Prescriptions.Count == 0 /*|| currentExam.Prescriptions == null*/) ? 1 : currentExam.Prescriptions[currentExam.Prescriptions.Count - 1].Id + 1;
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

    private Anamnesis anamnesis;

    public Anamnesis Anamnesis
    {
        get { return anamnesis; }
        set
        {
            if (value != anamnesis)
            {
                anamnesis = value;
            }
        }
    }

    private ObservableCollection<Prescription> prescriptions;

    public ObservableCollection<Prescription> Prescriptions
    {
        get { return prescriptions; }
        set
        {
            if (prescriptions != value)
            {
                prescriptions = value;
            }
        }
    }
    private int timesChanged;

    public int TimesChanged
    {
        get { return timesChanged; }
        set 
        {
            if (timesChanged != value)
            {
                timesChanged = value;
            }
        }
    }

    private DateTime timeOfCreation;

    public DateTime TimeOfCreation
    {
        get { return timeOfCreation; }
        set 
        {
            if (timeOfCreation != value)
            {
                timeOfCreation = value;
            }
        }
    }

    private string notes = "";

    public string Notes
    {
        get { return notes; }
        set
        {
            if (value != notes)
            {
                notes = value;
            }
        }
    }
}