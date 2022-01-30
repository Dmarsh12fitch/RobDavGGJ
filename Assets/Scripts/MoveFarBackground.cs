using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFarBackground : MonoBehaviour
{
    float speed;
    bool hasDuplicated;

    [SerializeField] private GameObject FarBackgroundPrefab;
    private Transform BackgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundManager = GameObject.Find("BackgroundManager").GetComponent<Transform>();
        speed = -0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -0.5f && !hasDuplicated)
        {
            hasDuplicated = true;
            var i = Instantiate(FarBackgroundPrefab, BackgroundManager);
            i.transform.position = new Vector3((float)transform.position.x, (float)transform.position.y, (float)transform.position.z + 10);
        } else if(transform.position.z < -15 && hasDuplicated)
        {
            Destroy(gameObject);
        }
        MoveIt();
    }

    void MoveIt()
    {
        transform.Translate(new Vector3(0, speed, 0));
    }

}
