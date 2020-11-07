using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    public GameSceneController gameSceneController;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //Avoid duplicates
        if (FindObjectsOfType(GetType()).Length > 1) { Destroy(gameObject); }

        Instantiate(gameSceneController);
    }

}
