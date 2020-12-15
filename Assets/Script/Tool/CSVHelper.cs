using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-8-18
/// Author: D.xy
/// CSV 读取
/// </summary>
public class CSVHelper {

    private string[] dataArray;

    /// <summary>
    /// 初始化了dataArray数组 该数组存放csv表中的整条数据
    /// </summary>
    /// <param name="path"></param>
    public void loadCSV (string path) {
        TextAsset binAsset = Resources.Load<TextAsset> ("Configdata/" + path);

        string dataString = binAsset.text.Replace ("\r\n", "\n");
        dataString = dataString.Replace ("\n\r", "\n");
        dataArray = dataString.Split ('\n');
    }

    /// <summary>
    /// 找title
    /// 遍历this.dataArray中数据条并切割 在第i条取第一个元素与函数形参idNo比较找到所需数据条
    /// 再通过colNo下标获取属性数据
    /// </summary>
    /// <param name="idNo"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public string getDataByIDandTitle (int idNo, string title) {
        int colNo = findColNoByTitle (title);

        if (colNo < 0) {
            return "";
        }

        string idNoStr = idNo.ToString ();

        string cellData = "";

        for (int i = 0; i < this.dataArray.Length; i++) {
            string[] colArr = this.dataArray[i].Split (',');
            if (colArr[0] == idNoStr) {
                cellData = colArr[colNo];
                break;
            }
        }
        return cellData;
    }
    public int getIntDataByIDandTitle (int idNo, string title) {
        return Convert.ToInt32 (getDataByIDandTitle (idNo, title));
    }

    /// <summary>
    /// unit表的读取
    /// 按id返回整条数据 在AttributeData里裁剪
    /// </summary>
    /// <param name="idNo"></param>
    /// <returns></returns>
    public string[] getColData (int idNo) {
        string idNoStr = idNo.ToString ();

        for (int i = 0; i < this.dataArray.Length; i++) {
            string[] colArr = this.dataArray[i].Split (',');

            if (colArr[0] == idNoStr) {
                return colArr;
            }
        }
        return null;
    }

    public int getRowLength () {
        return dataArray.Length;
    }

    /// <summary>
    /// 为什么this.dataArray[1]:equip的csv文件第0行为汉字 第1行为表头
    /// 返回属性的下标
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    private int findColNoByTitle (string title) {
        int colNo = -1;

        if (dataArray.Length <= 0) {
            return colNo;
        }

        string[] colArr = this.dataArray[1].Split (',');

        for (int i = 0; i < colArr.Length; i++) {
            if (colArr[i] == title) {
                colNo = i;
                break;
            }
        }
        return colNo;
    }

}