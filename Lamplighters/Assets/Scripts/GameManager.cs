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
    public bool helping;
    public bool failure;
    public bool gaming;

    public GameObject menu;
    public GameObject pause;
    public GameObject help;
    public GameObject lose;
    public GameObject inGame;
    public TextMeshProUGUI timer;

    void Start()
    {
        L = lamp.GetComponent<Lamp>();
        booleans(true, false, false, false);
    }

    void Update()
    {
        //If not in any menu, you're playing the game
        gaming = (!paused && !mainMenu && !failure && !helping);
        if (L.hit) countdown = 30.1f;
        if (gaming) {
            countdown = Mathf.Clamp(countdown - Time.deltaTime, 0, 30);
            if (countdown == 0) failure = true;
        }

        //Timer and Sunset/rise
        timer.text = "0:" + Mathf.Floor(countdown);
        sunlight.transform.eulerAngles = new Vector3(((dayNight ? 30 : 60) - countdown) * 6, 0, 0);

        //Show correct menus
        menu.SetActive(mainMenu);
        help.SetActive(helping);
        pause.SetActive(paused);
        lose.SetActive(failure);
        inGame.SetActive(gaming);
    }

    //MENU BUTTONS
    public void Begin()
    {
        countdown = 30.1f;
        dayNight = true;
        L.hit = false;
        L.onOff = false;
        booleans(false, false, false, false);
    }

    public void Resume()
    {
        booleans(false, false, false, false);
    }

    public void Pause()
    {
        booleans(false, false, true, false);
    }

    public void Rules()
    {
        booleans(false, true, false, false);
    }

    public void Exit()
    {
        if (mainMenu) Application.Quit();
        else {
            booleans(true, false, false, false);
            dayNight = true;
        }
    }

    private void booleans(bool M, bool H, bool P, bool F)
    {
        mainMenu = M;
        helping = H;
        paused = P;
        failure = F;
    }
}