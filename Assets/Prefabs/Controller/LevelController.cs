using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void Awake()
    {
        var prefabList = GameObject.Find("ApplicationController").GetComponent<PrefabList>();

        InstantiateUtils.Instantiate(prefabList.get("BackgroundGrid"), new Vector3(0,0), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(prefabList.get("Tree"), new Vector3(0, 0), Quaternion.identity, transform);
    }
}
