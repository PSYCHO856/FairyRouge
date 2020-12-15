using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created on: 2020-8-26
/// Author: D.xy
/// 战中判定显示
/// </summary>
public class Judge : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject iconBack;

    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void initJudge (List<int> list) {
        int count = list.Count;

        float interval = 100;

        float startingPoint = -((count - 1) * (interval / 2));

        for (int i = 0; i < count; i++) {
            GameObject icon = new GameObject ();
            icon.transform.SetParent (iconBack.transform);

            Image Image = icon.AddComponent<Image> ();
            if (list[i] == 1) {
                Image.sprite = Resources.Load<Sprite> ("Battle/auto2");
            } else {
                Image.sprite = Resources.Load<Sprite> ("Battle/auto1");
            }
            Image.SetNativeSize ();

            icon.transform.localPosition = new Vector3 (startingPoint, 0, 0);

            startingPoint += interval;
        }

        Invoke ("closeLayer", 2);
    }

    void closeLayer () {
        Destroy (gameObject);
    }
}