using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;
using CASP.SoundManager;
using System;

public class MainMenuManager : MonoBehaviour
{
   public static MainMenuManager Instance;
    [SerializeField] GameObject _backGround;
    [SerializeField] GameObject _nameOftheGame;
    [SerializeField] GameObject[] _menuItems;
    [SerializeField] GameObject SettingsPanel;
       [SerializeField] AudioSource _mainMenuMusic;
       [SerializeField] AudioSource _gameMusic;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this);
        _mainMenuMusic.Play();
    }
 

   

    void Start()
    {
        
        Image _backGroundImg = _backGround.GetComponent<Image>();
        _backGroundImg.color = new Color(0, 0, 0, 255);
        DOTween.To(() => _backGroundImg.color, x => _backGroundImg.color = x, new Color32(255, 255, 255, 255), 1f).OnComplete(()=>{
            TextMeshProUGUI _nameOftheGameText = _nameOftheGame.GetComponent<TextMeshProUGUI>();
            _nameOftheGameText.color =  new Color(_nameOftheGameText.color.r, _nameOftheGameText.color.g, _nameOftheGameText.color.b, 0f);
            DOTween.To(() => _nameOftheGameText.color, x => _nameOftheGameText.color = x,new Color(_nameOftheGameText.color.r, _nameOftheGameText.color.g, _nameOftheGameText.color.b, 1f), 2f).OnComplete(()=>{
                DOTween.To(() => _nameOftheGameText.color, x => _nameOftheGameText.color = x,new Color(_nameOftheGameText.color.r, _nameOftheGameText.color.g, _nameOftheGameText.color.b, 0f), 2f).OnComplete(() =>
            {
                _nameOftheGame.SetActive(false);
                foreach (var texts in _menuItems)
                {
                    TextMeshProUGUI _nameOfmenuItemsText = texts.GetComponent<TextMeshProUGUI>();
                    _nameOfmenuItemsText.color = new Color(_nameOfmenuItemsText.color.r,_nameOfmenuItemsText.color.g, _nameOfmenuItemsText.color.b, 0f);
                    DOTween.To(() => _nameOfmenuItemsText.color, x => _nameOfmenuItemsText.color = x,new Color(_nameOfmenuItemsText.color.r, _nameOfmenuItemsText.color.g,_nameOfmenuItemsText.color.b, 0.3f), 2f);
                }
            });
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 private void StopMainMenuMusic()
    {
        _mainMenuMusic.Stop();
        _gameMusic.Play();
    }
    public void OpenSettings()
    {

        SettingsPanel.SetActive(true);
        _menuItems.ToList().ForEach(x => x.GetComponent<TextMeshProUGUI>().enabled = false);
        Image panelImg = SettingsPanel.GetComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0);
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.2f);
        SettingsPanel.transform.DOScale(new Vector3(5.63f, 5.63f, 5.63f), 0.2f);

    }
    public void CloseSettings()
    {

        Image panelImg = SettingsPanel.GetComponent<Image>();
        DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.2f);
        SettingsPanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
            _menuItems.ToList().ForEach(x => x.GetComponent<TextMeshProUGUI>().enabled = true);
        });

    }
    public void onMouseHoverPlaySound()
    {
        SoundManager.Instance.Play("MainMenuOptionSound");
    }

       public void PlayGame()
    {
        Image _backGroundImg = _backGround.GetComponent<Image>();
        DOTween.To(() => _backGroundImg.color, x => _backGroundImg.color = x, new Color32(25, 25, 25, 255), 1f).OnComplete(()=>{
        GameManager.Instance.LoadScene("Game");
        StopMainMenuMusic();
        });
        
        // TipsList.ToList().ForEach(x => x.SetActive(true));

    }


}
