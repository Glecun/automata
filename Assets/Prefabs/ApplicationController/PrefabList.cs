using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PrefabList : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> prefabList;

    public static List<GameObject> staticPrefabList;

    private void Awake()
    {
        staticPrefabList = prefabList;
    }
    public static GameObject getPrefab(string prefabName)
    {
        var thePrefab = staticPrefabList.Find(prefab => prefab.name == prefabName);
        if (!thePrefab)
        {
            throw new Exception("Prefab " + prefabName + " doesn't exists");
        }
        return thePrefab;
    }
}
