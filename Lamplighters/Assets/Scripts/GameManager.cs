using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Lamp
    public float countdown;
    public bool dayNight;
    public GameObject sunlight;
    public GameObject lamp;
    private Lamp L;

    //Collectables
    public GameObject[] toFindDisplay;
    private int findID;
    private bool holding;
    private GameObject[] collectables;

    //Menus
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
    public TextMeshProUGUI request;

    public bool tutored;
    public GameObject swiper;

    public AudioSource MusicManager;
    public AudioClip wrong;
    private AudioSource jukebox;

    void Start()
    {
        L = lamp.GetComponent<Lamp>();
        booleans(true, false, false, false);
        jukebox = GetComponent<AudioSource>();
        tutored = false;
    }

    void Update()
    {
        //If not in any menu, you're playing the game
        gaming = (!paused && !mainMenu && !failure && !helping);
        if (L.hit) countdown = 31;
        if (gaming) {
            countdown = Mathf.Clamp(countdown - Time.deltaTime, 0, 30);
            if (countdown == 0) failure = true;
        }

        //Timer and Sunset/rise
        if (countdown < 6) {
            timer.color = Color.red;
            timer.text = "0:" + Mathf.Floor(countdown).ToString("00") + "\nTend the Lamp!";
        } else {
            timer.color = Color.grey;
            timer.text = "0:" + Mathf.Floor(countdown).ToString("00");
        }
        sunlight.transform.eulerAngles = new Vector3(((dayNight ? 30 : 60) - countdown) * 6, 0, 0);

        //Request ID
        request.text = "Find Item: " + toFindDisplay[findID].name;

        //Show correct menus
        menu.SetActive(mainMenu);
        help.SetActive(helping);
        pause.SetActive(paused);
        lose.SetActive(failure);
        inGame.SetActive(gaming);

        //Tutorial gif
        swiper.SetActive(!tutored && gaming);

        collectables = GameObject.FindGameObjectsWithTag("Respawn");

        //Music volume
        MusicManager.volume = gaming ? 0.6f : 0.3f;
    }

    public void Selection(GameObject selected, int ID, AudioClip clip)
    {
        //Deselect all other objects
        foreach (GameObject item in collectables) {
            item.GetComponent<Clickable>().selected = false;
            if (item == selected) item.GetComponent<Clickable>().selected = true;
        }

        //Compare IDs
        if (findID == ID) {
            PlaySound(clip);
            NewRequest(ID);
        } else {
            PlaySound(wrong);
        }
    }

    private void NewRequest(int not)
    {
        findID = Random.Range(0, toFindDisplay.Length);
        while (findID == not) findID = Random.Range(0, toFindDisplay.Length);
    }

    //Play clip
    private void PlaySound(AudioClip clip)
    {
        jukebox.PlayOneShot(clip, 1);
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