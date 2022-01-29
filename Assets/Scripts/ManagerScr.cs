using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScr : MonoBehaviour
{
    //Making this a singleton _____________________________________
    public static ManagerScr Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    //END Making this a singleton _________________________________

    float playerHealth;

    private PlayerShipController playerShipScript;

    [SerializeField] private GameObject enemyShipPrefab;

    [SerializeField] private Vector3[] EnemyShipSpawnLocos;

    float spawnRate;
    float spawnRateVariability;
    float spawnCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 4;
        spawnRateVariability = 1;
        spawnCoolDown = 4;
        playerHealth = 100;
        playerShipScript = GameObject.Find("PlayerShip").GetComponent<PlayerShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCoolDown <= 0)
        {
            SpawnEnemyShip();
            spawnCoolDown = Random.Range(spawnRate - spawnRateVariability, spawnRate + spawnRateVariability);
        }

        spawnCoolDown -= Time.deltaTime;
    }

    public void PlayerHit(float damage)
    {
        playerHealth -= damage;
        Debug.Log("Player Health = " + playerHealth);
        if(playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        playerShipScript.dead = true;
        //instansiate big explosion effect
        //set to not active the display of the player
    }

    void SpawnEnemyShip()
    {
        var justSpawnedEnemyShip = Instantiate(enemyShipPrefab);
        justSpawnedEnemyShip.transform.position = EnemyShipSpawnLocos[Random.Range(0, EnemyShipSpawnLocos.Length)];
    }

}
