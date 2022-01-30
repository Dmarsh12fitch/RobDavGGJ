using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrontBackground : MonoBehaviour
{

    float speed;
    bool hasDuplicated;

    private Transform StarTracker2;

    [SerializeField] private GameObject FrontBackgroundPrefab;

    private Transform BackgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundManager = GameObject.Find("BackgroundManager").GetComponent<Transform>();
        speed = 3f;
        StarTracker2 = GameObject.Find("StarTrackingOffset2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveIt();
        if (transform.position.z < -0.5f && !hasDuplicated)
        {
            hasDuplicated = true;
            var i = Instantiate(FrontBackgroundPrefab, BackgroundManager);
            i.transform.position = new Vector3((float)transform.position.x + Random.Range(-10, 10), (float)transform.position.y, (float)transform.position.z + 20);
        }
        else if (transform.position.z < -15 && hasDuplicated)
        {
            Destroy(gameObject);
        }
    }

    void MoveIt()
    {
        transform.position = Vector3.MoveTowards(transform.position, StarTracker2.position, speed * Time.deltaTime);
    }
}
