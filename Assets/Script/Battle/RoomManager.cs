using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-2
/// Author: D.xy
/// 房间管理
/// </summary>
public class RoomManager : MonoBehaviour {
    public List<RoomData> roomList;

    // Start is called before the first frame update
    void Start () {
        roomList = new List<RoomData> ();
        createRoom ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void createRoom () {
        int number = 20;

        Vector3 position = new Vector3 (0, 0, 0);

        for (int i = 0; i < number; i++) {
            while (isSameLocation (position)) {
                int direction = Random.Range (0, 4);

                switch (direction) {
                    case 0: //up
                        position.y += 1;
                        break;
                    case 1: //down
                        position.y -= 1;
                        break;
                    case 2: //left
                        position.x -= 1;
                        break;
                    case 3: //right
                        position.x += 1;
                        break;
                }
            }
            RoomData room = new RoomData ();
            roomList.Add (room);
            room.init (i + 1, position);
        }
    }

    private bool isSameLocation (Vector3 pos) {
        var e = roomList.GetEnumerator ();

        while (e.MoveNext ()) {
            if (e.Current.isSameLocation (pos)) {
                return true;
            }
        }
        return false;
    }
}