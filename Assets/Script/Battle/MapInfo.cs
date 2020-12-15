using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-2
/// Author: D.xy
/// 大地图
/// </summary>
public class MapInfo : MonoBehaviour {
    public GameObject roomPrefab;
    public Vector3 position;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void init (List<RoomData> list) {
        var e = list.GetEnumerator ();

        while (e.MoveNext ()) {
            GameObject room = GameObject.Instantiate (roomPrefab, gameObject.transform);
            room.transform.localPosition = new Vector3 (e.Current.position.x * position.x, e.Current.position.y * position.y, 0);
        }
    }

    public void exit() {
        Destroy(gameObject);
    }
}