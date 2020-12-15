using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-1
/// Author: D.xy
/// 资源管理类
/// </summary>
public class ResourcesManager {
    static ResourcesManager instance;

    static public ResourcesManager getInstance () {
        if (instance == null) {
            instance = new ResourcesManager ();
        }
        return instance;
    }

    private Dictionary<string, Sprite[]> resDictionary;

    public ResourcesManager () {
        resDictionary = new Dictionary<string, Sprite[]> ();
    }
    public void addResources (string path) {
        Sprite[] list = Resources.LoadAll<Sprite> (path);
        resDictionary.Add (path, list);
    }

    public Sprite getSprite (string path, int code) {
        if (!resDictionary.ContainsKey (path)) {
            addResources (path);
        }
        Sprite[] list = resDictionary[path];

        return list[code];
    }
}