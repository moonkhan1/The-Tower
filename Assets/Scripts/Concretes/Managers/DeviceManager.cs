using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeviceManager : MonoBehaviour
{
    public static DeviceManager Instance;
    [SerializeField] List<GameObject> Keys;
    [SerializeField] List<GameObject> Utils;
    public Dictionary<GameObject, GameObject> itemsDevices;

    /*LEVEL 1*/
    public int Level1AmuletCount = 0;
    

    /*LEVEL 2*/
    [SerializeField] public GameObject Level2Platforms;
    [SerializeField] public List<GameObject> Level2Mosaic;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

       
        itemsDevices = new();
        foreach (GameObject keys in Keys)
        {
            foreach (GameObject values in Utils)
            {
                if(!itemsDevices.ContainsKey(keys) && !itemsDevices.ContainsValue(values))
                    itemsDevices.Add(keys, values);

            }

        }
    }
    
}
