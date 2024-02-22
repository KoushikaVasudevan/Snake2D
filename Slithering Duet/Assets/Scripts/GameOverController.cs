using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void SnakeDied()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PlayerDeath);
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuScene()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
