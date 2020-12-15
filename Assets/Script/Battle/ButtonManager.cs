using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Created on: 2020-8-17
/// Author: D.xy
/// 战中按键管理
/// </summary>
public class ButtonManager : MonoBehaviour {

    [Header ("资源")]
    public GameObject bagPrefab;
    public GameObject mapPrefab;
    [Header ("控件")]
    public GameObject battleLayer;
    [Space (10)]
    [SerializeField]
    private BattleManager manager;
    [SerializeField]
    private BattleLayer layer;
    [SerializeField]
    private RoomManager room;
    [SerializeField]
    private GameObject imageEffect;

    void Awake () {
        imageEffect = null;
    }

    void Start () {
        manager = battleLayer.GetComponent<BattleManager> ();
        layer = battleLayer.GetComponent<BattleLayer> ();
        room = battleLayer.GetComponent<RoomManager> ();
    }

    void Update () {
        if (manager.unitType == BattleManager.ManagerType.BATTLE_UNIT_NONE) {
            fadeOutEffect ();
        }
    }

    /// <summary>
    /// 攻击键
    /// </summary>
    /// <param name="image">动画效果</param>
    public void onClickAtkButton (GameObject image) {
        if (manager.isAutoBattle == true) {
            return;
        }
        if (manager.unitType == BattleManager.ManagerType.BATTLE_UNIT_NONE) {
            return;
        }
        manager.unitType = BattleManager.ManagerType.BATTLE_UNIT_ATK;

        setImageEffect (image);
    }

    /// <summary>
    /// 技能键
    /// </summary>
    /// <param name="image">动画效果</param>
    public void onClickSkillButton (GameObject image) {
        if (manager.isAutoBattle == true) {
            return;
        }
        if (manager.unitType == BattleManager.ManagerType.BATTLE_UNIT_NONE) {
            return;
        }
        manager.unitType = BattleManager.ManagerType.BATTLE_UNIT_SKILL; 

        setImageEffect (image);
    }

    /// <summary>
    /// 自动按键
    /// </summary>
    /// <param name="PressImage">显示按下的状态图片</param>
    public void onClickAutoButton (GameObject PressImage) {
        setImageEffect (null);
        //无高亮特效
        if (PressImage.activeSelf) {
            PressImage.SetActive (false);
            manager.isAutoBattle = false;
        } else {
            PressImage.SetActive (true);
            manager.isAutoBattle = true;
        }
    }

    /// <summary>
    /// 头像按键
    /// </summary>
    public void onClickHeadButton () {
        Debug.Log ("onClickHeadButton");
    }

    /// <summary>
    /// 地图展开按键
    /// </summary>
    public void onClickMapButton () {
        GameObject map = GameObject.Instantiate (mapPrefab, gameObject.transform);

        MapInfo info = map.GetComponent<MapInfo> ();
        info.init (room.roomList);
    }

    /// <summary>
    /// 背包按键
    /// </summary>
    public void onClickBagButton () {
        GameObject.Instantiate (bagPrefab, gameObject.transform);
    }

    /// <summary>
    /// 返回按键
    /// </summary>
    public void onClickGobackButton () {
        SceneManager.LoadScene (0);
    }

    public void fadeOutEffect () {
        if (imageEffect != null) {
            imageEffect.SetActive (false);
        }
        imageEffect = null;
    }

    public void startBattle () {
        manager.init ();
    }

    /// <summary>
    /// 设置动画效果
    /// </summary>
    /// <param name="image">动画效果图片</param>
    private void setImageEffect (GameObject image) {
        if (imageEffect != null && imageEffect != image) {
            imageEffect.SetActive (false);
            if (manager.unitType != BattleManager.ManagerType.BATTLE_UNIT_NONE) {
                manager.unitType = BattleManager.ManagerType.BATTLE_UNIT_WAIT;
            }
        }
        imageEffect = image;

        if (imageEffect == null) {
            return;
        }

        if (image.activeSelf) {
            image.SetActive (false);
            if (manager.unitType != BattleManager.ManagerType.BATTLE_UNIT_NONE) {
                manager.unitType = BattleManager.ManagerType.BATTLE_UNIT_WAIT;
            }
        } else {
            image.SetActive (true);
        }
    }
}