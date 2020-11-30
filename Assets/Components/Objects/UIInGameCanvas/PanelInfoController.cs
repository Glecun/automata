using TMPro;
using UnityEngine;

public class PanelInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodAmount = null;
    [SerializeField] private TextMeshProUGUI foodAmount = null;
    private TownHall townHall;

    private void Update()
    {
        townHall = Utils.initWhenFound(townHall, () => GameObject.Find("TownHall").GetComponent<TownHall>());
        woodAmount.text = townHall != null ? townHall.getResource(ResourceEnum.WOOD).amount.ToString() : "0";
        foodAmount.text = townHall != null ? townHall.getResource(ResourceEnum.FOOD).amount.ToString() : "0";
    }
}