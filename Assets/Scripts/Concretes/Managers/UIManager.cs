using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;
using CASP.SoundManager;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TMP_Text _jumpCount;
    [SerializeField] TMP_Text _itemHoldCount;
    [SerializeField] public MonthItem _monthItem;

    [Header("GameOver Panel")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject GameOverUIPanel;
    [SerializeField] GameObject RestartButton;
    private PlayerController _playerController;
    private float _itemDropCount2 = 5;
    // private bool isTaken = false;

    [SerializeField] GameObject Dialogue;

    [Header("Pause Panel")]
    [SerializeField] GameObject PausePanel;

    [Header("Settings Panel")]
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject[] PauseMenuItems;

    // [Header("Tips Panel")]
    // [SerializeField] GameObject[] TipsList;
    // private int _index;
    // [SerializeField] GameObject _nextButton;

    [Header("Game Start")]

    [SerializeField] AudioSource _gameMusic;

    [Header("Final")]
    [SerializeField] public GameObject FinalMessage;

    [Header("Player")]
    [SerializeField] public Image RunBar;
    [SerializeField] public Image HoldBar;
    [SerializeField] public GameObject StaminaUIs;

    [Header("Angel")]
    [SerializeField] public GameObject Quest;
    [SerializeField] public GameObject AngleQuestionMark;

    // [SerializeField] GameObject _backGround;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        // DontDestroyOnLoad(this);
        _playerController = FindObjectOfType<PlayerController>();

    }

    private void Update()
    {

        // _itemDropCount2 = _itemDropCount2 - Time.deltaTime;
        // _itemHoldCount.text = System.Convert.ToInt32(_itemDropCount2).ToString();
        //  HoldBar.fillAmount = (HoldBar.fillAmount -  0.001f );



    }
    private void OnEnable()
    {
        if (_playerController == null) return;
        _playerController.GetComponent<PlayerController>().IsJumpedAction += JumpCountControl;
        _playerController.GetComponent<PlayerController>().isAngelTriggered += AngelDialogue;
        _playerController.GetComponent<PlayerController>().isPlayerDead += GameOverMethod;
        _playerController.GetComponent<PlayerController>().isRunning += RunBarControl;
        MonthManager.Instance.isItemTaken += ItemDropControl;
        GameManager.Instance.PauseMenu += OpenPause;
        GameManager.Instance.PauseMenu += ClosePause;

    }



    private void OnDisable()
    {
        if (_playerController == null) return;
        _playerController.GetComponent<PlayerController>().IsJumpedAction -= JumpCountControl;
        _playerController.GetComponent<PlayerController>().isAngelTriggered -= AngelDialogue;
        _playerController.GetComponent<PlayerController>().isPlayerDead -= GameOverMethod;
        _playerController.GetComponent<PlayerController>().isRunning -= RunBarControl;
        MonthManager.Instance.isItemTaken -= ItemDropControl;
        GameManager.Instance.PauseMenu -= OpenPause;
        GameManager.Instance.PauseMenu -= ClosePause;

    }

    private void RunBarControl(bool isPlayerRunning)
    {
        if (isPlayerRunning)
            RunBar.fillAmount = (RunBar.fillAmount - 0.005f);
        else
            RunBar.fillAmount = (RunBar.fillAmount + 0.007f);
    }

    private void JumpCountControl(int _realTimeJumpCount)
    {
        _jumpCount.text = _realTimeJumpCount.ToString();
    }
    private void ItemDropControl(bool isTaken)
    {
        if (isTaken)
            HoldBar.fillAmount = (HoldBar.fillAmount - 0.002f);
        else
            HoldBar.fillAmount = (HoldBar.fillAmount + 0.003f);
    }
    private void AngelDialogue()
    {
        Dialogue.SetActive(true);

    }

    public void GameOverMethod()
    {
        GameOverPanel.SetActive(true);
        GameOverUIPanel.SetActive(true);
        GameOverUIPanel.transform.localScale = Vector3.zero;
        Image panelImg = GameOverPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 180), 0.2f);
        GameOverUIPanel.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.2f);
    }

    public void OpenPause(int pause)
    {
        if (pause == 0)
        {
            PausePanel.SetActive(true);
            Image panelImg = PausePanel.GetComponent<Image>();
            panelImg.color = new Color(0, 0, 0, 0);
            DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 235), 0.2f);
            PausePanel.transform.DOScale(new Vector3(21.65f, 11.92f, 20.35f), 0.2f);
        }
    }
    public void ClosePause(int pause)
    {
        if (pause == 1)
        {
            Image panelImg = PausePanel.GetComponent<Image>();
            DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 0), 0.2f);
            PausePanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
            {
                PausePanel.SetActive(false);
            });
        }
    }

    public void OpenSettings()
    {

        SettingsPanel.SetActive(true);
        PauseMenuItems.ToList().ForEach(x => x.GetComponent<TextMeshProUGUI>().enabled = false);
        Image panelImg = SettingsPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.2f).SetUpdate(true);
        SettingsPanel.transform.DOScale(new Vector3(5.63f, 5.63f, 5.63f), 0.2f).SetUpdate(true);

    }
    public void CloseSettings()
    {

        Image panelImg = SettingsPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.2f).SetUpdate(true);
        SettingsPanel.transform.DOScale(0f, 0.2f).SetUpdate(true).OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
            PauseMenuItems.ToList().ForEach(x => x.GetComponent<TextMeshProUGUI>().enabled = true);
        });

    }

    public void RestartMethod()
    {
        GameManager.Instance.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void QuestManager(bool isActive)
    {
        Quest.SetActive(isActive);
        Image panelImg = PausePanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 255), 0.2f);
        Quest.transform.DOScale(new Vector3(0.86f, 0.86f, 0.86f), 0.7f);
    }

    // public void NextTip()
    // {
    //     _index++;
    //     if (_index < TipsList.Length)
    //     {
    //         TipsList[_index - 1].SetActive(false);
    //         SoundManager.Instance.Play("MainMenuOptionSound");
    //     }
    //     if (_index > TipsList.Length - 1)
    //     {

    //         _nextButton.SetActive(false);
    //         SoundManager.Instance.Play("MainMenuOptionSound");
    //         TipsList.ToList().ForEach(x => x.SetActive(false));
    //     }
    // }
    // public IEnumerator WaitAndShowTips()
    // {
    //     yield return new WaitForSeconds(1f);
    //     TipsList.ToList().ForEach(x => x.SetActive(true));
    //     _nextButton.SetActive(true);
    // }
}