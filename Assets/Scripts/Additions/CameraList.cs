using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraList : MonoBehaviour
{
     public static CameraList Instance;
    [SerializeField] List<GameObject> Keys;
    [SerializeField] List<GameObject> Utils;
    public Dictionary<GameObject, GameObject> CameraListControl;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

       
        CameraListControl = new();
        foreach (GameObject keys in Keys)
        {
            foreach (GameObject values in Utils)
            {
                if(!CameraListControl.ContainsKey(keys) && !CameraListControl.ContainsValue(values))
                    CameraListControl.Add(keys, values);
                    

            }

        }
    }
}
