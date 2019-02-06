using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    public bool destroyAll = false;
    private DestroyByContact destroyByContactController;

    public GameObject explosion;

    // private GameObject destroyByContactObject;

    //GameObject[] objs;
    GameObject[] destroyByContactObject;

    void explosions()
    {
        foreach (GameObject enemy in destroyByContactObject)
        {
            //enemy.GetComponent<DestroyByContact>().enemiesExplode();
            Debug.Log("Make enemies explode!");
            Instantiate(explosion, enemy.transform.position, enemy.transform.rotation);
            //Debug.Log("ENEMY FOUND");
        }
    }

    private void Start()
    {
        destroyByContactObject = GameObject.FindGameObjectsWithTag("Enemy");   

        /*
        objs = GameObject.FindGameObjectsWithTag("LightUser");
        foreach (GameObject lightuser in objs)
        {
            lightuser.GetComponent<Light>().enabled = false;
        }*/
        /*
       GameObject destroyByContactObject = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject destroy in destroyByContactObject)
        {
            destroyByContactController = destroyByContactObject.GetComponent<DestroyByContact>();

        }*/
        /*
        destroyByContactController = GameObject.FindGameObjectsWithTag("Enemy");

        destroyByContactObject = destroyByContactController.GetComponent<GameController>();
        */
        /*
        foreach (GameObject respawn in destroyByContactController)
        {

            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
        }*/
        // destroyByContactController = destroyByContactObject.GetComponent<DestroyByContact>();
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy" && destroyAll)
        {
            //explosions();
            Destroy(other.gameObject);
            StartCoroutine(destroyAllDelay());
        }
    }

       IEnumerator destroyAllDelay()
    {
        yield return new WaitForSeconds(2.5f);
        destroyAll = false;
       // destroyAllDisable();
    }
    /*
    void destroyAllDisable()
    {
        //timeAgain = timeLimit;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        destroyAll = false;


        Debug.Log("Enemies stop destroying");
    }*/

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}