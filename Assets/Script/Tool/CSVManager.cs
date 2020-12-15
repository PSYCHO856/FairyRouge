using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Created on: 2020-8-18
/// Author: D.xy
/// CSV 管理类
/// </summary>
public class CSVManager {
    static CSVManager instance;

    static public CSVManager getInstance () {
        if (instance == null) {
            instance = new CSVManager ();
        }
        return instance;
    }

    public CSVManager () {
        csvList = new Dictionary<string, CSVHelper> ();

        loadCSV ("unit");
        loadCSV ("equip");
    }

    private Dictionary<string, CSVHelper> csvList;

    public CSVHelper getCsvByName (string name) {
        if (csvList.ContainsKey (name)) {
            return csvList[name];
        }
        return null;
    }

    public void loadCSV (string name) {
        CSVHelper csv = new CSVHelper ();
        csv.loadCSV (name);

        csvList.Add (name, csv);
    }
}