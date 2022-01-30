using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public bool dead;

    float bounds;
    float speed;
    float rotationSpeed;
    float rateOfFire;
    float fireCoolDown;

    bool GhostVisionEnabled;
    float GhostVisionTimer;
    float GhostVisionCoolDown;

    Vector3 rotTo;

    [SerializeField] private Transform PlayerShipDisplay;
    [SerializeField] private Transform PlayerShipTurret;
    [SerializeField] private GameObject GhostVisionOverlay;

    [SerializeField] private GameObject LaserProjPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bounds = 8.4f;
        speed = 6;
        rotationSpeed = 6;
        fireCoolDown = 2;
        rateOfFire = 10;
        GhostVisionTimer = 5;
        GhostVisionCoolDown = 0;
        PlayerShipDisplay = GameObject.Find("PlayerShipDisplayAnchor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            checkPlayerMovement();
            checkPlayerRotation();
            CheckGhostVision();

            if (GhostVisionEnabled)
            {
                GhostVisionCoolDown = 5;
                GhostVisionTimer -= Time.deltaTime;
                if(GhostVisionTimer <= 0)
                {
                    GhostVisionEnabledORDisabled(false);
                }
            }
            else
            {
                GhostVisionTimer = 5;
                GhostVisionCoolDown -= Time.deltaTime;
            }

            if (fireCoolDown <= 0)
            {
                Fire();
                fireCoolDown = 1 / rateOfFire;
            }
            fireCoolDown -= Time.deltaTime;
        }
    }

    void Fire()
    {
        if (GhostVisionEnabled)
        {
            var JustSpawnedThing = Instantiate(LaserProjPrefab, PlayerShipTurret.position, PlayerShipTurret.rotation);
            JustSpawnedThing.gameObject.tag = "Player Ghost Laser";
        }
        else
        {
            Instantiate(LaserProjPrefab, PlayerShipTurret.position, PlayerShipTurret.rotation);
        }
        
    }

    void checkPlayerMovement()
    {
        if(InputManagerScr.Instance.MoveDirTry == InputManagerScr.MoveDir.Left
            && transform.position.x > -bounds)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        } else if(InputManagerScr.Instance.MoveDirTry == InputManagerScr.MoveDir.Right
            && transform.position.x < bounds)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void checkPlayerRotation()
    {
        switch (InputManagerScr.Instance.RotDirTry)
        {
            case InputManagerScr.RotDir.FarLeft:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(-85, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.Left:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(-60, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.SlightLeft:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(-30, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.FarRight:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(85, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.Right:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(60, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.SlightRight:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(30, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            case InputManagerScr.RotDir.Forward:
                rotTo = Quaternion.Lerp(PlayerShipDisplay.rotation, Quaternion.AngleAxis(0, Vector3.up),
                    Time.deltaTime * rotationSpeed).eulerAngles;
                PlayerShipDisplay.rotation = Quaternion.Euler(0f, rotTo.y, 0f);
                break;
            default:
                Debug.Log("Programmer done fricked up");
                break;
        }
    }

    void CheckGhostVision()
    {
        if (InputManagerScr.Instance.GhostVisionTry && GhostVisionCoolDown <= 0 && !GhostVisionEnabled && GhostVisionTimer == 5)
        {
            GhostVisionEnabledORDisabled(true);
        }
    }

    void GhostVisionEnabledORDisabled(bool EnabledORDisabled)
    {
        GhostVisionEnabled = EnabledORDisabled;

        GhostVisionOverlay.GetComponent<SpriteRenderer>().enabled = EnabledORDisabled;

        GameObject[] EnemyGhostShips = GameObject.FindGameObjectsWithTag("Ghost Enemy Ship");

        foreach(GameObject GhostEnemyShip in EnemyGhostShips)
        {
            GhostEnemyShip.GetComponentInChildren<SpriteRenderer>().enabled = EnabledORDisabled;
        }
    }



    //background has children astroids, and is always moving in the direction oposite the player is facing



}
