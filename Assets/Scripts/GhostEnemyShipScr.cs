using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyShipScr : MonoBehaviour
{

    float enemyHealth;
    float speed;
    float rotationSpeed;
    float fireCoolDown;
    float rateOfFire;
    float newDestinationCoolDown;

    Vector3 rotTo;

    Vector3 destination;

    [SerializeField] private GameObject LaserProjPrefab;

    [SerializeField] private Transform GhostEnemyShipTurret;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 10;
        speed = 1.5f;
        rotationSpeed = 1;
        fireCoolDown = 5;
        rateOfFire = 5;
        newDestinationCoolDown = 4;
        PickNewDestination();
    }

    // Update is called once per frame
    void Update()
    {

        if (newDestinationCoolDown <= 0 || (Mathf.Abs(transform.position.x - destination.x) < 0.5f
            && Mathf.Abs(transform.position.z - destination.z) < 0.5f))
        {
            PickNewDestination();
            newDestinationCoolDown = Random.Range(4, 6);
        }

        newDestinationCoolDown -= Time.deltaTime;

        moveForward();

        Rotate();

        if (fireCoolDown <= 0)
        {
            Fire();
            fireCoolDown = rateOfFire;
        }

        fireCoolDown -= Time.deltaTime;

    }

    void PickNewDestination()
    {
        destination = new Vector3(Random.Range(-8, 8), 0, Random.Range(-3, 4));
    }

    void Rotate()
    {
        Vector3 directionToPoint = destination - transform.position;

        Quaternion rotateToFaceTarget = Quaternion.LookRotation(directionToPoint);

        //make rotateToFaceTarget off by rotationHueristic

        rotTo = Quaternion.Lerp(transform.rotation, rotateToFaceTarget, Time.deltaTime * rotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
    }

    void moveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Fire()
    {
        var justFiredLaser = Instantiate(LaserProjPrefab, GhostEnemyShipTurret.position, GhostEnemyShipTurret.rotation);
        justFiredLaser.tag = "Enemy Laser";
    }

    public void EnemyHit(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        ManagerScr.Instance.killedAnEnemy();
        Destroy(gameObject);
    }

}
