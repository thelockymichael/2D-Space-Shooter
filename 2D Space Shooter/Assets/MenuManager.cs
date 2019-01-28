using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
  



   // private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
       // UIFaderController = GetComponent<UIFader>();

       // GameObject UIFaderControllerObject = GameObject.FindWithTag("GameOverMenu");
        //UIFaderController = UIFaderControllerObject.GetComponent<UIFader>();

   
  

  
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1.0f;

    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
