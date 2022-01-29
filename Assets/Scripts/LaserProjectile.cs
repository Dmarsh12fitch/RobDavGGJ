using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    float speed;
    float damage;

    bool hasHit;


    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
        damage = 5;
        hasHit = false;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }

    void moveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            hasHit = true;
            Debug.Log("Collide");
            if (gameObject.CompareTag("Player Laser"))
            {
                if (collision.gameObject.CompareTag("Enemy Ship"))
                {
                    collision.gameObject.GetComponent<EnemyShipScr>().EnemyHit(damage);
                    Explode();
                }
                else
                {
                    Explode();
                }
            }
            else if (gameObject.CompareTag("Enemy Laser"))
            {
                if (collision.gameObject.CompareTag("Player Ship"))
                {
                    ManagerScr.Instance.PlayerHit(damage);
                    Explode();
                } else
                {
                    Explode();
                }
            }
        }
    }



    void Explode()
    {
        //instantiate an explosion effect
        Destroy(gameObject);
    }


}
