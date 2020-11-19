using TMPro;
using UnityEngine;

public class PanelInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodAmount = null;
    private TownHall townHall;

    private void Update()
    {
        initWhenFound(); //TODO : trouver moyen de faire mieux
        woodAmount.text = townHall != null ? townHall.getResource(ResourceEnum.WOOD).amount.ToString() : "0";
    }

    private void initWhenFound()
    {
        if (townHall) return;
        var townHallGameObject = GameObject.Find("TownHall");
        townHall = townHallGameObject != null ? townHallGameObject.GetComponent<TownHall>() : null;
    }
}