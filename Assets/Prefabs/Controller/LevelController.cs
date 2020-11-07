using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void Awake()
    {
        InstantiateUtils.Instantiate(PrefabList.getPrefab("BackgroundGrid"), new Vector3(0,0), Quaternion.identity, transform);
        InstantiateUtils.Instantiate(PrefabList.getPrefab("Tree"), new Vector3(0, 0), Quaternion.identity, transform);
    }
}
