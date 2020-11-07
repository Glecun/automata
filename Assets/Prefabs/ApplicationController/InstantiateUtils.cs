using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtils : MonoBehaviour
{
    public static GameObject Instantiate(GameObject gameObject)
    {
        Debug.Log("lol");
        var gameObjectCreated = UnityEngine.Object.Instantiate(gameObject);
        return setName(gameObjectCreated, gameObject);
    }
    public static GameObject Instantiate(GameObject gameObject, Transform parent)
    {
        var gameObjectCreated = UnityEngine.Object.Instantiate(gameObject, parent);
        return setName(gameObjectCreated, gameObject);
    }

    public static GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
    {
        var gameObjectCreated = UnityEngine.Object.Instantiate(gameObject, position, rotation, parent);
        return setName(gameObjectCreated, gameObject);
    }

    private static GameObject setName(GameObject gameObjectCreated, GameObject gameObject)
    {
        gameObjectCreated.name = gameObject.name;
        return gameObjectCreated;
    }
}
