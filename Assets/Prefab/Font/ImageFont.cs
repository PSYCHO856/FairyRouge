using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-26
/// Author: D.xy
/// 战中数值文字显示
/// </summary>
public class ImageFont : MonoBehaviour {
    public string number;
    public string path;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void create (string pathName, int value) {
        number = value.ToString ();
        path = pathName;

        printNumber ();
    }

    public void create () {
        printString ();
    }

    private void printNumber () {
        string numString = number;

        int interval = 32;

        float sPosition = EntMethod.startPosition (numString.Length, interval);

        for (int i = 0; i < numString.Length; i++) {
            GameObject font = new GameObject ();
            font.transform.position = EntMethod.getPosition (sPosition + i * interval, 0, 0);
            font.transform.SetParent (gameObject.transform);

            var c = numString[i];

            SpriteRenderer spr = font.AddComponent<SpriteRenderer> ();
            spr.sprite = ResourcesManager.getInstance ().getSprite (path, Convert.ToInt32 (c.ToString ()));
            spr.sortingOrder = 10;
        }
    }

    private void printString () {
        GameObject font = new GameObject ();
        font.transform.position = new Vector3 (0, 0, 0);
        font.transform.SetParent (gameObject.transform);

        SpriteRenderer spr = font.AddComponent<SpriteRenderer> ();
        EntMethod.load2DSprite (spr, "Battle/miss");
        spr.sortingOrder = 10;
    }

    public void closeLayer () {
        Destroy (gameObject);
    }
}