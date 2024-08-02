using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGameManager : MonoBehaviour
{
    public Image startImage;
    public Button startButton;

    public Text gameState;

    public AudioSource clickSound;

    private void Start()
    {
        startImage.gameObject.SetActive(true);

        Time.timeScale = 0f;

        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        clickSound.Play();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        startImage.gameObject.SetActive(false);
        Time.timeScale = 1f;

        gameState.gameObject.SetActive(true);
        gameState.text = "Start!";

        yield return new WaitForSeconds(0.5f);
        gameState.gameObject.SetActive(false);
    }
}
