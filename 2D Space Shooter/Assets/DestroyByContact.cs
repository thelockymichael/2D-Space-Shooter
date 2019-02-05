using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    //Health
    public int healthBonus = 20;

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private PauseMenuManager PauseMenuManager;
    private PlayerController playerController;

    public bool isPowerUpHealth;
    public bool isFirePower;

    public int attackDamage = 10;               // The amount of health taken away per attack.

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject PauseMenuManagerObject = GameObject.FindWithTag("MainMenuManager");
        PauseMenuManager = PauseMenuManagerObject.GetComponent<PauseMenuManager>();

        GameObject PlayerMovementObject = GameObject.FindWithTag("Player");
        playerController = PlayerMovementObject.GetComponent<PlayerController>();

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

        if (other.tag == "Player" && isPowerUpHealth)
        {
            playerController.GainHealth(healthBonus);
            Destroy(this.gameObject);
            /*
            gameController.GameOver();
            PauseMenuManager.GameOver();
            Destroy(other.gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);*/
        }

        if (other.tag == "Player" && isFirePower)
        {
            playerController.GainFirePower();
            Destroy(this.gameObject);
            /*
            gameController.GameOver();
            PauseMenuManager.GameOver();
            Destroy(other.gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);*/
        }

        if (other.tag == "Player")
        {
            playerController.TakeDamage(attackDamage);
            Destroy(this.gameObject);
            /*
            gameController.GameOver();
            PauseMenuManager.GameOver();
            Destroy(other.gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);*/
        }
        else
        {
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
       
    }
}