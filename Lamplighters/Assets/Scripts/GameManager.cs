using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float countdown;
    public bool dayNight;
    public GameObject sunlight;
    public GameObject lamp;
    private Lamp L;

    public bool mainMenu;
    public bool paused;
    public bool failure;

    public TextMeshProUGUI timer;

    void Start()
    {
        L = lamp.GetComponent<Lamp>();
    }

    void Update()
    {
        if (L.hit) countdown = 30.1f;
        if (!paused && !mainMenu) countdown = Mathf.Clamp(countdown - Time.deltaTime, 0, 30);
        if (countdown == 0) failure = true;

        timer.text = "0:" + Mathf.Floor(countdown);
        if (mainMenu) timer.text = "";

        if (dayNight) {
            sunlight.transform.eulerAngles = new Vector3((30 - countdown) * 6, 0, 0);
        } else {
            sunlight.transform.eulerAngles = new Vector3((60 - countdown) * 6, 0, 0);
        }
    }

    //MENU BUTTONS
    public void Begin()
    {
        countdown = 30.1f;
        dayNight = true;
        paused = false;
        mainMenu = false;
        failure = false;
    }

    public void Resume()
    {
        paused = false;
        mainMenu = false;
    }

    public void Pause()
    {
        paused = true;
        mainMenu = false;
    }

    public void Exit()
    {
        if (mainMenu) Application.Quit();
        else {
            mainMenu = true;
            failure = false;
            dayNight = true;
        }
    }
}