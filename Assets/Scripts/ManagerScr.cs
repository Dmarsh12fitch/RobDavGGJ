using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScr : MonoBehaviour
{
    float playerHealth;

    [SerializeField] private PlayerShipController playerShipScript;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
                                                    //why is this next line giving me an error????
        playerShipScript.GetComponent<PlayerShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit(float damage)
    {
        playerHealth -= damage;
        //instanstiate explosion-type effect
    }

    public void GameOver()
    {
        playerShipScript.dead = true;
        //instanstiate explosion effect
        //set to not active the display of the player
    }

}
