using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Equipment : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();
	 private  static Dictionary<int ,Sheet> sheetDic=new Dictionary<int, Sheet>();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
		private static readonly Dictionary<int, Param> dicParams = new Dictionary<int, Param>();
		 /// <summary>
        ///     get one data
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public  Param GetData(int id)
        {
            if (dicParams.ContainsKey(id))
            {
                return dicParams[id];
            }
            Debug.LogError("No This id!");
            return null;
        }

        public void SetDic()
        {
            dicParams.Clear();
            for (int i = 0, iMax = list.Count; i < iMax; i++)
            {
                dicParams.Add(list[i].id, list[i]);
            }
        }
	}

	[System.SerializableAttribute]
	public class Param
	{
		
        /// <summary>
        /// 装备ID
        /// </summary>
        public  int id;

        /// <summary>
        /// 装备种类ID
        /// </summary>
        public  int equipTypeID;

        /// <summary>
        /// 装备重量（单位kg）
        /// </summary>
        public  int weight;

        /// <summary>
        /// 攻击
        /// </summary>
        public  int attack;

        /// <summary>
        /// 护甲
        /// </summary>
        public  int armor;

        /// <summary>
        /// 魔抗
        /// </summary>
        public  int MagicResistance;

        /// <summary>
        /// 力量
        /// </summary>
        public  int strength;

        /// <summary>
        /// 敏捷
        /// </summary>
        public  int agile;

        /// <summary>
        /// 体质
        /// </summary>
        public  int body;

        /// <summary>
        /// 感知
        /// </summary>
        public  int perception;

        /// <summary>
        /// 智力
        /// </summary>
        public  int intelligence;

        /// <summary>
        /// 意志
        /// </summary>
        public  int will;

        /// <summary>
        /// 才能
        /// </summary>
        public  int ability;

        /// <summary>
        /// 判定属性
        /// </summary>
        public  int propertyDecision;

        /// <summary>
        /// 判定次数
        /// </summary>
        public  int decisionTime;

        /// <summary>
        /// 特殊要求
        /// </summary>
        public  int specialRequireID;

        /// <summary>
        /// 改造点数
        /// </summary>
        public  int forgingNum;

        /// <summary>
        /// 所需改造1
        /// </summary>
        public  List<string> forging1;

        /// <summary>
        /// 所需改造2
        /// </summary>
        public  List<string> forging2;

        /// <summary>
        /// 所需改造3
        /// </summary>
        public  List<string> forging3;

        /// <summary>
        /// 已进行的改造
        /// </summary>
        public  List<string> hasForging;

	}

	    public void SetDic()
    {
        for (int i = 0, iMax = sheets.Count; i < iMax; i++)
        {
            var enumId=Enum.Parse(typeof (SheetName), sheets[i].name);
            sheetDic.Add((int)enumId, sheets[i]);
            sheets[i].SetDic();
        }
    }

	 /// <summary>
    ///     get one sheet
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    public static Sheet GetSheet(int id)
    {
        if (sheetDic.ContainsKey(id))
        {
            return sheetDic[id];
        }
        Debug.LogError("No This Sheet!");
        return null;
    }

	  public enum SheetName
    {
       Sheet1,

    }
}

