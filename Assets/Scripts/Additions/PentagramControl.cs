using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PentagramControl : MonoBehaviour
{
    Transform _trasnsform;
   private void Awake() {
    _trasnsform = GetComponent<Transform>();
   }
    void Start()
    {

        _trasnsform.DORotate(new Vector3(0,0,-10), 0.6f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
