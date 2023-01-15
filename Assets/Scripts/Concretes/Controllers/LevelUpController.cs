using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;
using UnityEngine.UI;
public class LevelUpController : ILevelControl
{
    private PlayerController _playerController;
    // public override event Action<int> isLeveltriggered;
    public override event Action isFinishtriggered;
    public LevelUpController(PlayerController playerController)
    {
        _playerController = playerController.GetComponent<PlayerController>();
    }

    public override void IsLeveltriggered(Collider other)
    {
        if (LevelUpManager.Instance.LevelTriggers.Any(u => u.name == other.name))
        {
            other.gameObject.SetActive(false);
            int index = LevelUpManager.Instance.LevelTriggers.FindIndex(a => a.name == other.name);
            foreach (var item in LevelUpManager.Instance.LevelDoors[index].GetComponentsInChildren<Transform>())
            {
                if (item.gameObject == null)
                    return;
                item.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 1f);
            }
        }
        if (LevelUpManager.Instance.FinishTriggers.Any(u => u.name == other.name))
        {
            isFinishtriggered?.Invoke();
            UIManager.Instance.FinalMessage.SetActive(true);
        }
        if (other.name ==  "UITrigger")
        {
            other.gameObject.SetActive(false);
            UIManager.Instance.StaminaUIs.SetActive(true);
            Image panelImg = UIManager.Instance.StaminaUIs.GetComponent<Image>();
            panelImg.color = new Color(0, 0, 0, 0);
            DOTween.To(() => panelImg.color, x => panelImg.color = x, new Color32(255, 255, 255, 0), 0.8f);
            UIManager.Instance.StaminaUIs.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        }
    }
}
