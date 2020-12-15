using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-2
/// Author: D.xy
/// 房间数据
/// </summary>
public class RoomData {
    public int roomId { get; private set; }
    public Vector3 position { get; private set; }

    public void init (int id, Vector3 pos) {
        roomId = id;
        position = pos;
    }

    public bool isSameLocation (Vector3 pos) {
        if (position == pos) {
            return true;
        }
        return false;
    }
}