using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;
using CASP.SoundManager;
public class DeviceController : IDevice
{
    [SerializeField] Transform _movePosition;
    private PlayerController _playerController;
    private ItemController _itemController;
    public override event System.Action isPicTriggered;
    


    public DeviceController(PlayerController playerController)
    {
        _playerController = playerController.GetComponent<PlayerController>();

    }
    public DeviceController(ItemController itemController)
    {
        _itemController = itemController.GetComponent<ItemController>();
    }

    public override void WhenTriggerInteractable(Collider amuletPlatform)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
        {
            if (amuletPlatform.name == items.Key.name && items.Key.name.Contains("Gate"))
            {
                items.Value.transform.DOMove(items.Value.transform.GetChild(0).position, 2f)
                .SetEase(Ease.Linear);
                SoundManager.Instance.Play("StoneSolveSound");
                Debug.Log("Touched Gate");
            }

            if (amuletPlatform.name == items.Key.name && items.Key.name.Contains("Rotate"))
            {
                items.Value.transform.GetComponentInChildren<ParticleSystem>().Play();
                SoundManager.Instance.Play("Magic");
                items.Value.transform.DOMoveY(items.Value.transform.GetChild(0).position.y, 2f).OnComplete(() =>
                {

                    items.Value.GetComponentInChildren<ParticleSystem>().Stop();
                });
                Debug.Log("Touched GateKeyIron");
            }
        }
    }

    public override void WhenTriggerInteractable(Collider amuletPlatform, Transform amulet)
    {
        var levelOneSolvedAmulets = DeviceManager.Instance.LevelOneSolvedAmulets;
        var itemsDevices = DeviceManager.Instance.itemsDevices;
        
        foreach (KeyValuePair<GameObject, GameObject> items in itemsDevices)
        {
            if (amuletPlatform.name == items.Key.name && items.Key.name.Contains("Amulet"))
            {
                Debug.Log(levelOneSolvedAmulets.Count);
                if (amulet.transform.parent == null)
                {
                    if (amulet.CompareTag(items.Key.tag))
                    {
                        levelOneSolvedAmulets.Add(amulet);
                    }

                    amulet.DOJump(amuletPlatform.transform.GetChild(0).position, 0.7f, 1, 0.6f).OnComplete(() =>
                    {
                        amulet.rotation = amuletPlatform.transform.GetChild(0).rotation;
                        SoundManager.Instance.Play("Amulet");
                    });
                }

                if (amulet.transform.parent != null && amulet.CompareTag(items.Key.tag))
                {
                    levelOneSolvedAmulets.Remove(amulet);
                }
                if (levelOneSolvedAmulets.Count == 3)
                {
                    foreach (var item in itemsDevices.Where(u => u.Key.name.Contains("Amulet")))
                    {
                        item.Value.transform.DOMoveX(item.Value.transform.GetChild(0).position.x, 2f)
                            .OnComplete(()=>
                        {
                            item.Key.name = "Solved";
                        });
                        SoundManager.Instance.Play("Magic");
                        item.Key.transform.GetComponentInChildren<ParticleSystem>().Play();
                        item.Value.transform.GetComponentInChildren<ParticleSystem>().Play();
                    }
                }
            }
            
        }
    }
    
    public override void WhenTriggerInteractable(Transform player, Vector3 playerDirection, Collider other, Quaternion isRotationCorrect)
    {
        foreach (KeyValuePair<GameObject, GameObject> items in DeviceManager.Instance.itemsDevices)
        {
            if (other.name == items.Key.name && items.Key.name.Contains("Roll"))
            {
                bool isPictureCorrect = false;
                player.DOJump(items.Key.transform.parent.position, 0.7f, 1, 0.2f);
                if (playerDirection.z > 0 && !isPictureCorrect)
                {
                    //SoundManager.Instance.PlaySound("StoneRoundSound", items.Value.transform.position, 1f);
                    items.Key.transform.DORotate(new Vector3(0, 0, items.Key.transform.rotation.z - 5), 0.1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                    items.Value.transform.DORotate(new Vector3(0, items.Value.transform.rotation.y - 3, 0), 0.1f).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                }
                if (playerDirection.z < 0 && !isPictureCorrect)
                {
                    //SoundManager.Instance.PlaySound("StoneRoundSound", items.Value.transform.position, 1f);
                    items.Key.transform.DORotate(new Vector3(0, 0, items.Key.transform.rotation.z + 5), 0.1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                    items.Value.transform.DORotate(new Vector3(0, items.Value.transform.rotation.y + 1, 0), 0.1f).SetLoops(-1, LoopType.Incremental)
                    .SetRelative();
                }
                if (playerDirection.z == 0)
                {
                    //SoundManager.Instance.Stop("StoneRoundSound");
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                }

                if ((DeviceManager.Instance.Level2Mosaic.All(u =>
                System.Math.Truncate(Mathf.Abs(u.transform.localEulerAngles.y)) >= isRotationCorrect.eulerAngles.y - 4
                && System.Math.Truncate(Mathf.Abs(u.transform.localEulerAngles.y)) <= isRotationCorrect.eulerAngles.y + 4)))
                {
                    //SoundManager.Instance.Stop("StoneRoundSound");
                    SoundManager.Instance.Play("StoneSolveSound");
                    Debug.Log("Correct");
                    isPictureCorrect = true;
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                    isPicTriggered?.Invoke();
                }

                if (InputReader.Instance.isInteractionPressed)
                {
                    // SoundManager.Instance.Stop("StoneRoundSound");
                    items.Key.transform.DOKill();
                    items.Value.transform.DOKill();
                    player.DOJump(items.Key.transform.GetChild(0).position, 0.8f, 1, 0.4f);

                }

            }



        }
    }

}
