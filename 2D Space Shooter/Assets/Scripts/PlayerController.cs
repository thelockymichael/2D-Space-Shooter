using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using InControl;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour
{
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
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private Rigidbody rb;


    // Audio
    private AudioSource audio;

    private void Awake()
    {
        // Set the initial health of the player.
        currentHealth = startingHealth;

    }
    // Start is called before the first frame update
    void Start()
    {
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

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

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


private void Update()
    {
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
        var InputDevice = InputManager.ActiveDevice;
        if (InputDevice.Action1.WasPressed)
        {
            Debug.Log(audio);
            audio.Play();
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }         
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
