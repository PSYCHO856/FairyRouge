using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-17
/// Author: D.xy
/// 战中单位
/// </summary>
public class BattleUnit : MonoBehaviour {

    public enum UnitType {
        CAMP_OUR = 100,
        CAMP_ENEMY,
        CLICK_NONE,
        CLICK_DOWN,
        CLICK_UP,
        BATTLE_READY,
        BATTLE_JUDGE,
        BATTLE_ATK,
        BATTLE_OVER,
        BATTLE_NONE,
        ATTACK_ATK,
        ATTACK_SKILL
    }

    [Header ("资源")]
    public GameObject fontPrefab;
    [Header ("组件")]
    public SkeletonAnimation unitSpine;
    public BoxCollider2D chooseBox;
    public GameObject movIcon;
    public GameObject numberBack;
    [Header ("变量")]
    [SerializeField]
    private UnitType campType;
    [SerializeField]
    private int unitId;
    [SerializeField]
    private int resourcesId;
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private bool isOut;
    [SerializeField]
    private UnitType clickType;
    [SerializeField]
    private float movline;
    [SerializeField]
    private bool isBattleReady;
    [SerializeField]
    private UnitType actionType;
    [SerializeField]
    private List<int> judgeTimes;
    public List<BattleUnit> targetList;
    public UnitType attackType;
    public Vector3 location;
    public BattleManager manager;
    private AttributeData attrData;
    private AttributeData attrDataOriginal;
    private bool isMove;
    private Vector3 endPosition;

    void Awake () {
        judgeTimes = new List<int> ();
        targetList = new List<BattleUnit> ();

        isDead = false;
        isOut = false;

        isMove = false;
    }

    void Start () {
        clickType = UnitType.CLICK_NONE;
        actionType = UnitType.BATTLE_NONE;

        movline = 0f;

        isBattleReady = false;
    }

    void Update () {

    }

    /*void FixedUpdate () {
        if (!isMove) {
            return;
        }


        
    }*/

    public void actionlogic () {
        if (isOut || isDead) {
            return;
        }

        switch (actionType) {
            case UnitType.BATTLE_READY:
                break;
            case UnitType.BATTLE_JUDGE:
                weaponJudge ();
                break;
            case UnitType.BATTLE_ATK:
                onAttack ();
                break;
            case UnitType.BATTLE_OVER:
                onActionOver ();
                break;
        }
    }

    public void logic (float dt) {
        if (isOut || isDead) {
            return;
        }

        movline += getProperty (AttributeData.AttrType.ATTR_SPEED) * dt;

        if (movline > manager.timeLine) {
            movline = manager.timeLine;
            isBattleReady = true;
        }

        if (movIcon != null) {
            MoveHead head = movIcon.GetComponent<MoveHead> ();

            head.toMove (movline, manager.timeLine);

            if (isBattleReady) {
                head.setHighlight (true);
            } else {
                head.setHighlight (false);
            }
        }
    }

    void OnMouseDown () {
        clickType = UnitType.CLICK_DOWN;
    }

    void OnMouseUp () {
        clickType = UnitType.CLICK_NONE;
    }

    void OnMouseUpAsButton () {
        if (EntMethod.isPointerOverUIObject ()) {
            return;
        }
    
        if (manager.getBattleType () == BattleManager.ManagerType.BATTLE_NONE) {
            return;
        }

        if (clickType == UnitType.CLICK_DOWN) {
            if (campType == UnitType.CAMP_OUR) {
                manager.updateHeadInfo (gameObject);
            } else {
                if (manager.unitType == BattleManager.ManagerType.BATTLE_UNIT_ATK ||
                    manager.unitType == BattleManager.ManagerType.BATTLE_UNIT_SKILL) {
                    manager.addTargetToUnit (this);
                } else {
                    manager.showEnemyData (this);
                }
            }

            clickType = UnitType.CLICK_UP;
        }
    }

    public void initUnit (int uid, int sid, UnitType camp) {
        this.unitId = uid;
        this.resourcesId = sid;

        this.campType = camp;

        createSpine ();

        attrDataOriginal = new AttributeData ();
        attrDataOriginal.initPlayer (uid, 1);

        attrData = new AttributeData ();
        attrData.setValue (attrDataOriginal);

        BoxCollider2D box = unitSpine.gameObject.AddComponent<BoxCollider2D> ();

        chooseBox = gameObject.AddComponent<BoxCollider2D> ();
        chooseBox.offset = box.offset;
        chooseBox.size = box.size;

        Destroy (box);

        numberBack.transform.localPosition = new Vector3 (0, chooseBox.size.y, 0);
    }

    public void clearUnit () {
        clickType = UnitType.CLICK_NONE;
        actionType = UnitType.BATTLE_NONE;

        movline = 0f;

        isBattleReady = false;

        if (movIcon != null) {
            MoveHead head = movIcon.GetComponent<MoveHead> ();
            head.resetPosition ();
        }
    }

    public void HandleEvent (Spine.TrackEntry trackEntry, Spine.Event e) {
        if (e.Data.Name == "fazhao") {
            if (attackType == UnitType.ATTACK_ATK) {
                generateAttack ();
            } else if (attackType == UnitType.ATTACK_SKILL) {
                skillAttack ();
            }
        }
    }

    public void SpinComplete (Spine.TrackEntry trackEntry) {
        string animationName = trackEntry.Animation != null ? trackEntry.Animation.Name : "";

        if (animationName != "idle")
        {
            Debug.Log("SpinComplete: " + animationName);
        }

        if (animationName == "pugong") {
            unitSpine.state.SetAnimation (0, "paobu", true);

            if (campType == UnitType.CAMP_OUR) {
                unitSpine.transform.localScale = new Vector3 (1, 1, 1);
            } else {
                unitSpine.transform.localScale = new Vector3 (-1, 1, 1);
            }

            iTween.MoveTo (gameObject,
                iTween.Hash (
                    "x", location.x,
                    "y", location.y,
                    "easeType", iTween.EaseType.linear,
                    "time", 0.25f,
                    "oncomplete", "setActionTypeToOver"));

        } else if (animationName == "attack") {
            setActionTypeToOver ();
        } else if (animationName == "injured") {
            unitSpine.state.SetAnimation (0, "idle", true);
        } else if (animationName == "die") {
            isOut = true;
        }
    }

    public void SpinStart (Spine.TrackEntry trackEntry) {
        string animationName = trackEntry.Animation != null ? trackEntry.Animation.Name : "";
    }

    public void SpinEnd (Spine.TrackEntry trackEntry) {
        string animationName = trackEntry.Animation != null ? trackEntry.Animation.Name : "";
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="source">伤害来源</param>
    /// <param name="value">伤害数值</param>
    /// <param name="isCrit">暴击判定</param>
    /// <param name="isDodge">闪避判定</param>
    public void onDamage (BattleUnit source, int value, bool isCrit, bool isDodge) {

        if (!isDodge) {
            int hp = (int) getProperty (AttributeData.AttrType.ATTR_HP_VALUE);

            hp -= value;

            if (hp <= 0) {
                isDead = true;
                unitSpine.state.SetAnimation (0, "die", false);
            } else {
                unitSpine.state.SetAnimation (0, "injured", false);
            }

            showNumber (value, isCrit?2 : 1);
        } else {
            showNumber (value, 4);
        }
    }

    /// <summary>
    /// 恢复
    /// </summary>
    /// <param name="source">恢复来源</param>
    /// <param name="hp">恢复数值</param>
    /// <param name="type">其他</param>
    public void onRestore (BattleUnit source, int hp, int type) {
        showNumber (hp, 3);
    }

    /// <summary>
    /// 是否可战斗
    /// </summary>
    /// <returns></returns>
    public bool isReady () {
        return isBattleReady;
    }

    /// <summary>
    /// 战斗准备
    /// </summary>
    public void                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         battleReady () {
        isBattleReady = false;
        movline = 0f;

        targetList.Clear ();

        clearBuff ();
        triggerBuff ();

        actionType = UnitType.BATTLE_READY;
    }

    /// <summary>
    /// 属性相关
    /// </summary>
    public float getProperty (AttributeData.AttrType type) {
        return attrData.toValue (type);
    }

    public void setProperty (AttributeData.AttrType type, float value) {
        attrData.setAttribute (type, value);
    }

    public float getOriginalProperty (AttributeData.AttrType type) {
        return attrDataOriginal.toValue (type);
    }

    public void resetProperty (AttributeData.AttrType type) {
        setProperty (type, getOriginalProperty (type));
    }

    /// <summary>
    /// 重置属性 但是不改变生命值
    /// </summary>
    public void resetProperty () {
        float hp = getProperty (AttributeData.AttrType.ATTR_HP_VALUE);
        attrData.setValue (attrDataOriginal);
        setProperty (AttributeData.AttrType.ATTR_HP_VALUE, hp);
    }

    public int getPropertyBar (AttributeData.AttrType type) {
        float value = getProperty (type);
        float limit = getOriginalProperty (type);

        return (int) (value / limit * 100);
    }

    public void changeProperty (AttributeData.AttrType type, float value) {
        float exist = getProperty (type);
        float result = Math.Max (0f, exist + value);
        setProperty (type, result);
    }

    public void changePropertyPer (AttributeData.AttrType type, float value) {
        float exist = getProperty (type);
        float result = Math.Max (0f, exist + value * exist * 0.01f);
        setProperty (type, result);
    }

    public int getUnitId () {
        return unitId;
    }

    public AttributeData GetAttributeData () {
        return attrData;
    }

    public void setActionTypeToAttack () {
        actionType = UnitType.BATTLE_ATK;
    }

    public void setActionTypeToJudge () {
        actionType = UnitType.BATTLE_JUDGE;
    }

    public void setActionTypeToOver () {
        unitSpine.state.SetAnimation (0, "idle", true);

        if (campType == UnitType.CAMP_OUR) {
            unitSpine.transform.localScale = new Vector3 (-1, 1, 1);
        } else {
            unitSpine.transform.localScale = new Vector3 (1, 1, 1);
        }
        actionType = UnitType.BATTLE_OVER;
    }

    public void setAttackType (UnitType type) {
        attackType = type;
    }

    public UnitType getCampType () {
        return campType;
    }

    public void attackAction () {
        unitSpine.state.SetAnimation (0, "pugong", false);
    }

    public float getHeight () {
        return chooseBox.size.y;
    }

    public bool isDeadAndOut () {
        return isOut;
    }

    public void removeFromParent () {
        Destroy (movIcon);
        Destroy (gameObject);
    }

    //---分割线 战外移动------------------------------------------------------
    public void setMoveToDoor (Vector3 position) {
        isMove = true;
        endPosition = position;

        // 为什么这么写 因为不想删掉这个变量 又讨厌警告
        if (isMove) {
            unitSpine.state.SetAnimation (0, "paobu", true);

            iTween.MoveTo (gameObject,
                iTween.Hash (
                    "x", position.x,
                    "y", position.y,
                    "easeType", iTween.EaseType.linear,
                    "speed", 10,
                    "time", 1f,
                    "oncomplete", "moveOver"));
        }
    }

    public void moveOver () {
        unitSpine.state.SetAnimation (0, "idle", true);
    }
    //---分割线 以下为私有成员---------------------------------------------------
    /// <summary>
    /// 动态加载spine动画
    /// </summary>
    private void createSpine () {
        unitSpine.skeletonDataAsset = Resources.Load<SkeletonDataAsset> ("Spine/Unit/skeleton_SkeletonData");
        unitSpine.Initialize (true);

        unitSpine.loop = true;
        unitSpine.AnimationName = "idle";

        if (campType == UnitType.CAMP_OUR) {
            unitSpine.transform.localScale = new Vector3 (-1, 1, 1);
        }

        // 注册事件
        unitSpine.state.Event += HandleEvent;
        unitSpine.state.Complete += SpinComplete;
        unitSpine.state.Start += SpinStart;
        unitSpine.state.End += SpinEnd;
    }
    private void generateAttack () {
        var e = targetList.GetEnumerator ();

        while (e.MoveNext ()) {
            int result = 0;

            bool isHitValue = isHit (e.Current);
            bool isCritValue = isCrit (e.Current);

            if (isHitValue) {
                result = (int) damageValue (e.Current);

                if (isCritValue) {
                    result = result * 2;
                }
            }
            e.Current.onDamage (this, result, isCritValue, !isHitValue);
        }
    }

    private void skillAttack () {
        var e = targetList.GetEnumerator ();

        while (e.MoveNext ()) {
            e.Current.onDamage (this, 100, true, false);
        }
    }

    private void weaponJudge () {
        judgeTimes.Clear ();

        int equipCount = 1;

        CSVHelper csv = CSVManager.getInstance ().getCsvByName ("equip");

        for (int i = 0; i < equipCount; i++) {

            int times = csv.getIntDataByIDandTitle (1001, "decisionTime");
            int property = csv.getIntDataByIDandTitle (1001, "propertyDecision");

            int value = (int) attrData.toValue ((AttributeData.AttrType) property);

            for (int n = 0; n < times; n++) {
                if (UnityEngine.Random.Range (0, 100) < value) {
                    judgeTimes.Add (1);
                } else {
                    judgeTimes.Add (0);
                }
            }
        }

        manager.showWeaponJudge (judgeTimes);

        actionType = UnitType.BATTLE_NONE;

        // 2秒后执行攻击
        Invoke ("setActionTypeToAttack", 2);
    }

    private void onAttack () {
        if (attackType == UnitType.ATTACK_ATK) {
            Vector3 pos = targetList[0].gameObject.transform.localPosition;

            if (campType == UnitType.CAMP_OUR) {
                pos.x -= chooseBox.size.x / 2 + targetList[0].chooseBox.size.x / 2;
            } else {
                pos.x += chooseBox.size.x / 2 + targetList[0].chooseBox.size.x / 2;
            }
            unitSpine.state.SetAnimation (0, "paobu", true);
            iTween.MoveTo (gameObject,
                iTween.Hash (
                    "x", pos.x,
                    "y", pos.y,
                    "easeType", iTween.EaseType.linear,
                    "time", 0.25f,
                    "oncomplete", "attackAction"));
        } else if (attackType == UnitType.ATTACK_SKILL) {
            unitSpine.state.SetAnimation (0, "attack", false);
        }
        actionType = UnitType.BATTLE_NONE;
    }

    private void onActionOver () {
        actionType = UnitType.BATTLE_NONE;
        manager.unitActionOver ();
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="target">目标</param>
    /// <returns></returns>
    private float damageValue (BattleUnit target) {
        float atk = getProperty (AttributeData.AttrType.ATTR_ATK_VALUE);
        float def = target.getProperty (AttributeData.AttrType.ATTR_ATK_DEF_VALUE);

        int isJudge = 0;

        for (int i = 0; i < judgeTimes.Count; i++) {
            if (judgeTimes[i] == 1) {
                isJudge += 1;
            }
        }

        float per = isJudge * 1f / judgeTimes.Count;
        float damage = Math.Max (atk * per - def, 0f);

        damage = 100;

        return damage;
    }

    /// <summary>
    /// 暴击判定
    /// </summary>
    /// <param name="target">目标</param>
    /// <returns></returns>
    private bool isCrit (BattleUnit target) {
        float crit = getProperty (AttributeData.AttrType.ATTR_CRIT_VALUE);

        if (UnityEngine.Random.Range (0, 100) < crit) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 命中判定
    /// </summary>
    /// <param name="target">目标</param>
    /// <returns></returns>
    private bool isHit (BattleUnit target) {
        return true;

        /*float dodge = target.getProperty (AttributeData.AttrType.ATTR_DODGE_VALUE);

        if (UnityEngine.Random.Range (0, 100) < dodge) {
            return false;
        }
        return true;*/
    }

    /// <summary>
    /// 触发BUFF
    /// </summary>
    private void triggerBuff () {

    }

    /// <summary>
    /// 清除BUFF
    /// </summary>
    private void clearBuff () {

    }

    private void showNumber (int number, int type) {
        GameObject obj = GameObject.Instantiate (fontPrefab);
        obj.transform.SetParent (numberBack.transform);

        ImageFont script = obj.GetComponent<ImageFont> ();

        switch (type) {
            case 1:
                script.create ("Battle/Number/zc_shanghaishu", number);
                break;
            case 2:
                script.create ("Battle/Number/zc_baojishu", number);
                break;
            case 3:
                script.create ("Battle/Number/zc_jiaxueshu", number);
                break;
            case 4:
                script.create ();
                break;
        }
    }
}


//[SerializeField]面板读取赋值