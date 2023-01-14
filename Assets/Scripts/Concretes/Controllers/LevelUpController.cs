using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

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
            UIManager.Instance.StaminaUIs.SetActive(true);
            other.gameObject.SetActive(false);
            int index = LevelUpManager.Instance.LevelTriggers.FindIndex(a => a.name == other.name);
            foreach (var item in LevelUpManager.Instance.LevelDoors[index].GetComponentsInChildren<Transform>())
            {
                if(index == 1)
                    return;
                item.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 1f);
            }
        }
        if (LevelUpManager.Instance.FinishTriggers.Any(u => u.name == other.name))
        {
            isFinishtriggered?.Invoke();
            UIManager.Instance.FinalMessage.SetActive(true);
        }
    }
}
