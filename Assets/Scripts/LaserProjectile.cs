using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    int speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
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


}
