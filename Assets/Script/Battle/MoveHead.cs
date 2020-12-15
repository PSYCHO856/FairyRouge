using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-21
/// Author: D.xy
/// 移动头像
/// </summary>
public class MoveHead : MonoBehaviour {

    public SpriteRenderer headIcon;
    public GameObject blackBack;
    public GameObject HighlightBack;
    public GameObject startPoint;
    public GameObject endPoint;
    public BattleUnit.UnitType unitCamp;

    void Start () {

    }

    void Update () {

    }

    public void init (int id, GameObject start, GameObject end, BattleUnit.UnitType camp) {
        headIcon.sprite = Resources.Load<Sprite> ("Battle/hpicon");

        startPoint = start;
        endPoint = end;

        unitCamp = camp;

        resetPosition ();
    }

    public void setHighlight (bool isbool) {
        blackBack.SetActive (!isbool);
        HighlightBack.SetActive (isbool);
    }

    public void toMove (float mov, float number) {
        float distance = endPoint.transform.position.y - startPoint.transform.position.y;
        float distance2d = EntMethod.transformTo2D (distance);
        //distance*100f
        float topos = EntMethod.transformTo3D (mov / number * distance2d) + startPoint.transform.position.y;
        //走过的比例*行动条总长+起始位置
        Vector3 position = gameObject.transform.localPosition;

        gameObject.transform.localPosition = new Vector3 (position.x, topos, position.z);
    }

    public void resetPosition () {
        if (unitCamp == BattleUnit.UnitType.CAMP_OUR) {
            blackBack.transform.localScale = new Vector3 (-1, 1, 1);
            HighlightBack.transform.localScale = new Vector3 (-1, 1, 1);

            gameObject.transform.localPosition = new Vector3 (-0.38f, startPoint.transform.position.y, 0);
        } else {
            gameObject.transform.localPosition = new Vector3 (0.38f, startPoint.transform.position.y, 0);
        }
        setHighlight (false);
    }
}