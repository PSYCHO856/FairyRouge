using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Created on: 2020-8-20
/// Author: D.xy
/// 通用功能
/// </summary>
public class EntMethod {
    /// <summary>
    /// 每单位像素数
    /// </summary>
    static float unit = 100f;

    /// <summary>
    /// 像素转单位
    /// </summary>
    static public Vector3 getPosition (float x, float y, float z) {
        Vector3 vec3 = new Vector3 ();

        vec3.x = x / unit;
        vec3.y = y / unit;
        vec3.z = z;

        return vec3;
    }

    static public Vector3 getPosition (Vector3 v3) {
        Vector3 vec3 = new Vector3 ();

        vec3.x = v3.x / unit;
        vec3.y = v3.y / unit;
        vec3.z = v3.z;

        return vec3;
    }

    /// <summary>
    /// 单位转像素
    /// </summary>
    static public Vector3 setPosition (float x, float y, float z) {
        Vector3 vec3 = new Vector3 ();

        vec3.x = x * unit;
        vec3.y = y * unit;
        vec3.z = z;

        return vec3;
    }

    static public Vector3 setPosition (Vector3 v3) {
        Vector3 vec3 = new Vector3 ();

        vec3.x = v3.x * unit;
        vec3.y = v3.y * unit;
        vec3.z = v3.z;

        return vec3;
    }

    static public float transformTo2D (float value) {
        return value * unit;
    }
    static public float transformTo3D (float value) {
        return value / unit;
    }

    static public float startPosition (int count, float interval) {
        return -(count - 1) * (interval / 2f);
    }

    static public void load2DSprite (SpriteRenderer icon, string path) {
        Texture2D texture2d = Resources.Load<Texture2D> (path);
        Sprite sp = Sprite.Create (texture2d, new Rect (0, 0, texture2d.width, texture2d.height), new Vector2 (.5f, .5f));
        icon.sprite = sp;
    }

    static public void loadUISprite (UnityEngine.UI.Image icon, string path) {
        icon.sprite = Resources.Load<Sprite> (path);
        icon.SetNativeSize ();
    }

    static public bool isPointerOverUIObject () {
        PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        eventDataCurrentPosition.pressPosition = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult> ();
        EventSystem.current.RaycastAll (eventDataCurrentPosition, results);

        return results.Count > 1;
    }
}