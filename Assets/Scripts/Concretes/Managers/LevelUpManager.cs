using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance;
    [SerializeField] public List<GameObject> LevelTriggers;
    [SerializeField] public List<GameObject> LevelDoors;

    private void Awake() {
        if(Instance == null)
            Instance = this;
    }
}
