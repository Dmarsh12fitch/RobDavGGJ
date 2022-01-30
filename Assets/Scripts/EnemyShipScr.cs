using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScr : MonoBehaviour
{
    float enemyHealth;
    float speed;
    float rotationSpeed;
    float fireCoolDown;
    float rateOfFire;

    float rotationHueristic;

    float rotateCoolDown;
    float rotateEvery;

    Vector3 rotTo;

    private Transform playerShipTransform;

    [SerializeField] private GameObject LaserProjPrefab;
    [SerializeField] private GameObject GhostEnemyShipPrefab;

    [SerializeField] private Transform EnemyShipTurret;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 10f;
        speed = 1f;
        rotationHueristic = 45f;
        rotationSpeed = 10f;
        fireCoolDown = Random.Range(1, 3);
        rateOfFire = 1f;
        rotateCoolDown = 2f;
        rotateEvery = 100f;

        playerShipTransform = GameObject.Find("PlayerShip").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if(rotateCoolDown <= 0)
        {
            ChangeRotateHueristic();
            rotateCoolDown = rotateEvery;
        }

        rotateCoolDown -= Time.deltaTime;

        moveForward();

        if (fireCoolDown <= 0)
        {
            Fire();
            fireCoolDown = 1 / rateOfFire;
        }

        fireCoolDown -= Time.deltaTime;
    }

    /*
Vector3 directionToPoint = new Vector3(playerShipTransform.position.x - transform.position.x,
    playerShipTransform.position.y - transform.position.y + rotationHueristic,
    playerShipTransform.position.z - transform.position.z );*/
    //Quaternion r = Quaternion.Euler(directionToPoint.x, directionToPoint.y + rotationHueristic, directionToPoint.z);
    //rotateToFaceTarget.SetAxisAngle(Vector3.up, rotationHueristic);

    void Rotate()
    {
        Vector3 directionToPoint = playerShipTransform.transform.position - transform.position;

        Quaternion rotateToFaceTarget = Quaternion.LookRotation(directionToPoint);

        //make rotateToFaceTarget off by rotationHueristic

        rotTo = Quaternion.Lerp(transform.rotation, rotateToFaceTarget, Time.deltaTime * rotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
    }

    void ChangeRotateHueristic()
    {
        rotationHueristic = Random.Range(-10, 10);
    }

    void moveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Fire()
    {
        var justFiredLaser = Instantiate(LaserProjPrefab, EnemyShipTurret.position, EnemyShipTurret.rotation);
        justFiredLaser.tag = "Enemy Laser";
    }

    public void EnemyHit(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("enemy health = " + enemyHealth);
        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        //instantiate bigexplosion effect
        Instantiate(GhostEnemyShipPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
