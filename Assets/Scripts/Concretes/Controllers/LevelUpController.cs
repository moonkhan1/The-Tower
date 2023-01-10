using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;

public class LevelUpController : ILevelControl
{
    private PlayerController _playerController;
    public override event Action<int> isLeveltriggered;

    public LevelUpController(PlayerController playerController)
    {
        _playerController = playerController.GetComponent<PlayerController>();
    }

    public override void IsLeveltriggered(Collider other)
    {
        if(LevelUpManager.Instance.LevelTriggers.Any(u => u.name == other.name))
        {
            isLeveltriggered?.Invoke(System.Convert.ToInt32(other.name.Substring(other.name.Length-1)));
            other.gameObject.SetActive(false);
            int index = LevelUpManager.Instance.LevelTriggers.FindIndex(a => a.name == other.name);
            foreach (var item in LevelUpManager.Instance.LevelDoors[index].GetComponentsInChildren<Transform>())
            {
                
            item.DOLocalRotateQuaternion(Quaternion.Euler(0,0,0), 1f);
            }
        } 
    }
}
