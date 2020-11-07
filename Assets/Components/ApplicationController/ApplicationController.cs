using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private GameObject gameSceneController = null;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //Avoid duplicates
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        InstantiateUtils.Instantiate(gameSceneController);
    }
}