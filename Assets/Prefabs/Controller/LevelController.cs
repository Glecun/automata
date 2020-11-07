using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void Start()
    {
        var prefabList = GameObject.Find("ApplicationController").GetComponent<PrefabList>();

        InstantiateUtils.Instantiate(prefabList.get("BackgroundGrid"), new Vector3(0,0), Quaternion.identity, transform);

        //Trees
        InstantiateUtils.Instantiate(prefabList.get("Tree"), new Vector3(20,3), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(prefabList.get("Tree"), new Vector3(20,4), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(prefabList.get("Tree"), new Vector3(20,5), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(prefabList.get("Tree"), new Vector3(19,4), Quaternion.identity, transform);
    }
}
