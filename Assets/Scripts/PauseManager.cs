﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] gameOverObjects;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		gameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
		hidePaused();
        hideGameOver();
	}

	// Update is called once per frame
	void Update () {

		//uses the p button to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.Escape))
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
		foreach(GameObject g in gameOverObjects)
        {
			if (g != null)
                g.SetActive(true);
		}
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

	//loads inputted level
	public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
	}
}
