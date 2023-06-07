using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class MonthItem : IMonth
{
    // [SerializeField] private List<GameObject> MonthItemList = new List<GameObject>();
    private Transform _playerMonth;
    Collider other;
    public MonthItem(PlayerController playerController)
    {
        _playerMonth = playerController.GetComponent<PlayerController>()._monthPoint;
    }



    public override void GetItemToMonth(Collider other)
    {
        if (other.name.Contains("Amulets") && MonthManager.Instance.MonthItemList.Count != 2)
        {

            var seq = DOTween.Sequence();
            seq.Kill();
            if (!other.GetComponent<ItemController>())
            {
                other.gameObject.AddComponent<ItemController>();
            }
            other.GetComponent<Collider>().isTrigger = true;
            other.GetComponent<Rigidbody>().isKinematic = true;
            seq = DOTween.Sequence();
            MonthManager.Instance.MonthItemList.Add(other.gameObject);
            seq.Append(other.transform.DOJump(new Vector3(_playerMonth.position.x, _playerMonth.position.y, _playerMonth.position.z), 2f, 1, 0.2f));
            other.transform.parent = _playerMonth;
            other.transform.GetComponentInChildren<ParticleSystem>().Play();
            MonthManager.Instance.isTime = true;
            MonthManager.Instance.StartCount(other);


        }
    }
   






}
