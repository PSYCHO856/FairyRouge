using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-19
/// Author: D.xy
/// 战斗界面 包括战斗 地下城
/// </summary>
public class BattleLayer : MonoBehaviour {
    // Start is called before the first frame update

    public enum LayerType {
        LAYER_NONE = 1,
        EVENT_BATTLE,
        EVENT_SHOP,
        EVENT_DIALOGUE
    }

    [Header ("资源")]
    public GameObject doorPrefab;
    [Header ("变量")]
    public List<GameObject> doorPositiList;
    [Tooltip ("悬挂位置标记等空节点")]
    public GameObject layer;
    public LayerType layerType;

    void Start () {
        layerType = LayerType.LAYER_NONE;
        createDoor ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void createDoor () {
        for (int i = 0; i < doorPositiList.Count; i++) {
            GameObject door = GameObject.Instantiate (doorPrefab, doorPositiList[i].transform.position, Quaternion.identity);
            door.transform.SetParent (layer.transform);
            //door.transform.position = doorPositiList[i].transform.position;

            DoorManager manager = door.GetComponent<DoorManager> ();
            manager.init (gameObject.GetComponent<BattleManager> ());
        }
    }
}