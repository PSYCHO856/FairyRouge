//属性文件

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-17
/// Author: D.xy
/// 战斗属性
/// </summary>
public class AttributeData {
    public enum AttrType {
        ATTR_ATK = 1,
        ATTR_SPEED,
        ATTR_MAGIC,
        ATTR_CRIT,
        ATTR_MAGIC_DEF,
        ATTR_ATK_DEF,
        ATTR_HP,
        ATTR_LUCK,
        ATTR_ATK_VALUE,
        ATTR_DODGE_VALUE,
        ATTR_MAGIC_VALUE,
        ATTR_CRIT_VALUE,
        ATTR_MAGIC_DEF_VALUE,
        ATTR_ATK_DEF_VALUE,
        ATTR_HP_VALUE
    }

    /// <summary>
    /// 力量 物理攻击=初始攻击+int（等级*力量*0.01）
    /// </summary>
    private float atk = 0f;
    /// <summary> 
    /// 敏捷 闪避=初始闪避+int（等级*敏捷*0.1）
    /// </summary>
    private float speed = 0f;
    /// <summary>
    /// 智力 魔法攻击=初始攻击+int（等级*智力*0.01）
    /// </summary>
    private float magic = 0f;
    /// <summary>
    /// 感知 暴击率=初始暴击+int（等级*感知*0.01）
    /// </summary>
    private float crit = 0f;
    /// <summary>
    /// 才能 魔抗=初始魔抗+int（等级*才能*0.01）
    /// </summary>
    private float magicdef = 0f;
    /// <summary>
    /// 意志 护甲=初始护甲+int（等级*意志*0.01）
    /// </summary>
    private float atkdef = 0f;
    /// <summary>
    /// 体质 生命值=初始生命+int（等级*体质*0.1）
    /// </summary>
    private float hp = 0f;
    /// <summary>
    /// 运气
    /// </summary>
    private float luck = 0f;

    //---分割线 以下为初始值只在初始化赋值---------------------------------------------
    // 初始攻击
    private float atkValue = 0;
    // 初始闪避
    private float dodgeValue = 0;
    // 初始魔法攻击
    private float magicValue = 0;
    // 初始暴击率
    private float critValue = 0;
    // 初始魔抗
    private float magicdefValue = 0;
    // 初始护甲
    private float atkdefValue = 0;
    // 初始生命值
    private float hpValue = 0;

    /// <summary>
    /// 等级
    /// </summary>
    public int level { get; private set; }

    public void initPlayer (int uid, int lv) {
        CSVHelper csv = CSVManager.getInstance ().getCsvByName ("unit");

        if (csv == null) {
            return;
        }
        var data = csv.getColData (uid);

        atk = Convert.ToSingle (data[10]);
        speed = Convert.ToSingle (data[11]);
        hp = Convert.ToSingle (data[12]);
        crit = Convert.ToSingle (data[13]);
        magic = Convert.ToSingle (data[14]);
        atkdef = Convert.ToSingle (data[15]);
        magicdef = Convert.ToSingle (data[16]);
        luck = Convert.ToSingle (data[17]);

        hpValue = Convert.ToSingle (data[18]);
        atkValue = Convert.ToSingle (data[10]);
        atkdefValue = Convert.ToSingle (data[10]);
        magicdefValue = Convert.ToSingle (data[10]);
        dodgeValue = Convert.ToSingle (data[10]);

        hpValue = hpValue + (int) (lv * hp * 0.1f);

        this.level = lv;
    }

    public float toValue (AttrType type) {
        float value = 0;

        switch (type) {
            case AttrType.ATTR_ATK:
                value = atk;
                break;
            case AttrType.ATTR_SPEED:
                value = speed;
                break;
            case AttrType.ATTR_MAGIC:
                value = magic;
                break;
            case AttrType.ATTR_CRIT:
                value = crit;
                break;
            case AttrType.ATTR_MAGIC_DEF:
                value = magicdef;
                break;
            case AttrType.ATTR_ATK_DEF:
                value = atkdef;
                break;
            case AttrType.ATTR_HP:
                value = hp;
                break;
            case AttrType.ATTR_LUCK:
                value = luck;
                break;
            case AttrType.ATTR_ATK_VALUE:
                value = atkValue + (int) (level * atk * 0.01f);
                break;
            case AttrType.ATTR_DODGE_VALUE:
                value = dodgeValue + (int) (level * speed * 0.1f);
                break;
            case AttrType.ATTR_MAGIC_VALUE:
                value = magicValue + (int) (level * magic * 0.01f);
                break;
            case AttrType.ATTR_CRIT_VALUE:
                value = critValue + (int) (level * crit * 0.01f);
                break;
            case AttrType.ATTR_MAGIC_DEF_VALUE:
                value = magicdefValue + (int) (level * magicdef * 0.01f);
                break;
            case AttrType.ATTR_ATK_DEF_VALUE:
                value = atkdefValue + (int) (level * atkdef * 0.01f);
                break;
            case AttrType.ATTR_HP_VALUE:
                value = hpValue;
                break;
        }
        return value;
    }

    public string toStringByType (AttrType type) {
        int value = (int) toValue (type);
        return value.ToString ();
    }

    public void setAttribute (AttrType type, float value) {
        switch (type) {
            case AttrType.ATTR_ATK:
                atk = value;
                break;
            case AttrType.ATTR_SPEED:
                speed = value;
                break;
            case AttrType.ATTR_MAGIC:
                magic = value;
                break;
            case AttrType.ATTR_CRIT:
                crit = value;
                break;
            case AttrType.ATTR_MAGIC_DEF:
                magicdef = value;
                break;
            case AttrType.ATTR_ATK_DEF:
                atkdef = value;
                break;
            case AttrType.ATTR_HP:
                hp = value;
                break;
            case AttrType.ATTR_LUCK:
                luck = value;
                break;
            case AttrType.ATTR_ATK_VALUE:
                atkValue = value;
                break;
            case AttrType.ATTR_DODGE_VALUE:
                dodgeValue = value;
                break;
            case AttrType.ATTR_MAGIC_VALUE:
                magicValue = value;
                break;
            case AttrType.ATTR_CRIT_VALUE:
                critValue = value;
                break;
            case AttrType.ATTR_MAGIC_DEF_VALUE:
                magicdefValue = value;
                break;
            case AttrType.ATTR_ATK_DEF_VALUE:
                atkdefValue = value;
                break;
            case AttrType.ATTR_HP_VALUE:
                hpValue = value;
                break;
        }
    }

    public void setPercentage (AttrType type, float per) {
        setAttribute (type, toValue (type) * per);
    }

    public void addAttribute (AttrType type, float value) {
        setAttribute (type, toValue (type) + value);
    }
    
    //修改属性222222

    public void addPercentage (AttrType type, float per) {
        addAttribute (type, toValue (type) * per * 0.01f);
    }

    public void setValue (AttributeData data) {
        /*foreach (AttrType key in Enum.GetValues (typeof (AttrType))) {
            setAttribute (key, data.toValue (key));
        }*/
        var e = Enum.GetValues (typeof (AttrType)).GetEnumerator ();

        while (e.MoveNext ()) {
            AttrType key = (AttrType) e.Current;
            setAttribute (key, data.toValue (key));
        }

        this.level = data.level;
    }

    //等级

    public int getPower () {
        return 0;
    }
}