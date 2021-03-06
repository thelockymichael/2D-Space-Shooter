﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using InControl;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour
{
    // Fire Power Variables
    private bool timeOut;
    private float timer;
    private float timeAgain = 10f;

    private float innerTimer;

    public float timeLimit;
    public float aikaLoppuuLimit = 10f;

    public bool FirePowerIsActive = false;

    //Health

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    public float speed = 5f;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;

    private float nextFire;

    private Rigidbody rb;

    public GameObject playerExplosion;

    // Audio
    private AudioSource audio;
    private GameController gameController;
    private PauseMenuManager PauseMenuManager;

    private void Awake()
    {
        // Set the initial health of the player.
        currentHealth = startingHealth;

    }
    // Start is called before the first frame update
    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject PauseMenuManagerObject = GameObject.FindWithTag("MainMenuManager");
        PauseMenuManager = PauseMenuManagerObject.GetComponent<PauseMenuManager>();

        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        // playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    public void GainHealth(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth += amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        // playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        /* if (currentHealth <= 0 && !isDead)
         {
             // ... it should die.
             Death();
         }*/
    }

    public void GainFirePower()
    {
        FirePowerIsActive = true;

        // Set the damaged flag so the screen will flash.
        //damaged = true;

        // Reduce the current health by the damage amount.
        // currentHealth += amount;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        // playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        /* if (currentHealth <= 0 && !isDead)
         {
             // ... it should die.
             Death();
         }*/
    }



    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        gameController.GameOver();
        PauseMenuManager.GameOver();
        Instantiate(playerExplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
        // Turn off any remaining shooting effects.
        //        playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        //      anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //    playerAudio.clip = deathClip;
        //  playerAudio.Play();

        // Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(10.0f); // the number corresponds to the nuber of seconds the speed up will be applied
        TimeOut();
    }


    void TimeOut()
    {
        //timeAgain = timeLimit;
        FirePowerIsActive = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timeOut = false;
        Debug.Log(Mathf.Round(timeLimit));
    }


    private void Update()
    {

       

        if (FirePowerIsActive)
        {
            StartCoroutine(StopSpeedUp());
        }
    

        /*
        if (FirePowerIsActive)
        {
           
            Debug.Log(Mathf.Round(timer));
            timer += Time.deltaTime;
            innerTimer -= Time.deltaTime;

            if (timer > timeLimit)
            {
                timeOut = true;
            }

            if (timer > aikaLoppuuLimit)
            {
                //ShowText();
            }
        }*/
        

        

        var InputDevice = InputManager.ActiveDevice;

        if (InputDevice.Action1.WasPressed && Time.time > nextFire && !FirePowerIsActive)
        {
            nextFire = Time.time + fireRate;

           
                Instantiate(shot, shotSpawns[0].position, shotSpawns[0].rotation);
            
            audio.Play();

            // Debug.Log(audio);

        }

        else if (InputDevice.Action1.WasPressed && Time.time > nextFire && FirePowerIsActive)
        {
            nextFire = Time.time + fireRate;


            foreach (var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            audio.Play();
            // Debug.Log(audio);
        }

        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var InputDevice = InputManager.ActiveDevice;

        float moveHorizontal = InputDevice.LeftStickX;
        float moveVertical = InputDevice.LeftStickY;


       // float moveHorizontal = Input.GetAxis("Horizontal");
      //  float moveVertical = Input.GetAxis("Vertical");

       Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
