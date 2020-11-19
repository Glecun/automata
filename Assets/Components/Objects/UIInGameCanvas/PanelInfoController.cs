using TMPro;
using UnityEngine;

public class PanelInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodAmount = null;
    private TownHall townHall;

    private void Update()
    {
        townHall = Utils.initWhenFound(townHall, () => GameObject.Find("TownHall").GetComponent<TownHall>());
        woodAmount.text = townHall != null ? townHall.getResource(ResourceEnum.WOOD).amount.ToString() : "0";
    }
}