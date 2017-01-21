using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public enum State
    {
        PressStart, MainMenu, InGame, Pause, Credits
    };

    public Font ourFont;

    private State theState;

    public GameObject[] screens;
    private GameObject mainMenu;
    private GameObject pressStartMenu;
    private GameObject pauseMenu;
    private GameObject HUD;
    private GameObject creditScreen;

    private GameObject[] colorCams;


    public State TheState
    {
        get { return theState; }
        set
        {
            theState = value;

            Debug.Log("State: " + theState.ToString());

            if (theState == State.PressStart)
            {
                Cursor.lockState = CursorLockMode.None;

                DeactivateAll();
                if (pressStartMenu != null) pressStartMenu.SetActive(true);
            }
            else if (theState == State.MainMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                DeactivateAll();
                mainMenu.SetActive(true);
            }
            else if (theState == State.InGame)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                DeactivateAll();
                if (colorCams != null)
                {
                    foreach (GameObject g in colorCams)
                    {
                        g.SetActive(true);
                    }
                }
                if (HUD != null) HUD.SetActive(true);

                Time.timeScale = 1.0f;
            }
            else if (theState == State.Pause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                DeactivateAll();
                colorCams = GameObject.FindGameObjectsWithTag("ColorCam");
                foreach (GameObject c in colorCams)
                {
                    c.SetActive(false);
                }
                pauseMenu.SetActive(true);

                Time.timeScale = 0.0f;
            }
            else if (theState == State.Credits)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                LoadLevel("CreditScene");
            }
            else
            {
                Debug.Log("Unrecognized State: " + value.ToString());
            }
        }
    }

    // Use this for initialization
    void Awake ()
    {
        foreach(Text f in FindObjectsOfType<Text>())
        {
            f.font = ourFont; // Not working for some reason.  Sets all to no font, despite having a font
        }

        screens = GameObject.FindGameObjectsWithTag("Screen");

        if (SceneManager.GetActiveScene().name.CompareTo("MainMenu") == 0)
        {
            Debug.Log("At the Main Menu");
            TheState = State.PressStart;

            foreach (GameObject g in screens)
            {
                if (g.name.CompareTo("PressStart") == 0)
                {
                    pressStartMenu = g;
                    g.SetActive(true);
                }
                else if (g.name.CompareTo("MainMenu") == 0)
                {
                    mainMenu = g;
                    g.SetActive(false);
                }
                else
                {
                    g.SetActive(false);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("FirstLevel") == 0)
        {
            Debug.Log("InGame");
            TheState = State.InGame;

            foreach (GameObject g in screens)
            {
                if (g.name.CompareTo("PauseMenu") == 0)
                {
                    pauseMenu = g;
                    g.SetActive(false);
                }
                else if(g.name.CompareTo("HUD") == 0)
                {
                    HUD = g;
                    g.SetActive(false);
                }
                else
                {
                    g.SetActive(false);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.CompareTo("CreditScene") == 0)
        {
            Debug.Log("Credit Scene");
            TheState = State.Credits;

            foreach (GameObject g in screens)
            {
                if (g.name.CompareTo("CreditScreen") == 0)
                {
                    creditScreen = g;
                    g.SetActive(true);
                    g.GetComponent<Scroll>().StartScroll();
                }
                else
                {
                    g.SetActive(false);
                }
            }
        }

        instance = this;
    }

    // Update is called once per frame
    void Update ()
    {
        if (TheState == State.PressStart && Input.anyKey) TheState = State.MainMenu;

        if (TheState == State.InGame && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) TheState = State.Pause;
        else if (TheState == State.Pause && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) TheState = State.InGame;
    }

    public void Unpause()
    {
        TheState = State.InGame;
    }

    public void StartCredits()
    {
        TheState = State.Credits;
    }

    private void DeactivateAll()
    {
        foreach (GameObject g in screens)
        {
            g.SetActive(false);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
