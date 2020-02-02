using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] gameOverObjects;
	GameObject[] startGameObjects;

	public GameObject player1;
	public GameObject player2;
	public GameObject gameOverRestartButton;
	
    public static bool gameStarted = false;

    private List<GameObject> activePlayers;

	// Use this for initialization
	void Start () 
    {
        activePlayers = new List<GameObject>();
		startGameObjects = GameObject.FindGameObjectsWithTag("ShowOnGameStart");
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		gameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
		hidePaused();
        hideGameOver();
		showGameStart();
	}

	
	
	// Update is called once per frame
	void Update () {
		
		if(player1 != null && !player1.activeSelf && Gamepad.all.Count > 0 && Gamepad.all[0].buttonSouth.wasPressedThisFrame)
        {
			enablePlayer(player1);
			
			if (!gameStarted)
				hideGameStart();
        }

		if (player2 != null && !player2.activeSelf && Gamepad.all.Count > 1 && Gamepad.all[1].buttonSouth.wasPressedThisFrame)
		{
			enablePlayer(player2);
			
			if (!gameStarted)
				hideGameStart();
		}

        if (gameStarted)
        {
            bool allPlayersDead = true;
            foreach (GameObject player in activePlayers)
            {
                if (player != null)
                    allPlayersDead = false;
            }
            if (allPlayersDead)
            {
                showGameOver();
            }
        }
	}


	//Reloads the Level
	public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//controls the pausing of the scene
	public void pauseControl()
{
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        } else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
	}

	//shows objects with ShowOnPause tag
	public void showPaused()
    {
		foreach(GameObject g in pauseObjects)
        {
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused()
    {
		foreach(GameObject g in pauseObjects)
        {
			if (g != null)
			    g.SetActive(false);
		}
	}

    //shows objects with ShowOnGameOver tag
	public void showGameOver()
    {
		Time.timeScale = 0;
		gameStarted = false;
		foreach(GameObject g in gameOverObjects)
        {
			if (g != null)
                g.SetActive(true);
		}
		EventSystem.current.SetSelectedGameObject(gameOverRestartButton);
	}

	//hides objects with ShowOnGameOver tag
	public void hideGameOver()
    {
		Time.timeScale = 1;
		foreach(GameObject g in gameOverObjects)
        {
			if (g != null)
			    g.SetActive(false);
		}
	}

    public void showGameStart()
    {
		Time.timeScale = 0;
		foreach (GameObject g in startGameObjects)
        {
			if (g != null)
				g.SetActive(true);
        }
        gameStarted = false;
    }

    public void hideGameStart()
    {
		Time.timeScale = 1;
		foreach (GameObject g in startGameObjects)
		{
			if (g != null)
				g.SetActive(false);
		}
        gameStarted = true;
	}

    private void enablePlayer(GameObject obj)
    {
		obj.SetActive(true);
        activePlayers.Add(obj);
    }

	//loads inputted level
	public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
	}
}

