using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public Button playButton;
    public Button OkButton;

    public GameObject GameInstructionsPopup;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayGame);
        OkButton.onClick.AddListener(LoadGame);
    }

    private void PlayGame()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        GameInstructionsPopup.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    private void LoadGame()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }
}
