using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    private bool isTaken = false;

    [SerializeField] GameObject Dialogue;

    [Header("Pause Panel")]
    [SerializeField] GameObject PausePanel;

    [Header("Settings Panel")]
    [SerializeField] GameObject SettingsPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _playerController = FindObjectOfType<PlayerController>();

    }
    private void Update()
    {
        if (isTaken)
        {
            _itemDropCount2 = _itemDropCount2 - Time.deltaTime;
            _itemHoldCount.text = System.Convert.ToInt32(_itemDropCount2).ToString();
        }


    }
    private void OnEnable()
    {
        if (_playerController == null) return;
        _playerController.GetComponent<PlayerController>().IsJumpedAction += JumpCountControl;
        _playerController.GetComponent<PlayerController>().isAngelTriggered += AngelDialogue;
        _playerController.GetComponent<PlayerController>().isPlayerDead += GameOverMethod;
        MonthManager.Instance.WaitItemForDropping += ItemTakeControl;
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
        MonthManager.Instance.WaitItemForDropping -= ItemTakeControl;
        MonthManager.Instance.isItemTaken -= ItemDropControl;
        GameManager.Instance.PauseMenu -= OpenPause;
        GameManager.Instance.PauseMenu -= ClosePause;

    }
    public void JumpCountControl(int _realTimeJumpCount)
    {
        _jumpCount.text = _realTimeJumpCount.ToString();
    }
    public void ItemDropControl(int _itemTakeCount)
    {
        _itemHoldCount.text = _itemTakeCount.ToString();
        isTaken = true;
    }
    private void AngelDialogue()
    {
        Dialogue.SetActive(true);
    }

    public void ItemTakeControl(float _itemDropCount)
    {
        _itemDropCount2 = _itemDropCount;
        isTaken = false;
        _itemHoldCount.text = _itemDropCount2.ToString();
        Debug.Log("Count startted");
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
            DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(32, 32, 32, 174), 0.2f);
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
        Image panelImg = SettingsPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 255), 0.2f);
        SettingsPanel.transform.DOScale(new Vector3(5.63f, 5.63f, 5.63f), 0.2f);

    }
    public void CloseSettings()
    {

        Image panelImg = SettingsPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.2f);
        SettingsPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
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



}
