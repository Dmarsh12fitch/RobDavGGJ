using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScr : MonoBehaviour
{
    bool onStart;
    int counterS;
    int counterQ;

    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject QuitButton;


    // Start is called before the first frame update
    void Start()
    {
        onStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTheGame();
        }

        if (Input.GetKeyDown(KeyCode.S) && onStart)
        {
            onStart = false;
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            onStart = true;
        }

        if (onStart)
        {
            if(counterS == 0)
            {
                counterQ = 0;
                counterS = 1;
                StartButton.GetComponent<Image>().color = Color.white;
                QuitButton.GetComponent<Image>().color = Color.grey;
                //GameObject.Find("StartButton").GetComponent<Image>().color = Color.white;
                //GameObject.Find("QuitButton").GetComponent<Image>().color = Color.grey;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                onStart = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartTheGame();
            }
        } else
        {
            if(counterQ == 0)
            {
                counterS = 0;
                counterQ = 1;
                QuitButton.GetComponent<Image>().color = Color.white;
                StartButton.GetComponent<Image>().color = Color.grey;
                //GameObject.Find("QuitButton").GetComponent<Image>().color = Color.white;
                //GameObject.Find("StartButton").GetComponent<Image>().color = Color.grey;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                onStart = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                QuitTheGame();
            }
        }
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

}
