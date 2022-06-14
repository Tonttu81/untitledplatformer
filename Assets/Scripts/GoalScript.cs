using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GoalScript : MonoBehaviour
{
    public CanvasGroup levelWinScreen;
    public PostProcessVolume volume;
    GameObject player;
    PlayerScript playerScript;
    DepthOfField dof;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            volume.profile.TryGetSettings(out dof);
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<PlayerScript>();
            dof.focusDistance.value = 10f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Retry();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelWinScreen.alpha = 1f;
        levelWinScreen.interactable = true;
        dof.focusDistance.value = 1f;
        playerScript.enabled = !enabled;
    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            SceneManager.LoadScene(0);
        }
        else
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
