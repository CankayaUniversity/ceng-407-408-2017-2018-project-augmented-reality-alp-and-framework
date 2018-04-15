/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;
using System.Data;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    //private DataLoader dl = new DataLoader();
    private DatabaseProcessor database = new DatabaseProcessor();
    //orientationMode
    public RawImage image;
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;
    //orientationMode
    public Text txtName;
    public Text txtTitle;
    public Text txtDepartment;
    public Text txtPersonalInfo;
    public Text txtSpeciality;
    public Text txtDevTeam;

    //systemDashboardTask
    public Text txtTaskCode;
    public Text txtStatus;
    public Text txtTaskName;
    public Text txtTaskDefinition;
    public Text txtAssignees;

    private int personIDForOperationMode = -1;
    private int taskIDForMeetingMode = -1;

    //operation
    public Text txtCurrentTasks;
    public Text txtSharedTasks;

    public Toggle button;
    public int counter = 0;

    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        //dl.Start();
        database.Connect();
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "OrientationMode")
        {
            DataTable myDT;
            string generatedQuery;
            PERSON foundedPerson = new PERSON();

            generatedQuery = foundedPerson.generatePersonQuery(mTrackableBehaviour.TrackableName);
            myDT = database.GetData(generatedQuery);
            displayOrientationData(myDT);
        }

        else if (scene.name == "SystemDashboard")
        {
            DataTable mySysDashDT;
            string generatedQueryWithPerson;
            string generatedQueryWithTask;

            TASK foundedTask = new TASK();
            PERSON foundedPerson = new PERSON();

            generatedQueryWithPerson = foundedPerson.generatePersonQuery(mTrackableBehaviour.TrackableName);
            generatedQueryWithTask = foundedTask.generateTaskQuery(mTrackableBehaviour.TrackableName);

            mySysDashDT = database.GetData(generatedQueryWithPerson);
            if (mySysDashDT.Rows.Count>0)
            {
                foreach(DataRow row in mySysDashDT.Rows)
                {
                    this.personIDForOperationMode = int.Parse(row["PersonID"].ToString());
                    break;
                }
                if(this.personIDForOperationMode!=-1)
                {
                    displayOperationData(this.personIDForOperationMode);
                }
                
            }
            else
            {
                mySysDashDT = database.GetData(generatedQueryWithTask);
                foreach (DataRow row in mySysDashDT.Rows)
                {
                    this.taskIDForMeetingMode = int.Parse(row["TaskID"].ToString());
                    break;
                }
                if (this.taskIDForMeetingMode != -1)
                {
                    displayTaskData(mTrackableBehaviour.TrackableName);
                }
            }           
        }
    }

    public void displayOrientationData(DataTable person)
    {
     
        foreach (DataRow row in person.Rows)
        {
            this.txtName.text = row["Name"].ToString() + " " + row["Surname"].ToString();
            this.txtTitle.text = row["Title"].ToString();
            this.txtDepartment.text = row["Department"].ToString();
            this.txtPersonalInfo.text = row["PersonalInfo"].ToString();
            this.txtSpeciality.text = row["Speciality"].ToString();
            this.txtDevTeam.text = row["Team"].ToString();
        }

    }

    public void displayOperationData(int personID) //AR fotoðrafý okunan kiþinin id'si geliyor
    {
        int denemeA=1;
        PERSONTASK myPersonTask = new PERSONTASK();
        DataTable currentTasksDT;
        DataTable sharedTasksDT;

        currentTasksDT = database.GetData(myPersonTask.generatePersonTaskQueryAccordingToPerson(personID));
        this.txtCurrentTasks.text = "";
        foreach (DataRow row in currentTasksDT.Rows)
        {
            this.txtCurrentTasks.text += "\n";
            this.txtCurrentTasks.text += row["TaskName"].ToString();
        }

        sharedTasksDT = database.GetData(myPersonTask.generatePersonTaskQueryAccordingToTaskSharing(denemeA, personID));
        this.txtSharedTasks.text = "";
        foreach (DataRow row in sharedTasksDT.Rows)
        {
            this.txtSharedTasks.text += "\n";
            this.txtSharedTasks.text += row["TaskName"].ToString();
        }
    }

    public void displayTaskData(string uniquePhotoName) //AR fotoðrafý okunan taskýn id'si geliyor
    {
        TASK foundTask = new TASK();
        PERSONTASK personTaskForAssignees = new PERSONTASK();
        DataTable taskInfoDT;
        DataTable taskAssigneesDT;

        taskInfoDT = database.GetData(foundTask.generateTaskQuery(uniquePhotoName));
        foreach (DataRow row in taskInfoDT.Rows)
        {
            this.txtTaskCode.text = row["TaskCode"].ToString();
            this.txtTaskName.text = row["TaskName"].ToString();
            this.txtStatus.text = row["TaskProgress"].ToString();
            this.txtTaskDefinition.text = row["TaskDetail"].ToString();
        }

        taskAssigneesDT = database.GetData(personTaskForAssignees.generatePersonTaskQueryAccordingToTask(this.taskIDForMeetingMode));
        this.txtAssignees.text = "";
        foreach (DataRow row in taskAssigneesDT.Rows)
        {
            this.txtAssignees.text += "\n";
            this.txtAssignees.text += row["Name"].ToString();
            this.txtAssignees.text += " ";
            this.txtAssignees.text += row["Surname"].ToString();
        }
    }

    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
        if (videoPlayer != null && videoPlayer.isPlaying)
            stopVideo();
    }

    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        //while (videoPlayer.isPlaying)
        //{
        //    Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
        //    yield return null;
        //}

        Debug.Log("Done Playing Video");
    }

    public void stopVideo()
    {
        //videoPlayer = gameObject.AddComponent<VideoPlayer>();
        //audioSource = gameObject.AddComponent<AudioSource>();
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            audioSource.Stop();
        }
        counter = 0;
    }

    public void playPauseVideo()
    {
        counter++;

        if(counter==1)
        {
            StartCoroutine(playVideo());
        }

        else if (counter % 2 == 0)
        {
            videoPlayer.Pause();
            audioSource.Pause();
            button.isOn = true;
        }

        else
        {
            videoPlayer.Play();
            audioSource.Play();
            button.isOn = false;
        }
    }

    #endregion // PRIVATE_METHODS
}
