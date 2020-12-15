using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-1
/// Author: D.xy
/// 入口
/// </summary>
public class DoorManager : MonoBehaviour {
    public BattleManager manager;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void OnMouseUpAsButton () {
        if (EntMethod.isPointerOverUIObject ()) {
            return;
        }
        manager.unitMove (gameObject.transform.position);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "UnitFoot") {

        }
    }

    public void init (BattleManager m) {
        manager = m;
    }
}