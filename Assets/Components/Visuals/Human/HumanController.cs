using UnityEngine;

public class HumanController : MonoBehaviour
{
    private HumanDecisionController humanDecisionController;

    public HumanController()
    {
        humanDecisionController = new HumanDecisionController();
    }
}