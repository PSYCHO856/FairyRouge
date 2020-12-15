using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-17
/// Author: D.xy
/// 战斗管理
/// </summary>
public class BattleManager : MonoBehaviour {
    public enum ManagerType {
        BATTLE_NONE = 1,
        BATTLE_READY,
        BATTLE_ENTER,
        BATTLE_PAUSE,
        BATTLE_GAME,
        BATTLE_OVER,
        BATTLE_UNIT_WAIT,
        BATTLE_UNIT_ATK,
        BATTLE_UNIT_SKILL,
        BATTLE_UNIT_NONE,
        BATTLE_WIN,
        BATTLE_FAIL
    }

    [Header ("资源")]
    public GameObject unitPrefab;
    public GameObject moveHeadPrefab;
    public GameObject JudgePrefab;
    [Header ("控件")]
    public GameObject battleLayer;
    public GameObject unitLayer;
    public GameObject chooseIcon;
    public GameObject buttonPanel;
    public GameObject moveHeadBack;
    public GameObject buttonHead;
    public GameObject enemyWarning;
    public GameObject[] movepoint;
    public GameObject[] unitPosition;

    public GameObject battlereceipt;
    [Header ("公有变量")]
    public int timeLine;
    public bool isAutoBattle;
    public ManagerType unitType;
    [Header ("私有变量")]
    private BattleUnit unitAction;
    private ManagerType battleType;
    private ManagerType resultType;

    /// <summary>
    /// 我方战斗单位
    /// </summary>
    private List<GameObject> ourUnitList;
    /// <summary>
    /// 敌方战斗单位 战斗结束要清除
    /// </summary>
    private List<GameObject> enemyUnitList;

    private void Awake () {
        ourUnitList = new List<GameObject> ();
        enemyUnitList = new List<GameObject> ();

        battleType = ManagerType.BATTLE_NONE;
        unitType = ManagerType.BATTLE_UNIT_NONE;
        resultType = ManagerType.BATTLE_NONE;

        isAutoBattle = false;
    }

    void Start () {
        initOur ();
    }

    // Update is called once per frame
    void Update () {
        if (battleType == ManagerType.BATTLE_NONE) {
            return;
        }

        if (unitAction != null) {
            unitAction.actionlogic ();
        }
        //-------------------------------------------------------------
        switch (battleType) {
            case ManagerType.BATTLE_READY:
                battleReady ();
                break;
            case ManagerType.BATTLE_ENTER:
                battleEnter ();
                break;
            case ManagerType.BATTLE_GAME:
                battleGame ();
                break;
            case ManagerType.BATTLE_OVER:
                break;
        }
    }

    void FixedUpdate () {
        if (battleType != ManagerType.BATTLE_GAME) {
            return;
        }
        unitLogic ();
    }

    /// <summary>
    /// 战斗开始 初始化
    /// </summary>
    public void init () {
        buttonPanel.GetComponent<Animator> ().SetTrigger ("Enter");
        moveHeadBack.SetActive (true);

        battleType = ManagerType.BATTLE_READY;
        unitType = ManagerType.BATTLE_UNIT_NONE;
        resultType = ManagerType.BATTLE_NONE;

        updateHeadInfo (ourUnitList[0]);
    }

    /// <summary>
    /// 战斗结束 清场
    /// </summary>
    public void closeLayer () {
        buttonPanel.GetComponent<Animator> ().SetTrigger ("Exit");
        moveHeadBack.SetActive (false);
        chooseIcon.SetActive (false);

        unitAction = null;
        battleType = ManagerType.BATTLE_NONE;
    }

    /// <summary>
    /// 更新头像信息
    /// </summary>
    /// <param name="unit"></param>
    public void updateHeadInfo (GameObject unit) {
        BattleUnit data = unit.GetComponent<BattleUnit> ();

        chooseIcon.transform.position = new Vector3 (
            unit.transform.position.x,
            unit.transform.position.y + data.getHeight (),
            0);
        chooseIcon.SetActive (true);

        HeadInfo info = buttonHead.GetComponent<HeadInfo> ();
        info.updateHead (data.getUnitId (), data.GetAttributeData ());
    }

    /// <summary>
    /// 显示判定结果
    /// </summary>
    /// <param name="list">判定结果</param>
    public void showWeaponJudge (List<int> list) {
        GameObject judge = GameObject.Instantiate (JudgePrefab);
        judge.transform.SetParent (battleLayer.transform);

        Judge cpp = judge.GetComponent<Judge> ();
        cpp.initJudge (list);
    }

    /// <summary>
    /// 显示敌人信息面板
    /// </summary>
    /// <param name="unit"></param>
    public void showEnemyData (BattleUnit unit) {

    }

    /// <summary>
    /// 添加目标到行动单位
    /// </summary>
    /// <param name="target">可攻击目标</param>
    /// <returns></returns>
    public bool addTargetToUnit (BattleUnit target) {
        if (unitAction != null) {
            unitAction.targetList.Add (target);
        }

        if (unitType == ManagerType.BATTLE_UNIT_ATK) {
            unitAction.setAttackType (BattleUnit.UnitType.ATTACK_ATK);
            unitAction.setActionTypeToJudge ();
            unitType = BattleManager.ManagerType.BATTLE_UNIT_NONE;

            return true;
        } else if (unitType == ManagerType.BATTLE_UNIT_SKILL) {
            int skillcount = 1;

            if (unitAction.targetList.Count >= skillcount) {
                unitAction.setAttackType (BattleUnit.UnitType.ATTACK_SKILL);
                unitAction.setActionTypeToJudge ();
                unitType = BattleManager.ManagerType.BATTLE_UNIT_NONE;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 单位结束战斗
    /// </summary>
    public void unitActionOver () {
        battleType = ManagerType.BATTLE_GAME;
        unitType = ManagerType.BATTLE_UNIT_NONE;
    }

    public ManagerType getBattleType () {
        return battleType;
    }

    //---分割线 战斗外调用------------------------------------------------------
    public void unitMove (Vector3 position) {
        var our = ourUnitList.GetEnumerator ();

        while (our.MoveNext ()) {
            BattleUnit unit = our.Current.GetComponent<BattleUnit> ();
            unit.setMoveToDoor (position);
        }
    }

    //---分割线 以下为私有成员---------------------------------------------------
    /// <summary>
    /// 创建行动条头像
    /// </summary>
    /// <param name="id">头像ID</param>
    /// <param name="camp">阵营</param>
    /// <returns></returns>
    private GameObject createMoveHead (int id, BattleUnit.UnitType camp) {
        GameObject icon = GameObject.Instantiate (moveHeadPrefab);
        icon.transform.SetParent (moveHeadBack.transform);

        MoveHead head = icon.GetComponent<MoveHead> ();
        head.init (id, movepoint[0], movepoint[1], camp);

        return icon;
    }

    private void initOur () {
        List<int> ourlist = new List<int> ();
        ourlist.Add (1);
        ourlist.Add (0);
        ourlist.Add (0);
        ourlist.Add (2);
        
        int index = 0;

        foreach (int item in ourlist) {
            if (item < 1) {
                index++;
                continue;
            }
            
            GameObject node = GameObject.Instantiate (unitPrefab);
            ourUnitList.Add (node);
            //node 黑影单元 unitPrefab
            
            node.transform.SetParent (unitLayer.transform);
            //让node和父级对象位置保持一致——目的是在Unit里生成
            node.transform.position = unitPosition[index].transform.position;
            //设置两个玩家角色node位置
            BattleUnit unit = node.GetComponent<BattleUnit> ();
            //??解决
            unit.initUnit (item, item, BattleUnit.UnitType.CAMP_OUR);
            unit.manager = this;
            unit.movIcon = createMoveHead (item, BattleUnit.UnitType.CAMP_OUR);

            unit.location = node.transform.position;

            index++;
        }
        //己方单位链表遍历结束 两个单位生成
    }

    private void initEnemy () {
        GameObject node = GameObject.Instantiate (unitPrefab);
        enemyUnitList.Add (node);

        node.transform.SetParent (unitLayer.transform);
        node.transform.position = unitPosition[4].transform.position;

        BattleUnit unit = node.GetComponent<BattleUnit> ();
        unit.initUnit (2, 2, BattleUnit.UnitType.CAMP_ENEMY);
        unit.manager = this;
        unit.movIcon = createMoveHead (2, BattleUnit.UnitType.CAMP_ENEMY);

        unit.location = node.transform.position;
    }

    /// <summary>
    /// 战斗准备 初始化敌人 显示警告
    /// </summary>
    private void battleReady () {
        enemyWarning.GetComponent<Animator> ().SetTrigger ("Show");

        initEnemy ();
        battleType = ManagerType.BATTLE_ENTER;
    }

    /// <summary>
    /// 敌人入场 对话或者入场动画
    /// </summary>
    private void battleEnter () {
        battleType = ManagerType.BATTLE_GAME;
    }

    /// <summary>
    /// 单位循环
    /// </summary>
    private void unitLogic () {
        unitAction = null;

        var our = ourUnitList.GetEnumerator ();

        while (our.MoveNext ()) {
            BattleUnit unit = our.Current.GetComponent<BattleUnit> ();

            unit.logic (Time.fixedDeltaTime);

            if (unit.isReady ()) {
                unitAction = unit;
            }
        }

        var enemy = enemyUnitList.GetEnumerator ();

        while (enemy.MoveNext ()) {
            BattleUnit unit = enemy.Current.GetComponent<BattleUnit> ();

            unit.logic (Time.fixedDeltaTime);

            if (unitAction == null) {
                if (unit.isReady ()) {
                    unitAction = unit;
                }
            }
        }

        if (unitAction != null) {
            battleType = ManagerType.BATTLE_PAUSE;
            unitAction.battleReady ();

            if (unitAction.getCampType () == BattleUnit.UnitType.CAMP_OUR) {
                if (isAutoBattle) {
                    autoBattle ();
                } else {
                    unitType = ManagerType.BATTLE_UNIT_WAIT;
                }
            } else {
                autoBattle ();
            }
        }
    }

    /// <summary>
    /// 战斗循环 死亡消失 结果判定
    /// </summary>
    private void battleGame () {
        
        for (int i = 0; i < ourUnitList.Count;) {
            BattleUnit unit = ourUnitList[i].GetComponent<BattleUnit> ();
            if (unit.isDeadAndOut ()) {
                unit.removeFromParent ();
                ourUnitList.RemoveAt (i);
            } else {
                i++;
            }
        }

        for (int i = 0; i < enemyUnitList.Count;) {
            BattleUnit unit = enemyUnitList[i].GetComponent<BattleUnit> ();
            if (unit.isDeadAndOut ()) {
                unit.removeFromParent ();
                enemyUnitList.RemoveAt (i);
            } else {
                i++;
            }
        }

        if (ourUnitList.Count <= 0) {
            resultType = ManagerType.BATTLE_FAIL;
            showReceipt();
        } else if (enemyUnitList.Count <= 0) {
            resultType = ManagerType.BATTLE_WIN;
            //显示结算界面
            showReceipt();

        }

        if (resultType != ManagerType.BATTLE_NONE) {
            battleType = ManagerType.BATTLE_NONE;

            var our = ourUnitList.GetEnumerator ();

            while (our.MoveNext ()) {
                BattleUnit unit = our.Current.GetComponent<BattleUnit> ();
                unit.clearUnit ();
            }

            battleSettlement ();
        }
    }

    private void autoBattle () {
        // 技能&普攻
        unitType = ManagerType.BATTLE_UNIT_ATK;

        // 随机目标
        if (unitAction.getCampType () == BattleUnit.UnitType.CAMP_OUR) {
            foreach (var item in enemyUnitList) {
                if (addTargetToUnit (item.GetComponent<BattleUnit> ())) {
                    break;
                }
            }
        } else if (unitAction.getCampType () == BattleUnit.UnitType.CAMP_ENEMY) {
            foreach (var item in ourUnitList) {
                if (addTargetToUnit (item.GetComponent<BattleUnit> ())) {
                    break;
                }
            }
        }
    }

    private void battleSettlement () {
        closeLayer ();
    }

    private void showReceipt()
    {
        battlereceipt.SetActive(true);
    }
}

//BattleUnit类型定义？