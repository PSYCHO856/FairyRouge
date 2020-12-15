using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-14
/// Author: D.xy
/// 背包
/// </summary>
public class BagManager : MonoBehaviour {

    /// <summary>
    /// 承载界面
    /// </summary>
    public GameObject layer;

    /// <summary>
    /// 播放退出动画
    /// </summary>
    public void onClickExit () {
        gameObject.GetComponent<Animator> ().SetTrigger ("exit");
    }

    /// <summary>
    /// 删除本体
    /// </summary>
    public void closed () {
        GameObject.Destroy (layer);
    }
}