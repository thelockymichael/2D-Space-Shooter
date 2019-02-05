using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenu;

    public GameObject GameOverMenu;

    public bool openMenu = false;

    private float gameOverWait = 1.5f;
   // private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
       // UIFaderController = GetComponent<UIFader>();

       // GameObject UIFaderControllerObject = GameObject.FindWithTag("GameOverMenu");
        //UIFaderController = UIFaderControllerObject.GetComponent<UIFader>();

        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
       // PauseMenu.SetActive(true);
       // Time.timeScale = 0.0f;
        if (!openMenu /*&& !GameOverMenu*/)
        {

            openMenu = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            Debug.Log("Menu open");
            //Pause();
        }
        else if (openMenu /*&& GameOverMenu*/)
        {
            openMenu = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            Debug.Log("Menu close");
            //Resume();
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void GameOver()
    {
        StartCoroutine(gameOverDelay());
        //UIFaderController.FadeIn();
    }
    /*
    IEnumerator DoFadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeTime));
            yield return null;
        }

        yield return null;
    }*/

    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(gameOverWait);
        GameOverMenu.SetActive(true);

        
        Time.timeScale = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
