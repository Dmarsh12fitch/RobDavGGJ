using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScr : MonoBehaviour
{
    [SerializeField] private Transform astroidDisplay;

    Vector3 rotBy;

    float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = 1;

        rotBy = new Vector3(0, 0, 0);
        while(Mathf.Abs(rotBy.y) < 0.5f)
        {
            rotBy = new Vector3(0, Random.Range(-2, 2), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateDisplay();
        MoveForward();
    }


    void RotateDisplay()
    {
        astroidDisplay.Rotate(rotBy);
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
