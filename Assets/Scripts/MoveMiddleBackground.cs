using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMiddleBackground : MonoBehaviour
{

    //go in the opposite direction of the rotation of the player

    float speed;
    bool hasDuplicated;

    private Transform StarTracker;

    [SerializeField] private GameObject MiddleBackgroundPrefab;

    private Transform BackgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundManager = GameObject.Find("BackgroundManager").GetComponent<Transform>();
        speed = 0.7f;
        StarTracker = GameObject.Find("StarTrackingOffset").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveIt();
        if (transform.position.z < -0.5f && !hasDuplicated)
        {
            hasDuplicated = true;
            var i = Instantiate(MiddleBackgroundPrefab, BackgroundManager);
            i.transform.position = new Vector3((float)transform.position.x, (float)transform.position.y, (float)transform.position.z + 10);
        }
        else if (transform.position.z < -15 && hasDuplicated)
        {
            Destroy(gameObject);
        }
    }

    void MoveIt()
    {
        transform.position = Vector3.MoveTowards(transform.position, StarTracker.position, speed * Time.deltaTime);
    }
}
