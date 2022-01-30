using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{



    
    [SerializeField] private GameObject MiddleBackgroundPrefab;
    [SerializeField] private GameObject FrontBackgroundPrefab;

    [SerializeField] private Transform PlayerShipAnchor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveFarBackground();
    }

    void FindPlayerRotationTo()
    {

    }

    void MoveFarBackground()
    {

    }


}
