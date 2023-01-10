
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level2RollControl : MonoBehaviour
{

    public float FixeScale = 0.5f;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, -9, transform.localPosition.z);

    }
}
