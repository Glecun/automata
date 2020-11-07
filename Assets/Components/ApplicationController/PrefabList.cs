using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PrefabList : MonoBehaviour
{
    public List<GameObject> prefabList;

    public GameObject get(string prefabName)
    {
        var thePrefab = prefabList.Find(prefab => prefab.name == prefabName);
        if (!thePrefab)
        {
            throw new Exception("Prefab " + prefabName + " doesn't exists");
        }
        return thePrefab;
    }
}
