using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERSONTASK : MonoBehaviour
{

    private int TaskID;
    private int PersonID;
    private string TaskName;
    private int ProjectID;

    public PERSONTASK()
    {
    }

    public PERSONTASK(int taskID, int personID, string taskName, int projectID)
    {
        TaskID1 = taskID;
        PersonID1 = personID;
        TaskName1 = taskName;
        ProjectID1 = projectID;
    }

    public int TaskID1
    {
        get
        {
            return TaskID;
        }

        set
        {
            TaskID = value;
        }
    }

    public int PersonID1
    {
        get
        {
            return PersonID;
        }

        set
        {
            PersonID = value;
        }
    }

    public string TaskName1
    {
        get
        {
            return TaskName;
        }

        set
        {
            TaskName = value;
        }
    }

    public int ProjectID1
    {
        get
        {
            return ProjectID;
        }

        set
        {
            ProjectID = value;
        }
    }
}
