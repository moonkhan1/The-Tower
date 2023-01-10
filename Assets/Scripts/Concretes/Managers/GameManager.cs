using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event System.Action<int> PauseMenu;
    int pauseCount = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
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

    public void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu?.Invoke(pauseCount);
            if(pauseCount == 0)
            {
                StartCoroutine(WaitForTimeScale(0));
                pauseCount++;
            }
            else
            {
                Time.timeScale = 1;
                pauseCount--;
            }
        }
        // StopAllCoroutines();
    }
    IEnumerator WaitForTimeScale(int index)
    {
        
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = index;
        
    }
}
