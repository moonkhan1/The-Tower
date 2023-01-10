using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletController : MonoBehaviour
{
   Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_transform.parent == null)
    {
        _transform.GetComponentInChildren<TextMesh>().gameObject.SetActive(true);
        _transform.GetComponentInChildren<TextMesh>().transform.rotation.SetLookRotation(Camera.main.gameObject.transform.position);
    }
    else if(_transform.parent != null){
       _transform.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
    }
    }
}
