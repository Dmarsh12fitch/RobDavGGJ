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
    [SerializeField] private GameObject astroidPrefab;

    [SerializeField] private Vector3[] spawnLocos;

    float spawnRate;
    float spawnRateVariability;
    float spawnCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 4;
        spawnRateVariability = 2;
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
            SpawnAstroid();
            SpawnAstroid();
            StartCoroutine(AsteroidSecond());
            spawnCoolDown = Random.Range(spawnRate - spawnRateVariability, spawnRate + spawnRateVariability);
        }


        spawnCoolDown -= Time.deltaTime;
    }

    public void PlayerHit(float damage)
    {
        playerHealth -= damage;
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
        justSpawnedEnemyShip.transform.position = spawnLocos[Random.Range(0, spawnLocos.Length)];
    }

    void SpawnAstroid()
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("Front Background");
        var justSpawnedAstroid = Instantiate(astroidPrefab);
        justSpawnedAstroid.transform.position = spawnLocos[Random.Range(0, spawnLocos.Length)];
        justSpawnedAstroid.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        justSpawnedAstroid.transform.SetParent(things[things.Length - 1].transform, true);
    }

    IEnumerator AsteroidSecond()
    {
        yield return new WaitForSeconds(Random.Range(spawnRateVariability, spawnRateVariability + 1f));
        float rand = Random.Range(0, 2);
        if(rand == 1)
        {
            var justSpawnedAstroid = Instantiate(astroidPrefab);
            justSpawnedAstroid.transform.position = spawnLocos[Random.Range(0, spawnLocos.Length)];
            justSpawnedAstroid.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            justSpawnedAstroid.transform.localScale = new Vector3(2, 2, 2);
        }
    }

}
