using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CASP.SoundManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event System.Action OnGamePaused;
    public event System.Action OnGameUnPaused;
    private bool IsGamePause = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //DontDestroyOnLoad(this);
    }

    void Update()
    {
        OpenPauseMenu();

    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name)
    {
        yield return SceneManager.LoadSceneAsync(name);
    }

    private void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsGamePause = !IsGamePause;
            if (IsGamePause)
            {
                OnGamePaused?.Invoke();
                Time.timeScale = 0;
            }
            else
            {
                OnGameUnPaused?.Invoke();
                Time.timeScale = 1f;
            }
        }
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
    
