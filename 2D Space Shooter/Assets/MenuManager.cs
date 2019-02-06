using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public Image image;



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
    }
    public void Story()
    {

        //SceneManager.LoadScene("game");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
