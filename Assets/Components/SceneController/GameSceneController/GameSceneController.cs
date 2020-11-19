using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private GameObject levelController = null;
    [SerializeField] private GameObject UiInGameCanvas = null;

    public GridScript grid;

    private void Awake()
    {
        var settings = GameObject.Find("ApplicationController").GetComponent<Settings>();

        grid = new GridScript(settings.cellSize, settings.numberOfCellInLine, settings.numberOfCellInColumn);
        InstantiateUtils.Instantiate(levelController, transform);
        InstantiateUtils.Instantiate(UiInGameCanvas, transform);
    }
}