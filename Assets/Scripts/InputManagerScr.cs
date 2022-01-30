using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScr : MonoBehaviour
{
    //Making this a singleton _____________________________________
    public static InputManagerScr Instance = null;

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

    //move var
    public enum MoveDir
    {
        Stationary,
        Left,
        Right
    }
    public MoveDir MoveDirTry;

    //rot var
    public enum RotDir
    {
        Forward,
        SlightLeft,
        Left,
        FarLeft,
        SlightRight,
        Right,
        FarRight
    }
    public RotDir RotDirTry;

    public bool GhostVisionTry;

    // Start is called before the first frame update
    void Start()
    {
        MoveDirTry = MoveDir.Stationary;
        RotDirTry = RotDir.Forward;
        GhostVisionTry = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        //player movement
        if (Input.GetKey(KeyCode.A))
        {
            MoveDirTry = MoveDir.Left;
            
        } else if(Input.GetKey(KeyCode.D))
        {
            MoveDirTry = MoveDir.Right;
        } else
        {
            MoveDirTry = MoveDir.Stationary;
        }


        //player rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RotDirTry = RotDir.SlightLeft;
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                RotDirTry = RotDir.FarLeft;
            }
            else
            {
                RotDirTry = RotDir.Left;
            }
        } else if(Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RotDirTry = RotDir.SlightRight;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                RotDirTry = RotDir.FarRight;
            }
            else
            {
                RotDirTry = RotDir.Right;
            }
        } else
        {
            RotDirTry = RotDir.Forward;
        }

        //player ghostVision
        if (Input.GetKey(KeyCode.Space))
        {
            GhostVisionTry = true;
        }
        else
        {
            GhostVisionTry = false;
        }
        
    }





}
