using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoleInformation : ScriptableObject
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
        /// 名字
        /// </summary>
        public  string name;

        /// <summary>
        /// 性别
        /// </summary>
        public  string sex;

        /// <summary>
        /// 年龄
        /// </summary>
        public  int age;

        /// <summary>
        /// 职业ID
        /// </summary>
        public  int occupationID;

        /// <summary>
        /// 身高cm
        /// </summary>
        public  string height;

        /// <summary>
        /// 体重KG
        /// </summary>
        public  string weight;

        /// <summary>
        /// 技能1ID
        /// </summary>
        public  int skillID1;

        /// <summary>
        /// 技能2ID
        /// </summary>
        public  int skillID2;

        /// <summary>
        /// 奥义ID
        /// </summary>
        public  int MentalID;

        /// <summary>
        /// 角色描述
        /// </summary>
        public  string describe;

        /// <summary>
        /// 头像
        /// </summary>
        public  int title;

        /// <summary>
        /// 角色等级
        /// </summary>
        public  int Level;

        /// <summary>
        /// 羁绊值
        /// </summary>
        public  int Fetters;

        /// <summary>
        /// 装备id
        /// </summary>
        public  int EquipmentID;

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

