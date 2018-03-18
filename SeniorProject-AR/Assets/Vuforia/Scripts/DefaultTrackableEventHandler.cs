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

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private DataLoader dl = new DataLoader();

    public RawImage image;
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

    public Text txtName;
    public Text txtTitle;
    public Text txtDepartment;
    public Text txtPersonalInfo;
    public Text txtSpeciality;
    public Text txtDevTeam;
    public Text txtCurrentTasks;

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

        dl.Start();
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

        counter = 0;

        int indexOfPerson = -1;

        int i = 0;
        foreach (DataRow row in dl.dtPERSON.Rows)
        {
            if (row["ARFotoName"].ToString().Equals(mTrackableBehaviour.TrackableName))
            {
                indexOfPerson = i;
                break;
            }
            i++;
        }

        this.txtName.text = dl.myPeople[indexOfPerson].Name1 + " " + dl.myPeople[indexOfPerson].Surname1;
        this.txtTitle.text = dl.myPeople[indexOfPerson].Title1;
        this.txtDepartment.text = dl.myPeople[indexOfPerson].Department1;
        this.txtPersonalInfo.text = dl.myPeople[indexOfPerson].PersonalInfo1;
        this.txtSpeciality.text = dl.myPeople[indexOfPerson].Speciality1;
        this.txtDevTeam.text = dl.myPeople[indexOfPerson].Team1;
        this.txtCurrentTasks.text = "";

        int ind = 0;
        foreach (DataRow row in dl.dtPERSONTASK.Rows)
        {
            int persID = int.Parse(row["PersonID"].ToString());
            if (persID==dl.myPeople[indexOfPerson].PersonID1)
            {
                if (ind==0)
                {
                    this.txtCurrentTasks.text = this.txtCurrentTasks.text + row["TaskName"].ToString();
                }
                else
                {
                    this.txtCurrentTasks.text = this.txtCurrentTasks.text + "\n" + row["TaskName"].ToString();
                }
            }
            ind++;
        }

        //StartCoroutine(playVideo());
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
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

    public void stopVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();
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
