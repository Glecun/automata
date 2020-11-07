using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //Avoid duplicates
        if (FindObjectsOfType(GetType()).Length > 1) { Destroy(gameObject); }

        InstantiateUtils.Instantiate(PrefabList.getPrefab("GameSceneController"));
    }

}
