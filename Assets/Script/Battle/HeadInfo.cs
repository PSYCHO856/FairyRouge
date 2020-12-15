using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created on: 2020-8-17
/// Author: D.xy
/// 战中头像图标 属性显示
/// </summary>
public class HeadInfo : MonoBehaviour {
    [Header ("头像资源")]
    public Image headImage;
    [Space (10)]
    public Text hpText;
    public Text mpText;
    public Text atkText;
    public Text defText;

    void Start () {

    }

    void Update () {

    }

    /// <summary>
    /// 头像数据
    /// </summary>
    public void updateHead (int id, AttributeData attr) {
        headImage.sprite = Resources.Load<Sprite> ("Battle/hpicon");
        headImage.SetNativeSize ();

        hpText.text = attr.toStringByType (AttributeData.AttrType.ATTR_HP_VALUE);
        mpText.text = attr.toStringByType (AttributeData.AttrType.ATTR_MAGIC_VALUE);
        atkText.text = attr.toStringByType (AttributeData.AttrType.ATTR_ATK_VALUE);
        defText.text = attr.toStringByType (AttributeData.AttrType.ATTR_ATK_DEF_VALUE);
    }
}