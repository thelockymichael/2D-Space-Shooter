using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private PauseMenuManager PauseMenuManager;
       
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject PauseMenuManagerObject = GameObject.FindWithTag("MainMenuManager");
        PauseMenuManager = PauseMenuManagerObject.GetComponent<PauseMenuManager>();

        /*if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.CompareTag ("Enemy"))
        {
            return;
        }

        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            gameController.GameOver();
            PauseMenuManager.GameOver();
            Destroy(other.gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        else
        {
            gameController.AddScore(scoreValue);

            Destroy(gameObject);
        }
       
    }
}