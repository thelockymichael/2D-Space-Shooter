﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject storyImage;
    private bool openMenu = false;

    // private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

        storyImage.SetActive(false);
       // UIFaderController = GetComponent<UIFader>();
       // GameObject UIFaderControllerObject = GameObject.FindWithTag("GameOverMenu");
        //UIFaderController = UIFaderControllerObject.GetComponent<UIFader>();
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }
    public void Story()
    {
        storyImage.SetActive(false);

        //SceneManager.LoadScene("game");
    }

    public void Pause()
    {
        // PauseMenu.SetActive(true);
        // Time.timeScale = 0.0f;
        if (!openMenu /*&& !GameOverMenu*/)
        {

            openMenu = true;
            storyImage.SetActive(true);
            Time.timeScale = 0.0f;
            Debug.Log("Menu open");
            //Pause();
        }
        else if (openMenu /*&& GameOverMenu*/)
        {
            openMenu = false;
            storyImage.SetActive(false);
            Time.timeScale = 1.0f;
            Debug.Log("Menu close");
            //Resume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
