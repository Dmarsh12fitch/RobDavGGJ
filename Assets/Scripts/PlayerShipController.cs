using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image img;
    [SerializeField] private Image img2;

    [SerializeField] private GameObject LaserProjPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bounds = 8.4f;
        speed = 4;
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
            CheckForCloseThings();

            if (GhostVisionEnabled)
            {
                GhostVisionCoolDown = 5;
                GhostVisionTimer -= Time.deltaTime;
                img.fillAmount = 0;
                img2.fillAmount = (5 - GhostVisionTimer) / 5;
                if (GhostVisionTimer <= 0)
                {
                    
                    GhostVisionEnabledORDisabled(false);
                }
            }
            else
            {
                img.fillAmount = (5 - GhostVisionCoolDown) / 5;
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
            img2.fillAmount = 0;
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

    void CheckForCloseThings()
    {
        GameObject[] EnemyGhostShips = GameObject.FindGameObjectsWithTag("Ghost Enemy Ship");

        foreach (GameObject GhostEnemyShip in EnemyGhostShips)
        {
            if(Mathf.Abs(GhostEnemyShip.transform.position.x - transform.position.x) < 0.5f
                && Mathf.Abs(GhostEnemyShip.transform.position.z - transform.position.z) < 0.5f)
            {
                ManagerScr.Instance.PlayerHit(1000);
            }
        }

        GameObject[] Astroids = GameObject.FindGameObjectsWithTag("Astroid");

        foreach(GameObject Astroid in Astroids)
        {
            if(Mathf.Abs(Astroid.transform.position.x - transform.position.x) < 0.5f
                && Mathf.Abs(Astroid.transform.position.z - transform.position.z) < 0.5f)
            {
                ManagerScr.Instance.PlayerHit(1000);
            }
        }

    }

    //background has children astroids, and is always moving in the direction oposite the player is facing



}
