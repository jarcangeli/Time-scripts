using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController current;

    // track elapsed time and format it
    float currTime = 0f;
    DateTime dateTime; 
    public float timeScale = 1f;
    public float lastTimeScale = 1f;

    [SerializeField]
    TextMeshProUGUI timeText = null;

    [SerializeField]
    Rigidbody2D secondHand = null;
    [SerializeField]
    Rigidbody2D minuteHand = null;
    [SerializeField]
    Rigidbody2D hourHand = null;

    private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTimeTrack();
        ChangeTimeScale(timeScale);
    }

    void StartTimeTrack()
    {
        currTime = ReadCurrentHands();
        dateTime = new DateTime();
        dateTime = dateTime.AddSeconds(currTime);
        Debug.Log("Starting at " + currTime);
    }

    public void ChangeTimeScale(float newTimeScale)
    {
        Debug.Log("Changing timescale");
        lastTimeScale = timeScale;
        timeScale = newTimeScale;
        UpdateTimeFlows();
    }

    void UpdateTimeFlows()
    {
        // push the updated timescale to the timeFlow components
        foreach (TimeFlow timeFlow in FindObjectsOfType<TimeFlow>())
        {
            timeFlow.UpdateTimeScale();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = dateTime.ToString("HH:MM:ss"); //$"<mspace={10}>{dateTime.ToString("HH:MM:ss")}</mspace>";

        if (Input.GetKeyDown(KeyCode.T)) 
        { 
            if (timeScale == 10f) ChangeTimeScale(0.5f);
            else ChangeTimeScale(10f);
        }
    }
    void FixedUpdate()
    {
        currTime += Time.fixedDeltaTime * timeScale;
        dateTime = dateTime.AddSeconds(Time.fixedDeltaTime * timeScale);

        RotateHand(secondHand, currTime, 60f);
        RotateHand(minuteHand, currTime, 60f * 60f);
        RotateHand(hourHand, currTime, 12f * 60f * 60f);
    }

    void RotateHand(Rigidbody2D hand, float time, float modulo)
    {
        // rotate clock hand to given time
        float rot = 90f - (time % modulo) / modulo * 360f;
        hand.MoveRotation(Quaternion.Euler(0f, 0f, rot));
    }

    int ReadCurrentHands()
    {
        // read current time of all clock hands in seconds from 00:00
        int hour = (int)(12 * (90f - hourHand.transform.rotation.eulerAngles.z) / 360f);
        int minute = (int)(60 * (90f - minuteHand.transform.rotation.eulerAngles.z) / 360f);
        int seconds = (int)(60 * (90f - secondHand.transform.rotation.eulerAngles.z) / 360f);
        int handTime = seconds + 60 * (minute + 60 * hour);
        return handTime;
    }
}
