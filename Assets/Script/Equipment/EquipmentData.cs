using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
/// <summary>
/// 装备信息类
/// </summary>
public class EquipmentData : MonoBehaviour
{
    /// <summary>
    /// 装备id
    /// </summary>
    public static int _equipmentID;
    /// <summary>
    /// 装备名称
    /// </summary>
    public static string _name;
    /// <summary>
    /// 当前改造名
    /// </summary>
    public static string _currentForging;
    /// <summary>
    /// 需要的改造Type1
    /// </summary>
    public static List<string> _forging1;
    /// <summary>
    /// 需要的改造Type2
    /// </summary>
    public static List<string> _forging2;
    /// <summary>
    /// 需要的改造Type3
    /// </summary>
    public static List<string> _forging3;
    /// <summary>
    /// 已经进行的改造
    /// </summary>
    public static List<string> _hasForging;
    /// <summary>
    /// 前置条件
    /// </summary>
    public static string _preconditions;
    /// <summary>
    /// int是角色ID，存放角色相关
    /// </summary>
    public static Dictionary<int, Dictionary<int, List<string>>> dic = new Dictionary<int, Dictionary<int, List<string>>>();
    /// <summary>
    /// 存放两张表
    /// </summary>
    public static Dictionary<int, List<string>> equ = new Dictionary<int, List<string>>();
    /// <summary>
    /// 改造点数
    /// </summary>
    public static int _count = 5;
    /// <summary>
    /// 存放count
    /// </summary>
    public static Dictionary<int, int> count = new Dictionary<int, int>();
    /// <summary>
    /// 改造材料1
    /// </summary>
    public static int _material1;
    /// <summary>
    /// 材料1所需数量
    /// </summary>
    public static int _material1Num;
    /// <summary>
    /// 拥有的材料数量
    /// </summary>
    public static int _material1Get;
    /// <summary>
    /// 改造材料2
    /// </summary>
    public static int _material2;
    /// <summary>
    /// 材料2所需数量
    /// </summary>
    public static int _material2Num;
    /// <summary>
    /// 拥有的材料数量
    /// </summary>
    public static int _material2Get;
    /// <summary>
    /// 改造材料3
    /// </summary>
    public static int _material3;
    /// <summary>
    /// 材料3所需数量
    /// </summary>
    public static int _material3Num;
    /// <summary>
    /// 拥有的材料数量
    /// </summary>
    public static int _material3Get;
    /// <summary>
    /// 改造编号
    /// </summary>
    public static int _forgingId;
}
