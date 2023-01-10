using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public class MonthManager : MonoBehaviour
{
    public static MonthManager Instance;
    float endTime = 0f;
    bool isTime = false;
    [SerializeField] public List<GameObject> MonthItemList = new List<GameObject>();
    public event System.Action<int> isItemTaken;
    public event System.Action<float> WaitItemForDropping;
    // public delegate IEnumerator ItemEventHandler(float time);
    //  public  event ItemEventHandler isItemInMonth;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        if (isTime)
        {
            endTime += Time.deltaTime;

        }
        if(MonthItemList.Count > 1)
            MonthFollow();


    }

    public void MonthFollow()
    {
        for (int i = 1; i < MonthItemList.Count; i++)
        {
        Vector3 PrevPos = MonthItemList[i-1].transform.position;
        Vector3 CurrentPos = MonthItemList[i].transform.position;
    //    MonthItemList[i].transform.position = Vector3.Lerp(CurrentPos, PrevPos, 500f * Time.deltaTime);
       MonthItemList[i].transform.position = Vector3.Slerp(CurrentPos, PrevPos,500f*Time.deltaTime);
    MonthItemList[i].transform.DORotate(new Vector3(MonthItemList[i].transform.position.x,0,MonthItemList[i].transform.position.z), 0.1f);
            
        }
    }
    // public async Task WaitBeforeDrop(float time, GameObject other)
    // {

    //     Debug.Log("Started");
    //     isTime = true;

    //     if (endTime > time)
    //     {
    //         Debug.Log("End");
    //         MonthItemList.Remove(other);
    //         other.transform.parent = null;
    //         other.transform.DOJump(new Vector3(other.transform.position.x + Random.Range(-2, 2), 0.25f, (other.transform.position.z + Random.Range(2, 3))), 0.7f, 1, 0.4f);
    //         endTime = 0f;
    //         isTime = false;
    //         Debug.Log("Ended");
    //         await Task.WhenAll();
    //     }
    // }
    public void CanStart(bool isTime, float time2, GameObject other2)
    {
        if (isTime)
        {
            isItemTaken?.DynamicInvoke(5);
            StartCoroutine(WaitBeforeDrop(time2, other2));
        }
        StopCoroutine(WaitBeforeDrop(0.01f, other2));
    }

    public IEnumerator WaitBeforeDrop(float time, GameObject other)
    {

        yield return new WaitForSeconds(time);
        WaitItemForDropping?.Invoke(time);
        other.GetComponent<Collider>().isTrigger = false;
        other.GetComponent<Rigidbody>().isKinematic = false;
        MonthItemList.Remove(other);
        other.transform.DOJump(new Vector3(other.transform.position.x + 2, 1f, (other.transform.position.z + 2)), 0.7f, 1, 0.4f);
        other.transform.parent = null;
        endTime = 0f;
        isTime = false;
        other.name = "Wait";
        StartCoroutine(WaitBeforeMakeKeyItem(2f));

        IEnumerator WaitBeforeMakeKeyItem(float time3)
        {
            yield return new WaitForSeconds(time3);
            other.name = "Amulets";
        }
    }


}

