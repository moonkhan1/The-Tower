using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingUI : MonoBehaviour
{
    public static KeyBindingUI Instance { get; private set; }
    [Header("Key Buttons")]
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _sprintButton;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _pauseButton;
    [Header("Key Text in buttons")]
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _sprintText;
    [SerializeField] private TextMeshProUGUI _jumpText;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    
    [SerializeField] private Transform _pressToAnyKeyToRebindTransform;

    private void Awake()
    {
        Instance = this;
        _moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveUp);
        });
        _moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveDown);
        });
        _moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveLeft);
        });
        _moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.MoveRight);
        });
        _interactionButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Interaction);
        });
        _jumpButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Jump);
        });
        _sprintButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Sprint);
        });
        _pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(InputReader.Bindings.Pause);
        });
    }

    private void Start()
    {
        UpdateVisual();
        HidePressToRebindKey();
    }

    private void UpdateVisual()
    {
        _moveUpText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveUp);
        _moveDownText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveDown);
        _moveLeftText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveLeft);
        _moveRightText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.MoveRight);
        _interactionText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Interaction);
        _sprintText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Sprint);
        _jumpText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Jump);
        _pauseText.text = InputReader.Instance.GetBindingText(InputReader.Bindings.Pause);
    }
    
    private void ShowPressToRebindKey() => _pressToAnyKeyToRebindTransform.gameObject.SetActive(true);
    private void HidePressToRebindKey() => _pressToAnyKeyToRebindTransform.gameObject.SetActive(false);
    private void RebindBinding(InputReader.Bindings bindings)
    {
        ShowPressToRebindKey();
        InputReader.Instance.RebindBinding(bindings, ()=>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
    
    }

