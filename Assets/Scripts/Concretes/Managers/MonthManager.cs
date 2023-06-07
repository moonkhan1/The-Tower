using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public class MonthManager : MonoBehaviour
{
    public static MonthManager Instance;
    public bool isTime = false;
    [SerializeField] public List<GameObject> MonthItemList = new List<GameObject>();
    public event System.Action<bool> isItemTaken;
    public GameObject other;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }
    private void Update()
    {
        if (MonthItemList.Count > 1)
            MonthFollow();
        if (isTime)
            isItemTaken?.Invoke(true);            
        if(!isTime)
             isItemTaken?.Invoke(false);    
    }

    public void MonthFollow()
    {
        for (int i = 1; i < MonthItemList.Count; i++)
        {
            Vector3 PrevPos = MonthItemList[i - 1].transform.position;
            Vector3 CurrentPos = MonthItemList[i].transform.position;
            //    MonthItemList[i].transform.position = Vector3.Lerp(CurrentPos, PrevPos, 500f * Time.deltaTime);
            MonthItemList[i].transform.position = Vector3.Slerp(CurrentPos, PrevPos, 500f * Time.deltaTime);
            MonthItemList[i].transform.DORotate(new Vector3(MonthItemList[i].transform.position.x, 0, MonthItemList[i].transform.position.z), 0.1f);

        }
    }
    public void StartCount(Collider other)
    {
        if(isTime)
        {
            StartCoroutine(WaitBeforeDrop(other));
        }
        StopCoroutine(WaitBeforeDrop(other));
    }

    private IEnumerator WaitBeforeDrop(Collider other)
    {
        yield return new WaitUntil(() => UIManager.Instance.HoldBar.fillAmount == 0f);
            other.GetComponent<Collider>().isTrigger = false;
            other.GetComponent<Rigidbody>().isKinematic = false;
            MonthManager.Instance.MonthItemList.Remove(other.gameObject);
            other.transform.DOJump(new Vector3(other.transform.position.x + 2, 1f, (other.transform.position.z + 2)), 0.7f, 1, 0.4f);
            other.transform.parent = null;
            MonthManager.Instance.isTime = false;
            other.name = "Wait";
            isTime = false;
        StartCoroutine(WaitBeforeMakeKeyItem(2f));
        IEnumerator WaitBeforeMakeKeyItem(float time)
        {
            yield return new WaitForSeconds(time);
            other.name = "Amulets";
        }
        
    }
  

    
}

