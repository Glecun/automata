using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField]
    public GridScript grid;

    private void Awake()
    {
        grid = new GridScript(16, 40, 40);
        Instantiate(PrefabList.getPrefab("LevelController"), transform);
    }
}
