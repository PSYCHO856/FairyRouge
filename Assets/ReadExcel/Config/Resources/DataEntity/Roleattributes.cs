using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Roleattributes : ScriptableObject
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
        /// 角色ID
        /// </summary>
        public  int id;

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
        /// 魅力
        /// </summary>
        public  int luck;

        /// <summary>
        /// 初始生命
        /// </summary>
        public  int hp;

        /// <summary>
        /// 生命成长
        /// </summary>
        public  float hpGrowth;

        /// <summary>
        /// 初始魔法值
        /// </summary>
        public  int mp;

        /// <summary>
        /// 魔力成长
        /// </summary>
        public  float mpGrowth;

        /// <summary>
        /// 初始攻击
        /// </summary>
        public  int attack;

        /// <summary>
        /// 攻击成长
        /// </summary>
        public  float attackGrowth;

        /// <summary>
        /// 初始魔攻
        /// </summary>
        public  int Mattack;

        /// <summary>
        /// 魔攻成长
        /// </summary>
        public  float MattackGrowth;

        /// <summary>
        /// 初始护甲
        /// </summary>
        public  int armor;

        /// <summary>
        /// 初始魔抗
        /// </summary>
        public  int MagicRes;

        /// <summary>
        /// 初始状态抗性
        /// </summary>
        public  int Resistance;

        /// <summary>
        /// 初始闪避
        /// </summary>
        public  int Dodge;

        /// <summary>
        /// 闪避成长
        /// </summary>
        public  float DodgeGrowth;

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

