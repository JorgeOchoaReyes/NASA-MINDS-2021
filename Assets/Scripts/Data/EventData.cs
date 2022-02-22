using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using UnityEngine;



// List of events after each experiment
[System.Serializable]
public class EventDataList
{
    public string Name; // name of the EventDataList (it can be the name of the condition) 
    public string Date; // date of when the EventDataList is recorded
    public List<EventData> EventList = new List<EventData>();

    public void Save(string filename)
    {
        string str = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/" + filename, str);
    }

    public void AddEvent(int ButtonID, string Output)
    {
        EventData EV = new EventData();
        EV.ButtonID = ButtonID;
        EV.Output = Output;
        DateTime dt = DateTime.Now;
        EV.Time=dt.ToString("dd/MM/yyyy HH:mm:ss");
        EventList.Add(EV);
    }
}


// Information to record after each event
[System.Serializable]
public class EventData
{
    public int ButtonID; // id of the button that has been pressed
    public string Time; // time at which the event has occurred
    public string Output; // string that is currently spelled out by the user

}



[System.Serializable]
public class HeadInformationList
{
    public string Name;
    public List<HeadInformation> EventList = new List<HeadInformation>();

    public void Save(string filename)
    {
        string str = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/" + filename, str);
    }
}
   

[System.Serializable]
public class HeadInformation
{
    public Vector3 Position;
    public Vector3 Orientation;
    public string Time;
}



