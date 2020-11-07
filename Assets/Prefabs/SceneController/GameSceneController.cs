using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public GridScript grid;

    private void Awake()
    {
        var prefabList = GameObject.Find("ApplicationController").GetComponent<PrefabList>();
        var settings = GameObject.Find("ApplicationController").GetComponent<Settings>();

        grid = new GridScript(settings.cellSize, settings.numberOfCellInLine, settings.numberOfCellInColumn);
        InstantiateUtils.Instantiate(prefabList.get("LevelController"), transform);
    }
}
