using System;
using UnityEngine;
using static Utils;

public class HumanController : MonoBehaviour
{
    public GameObject infoPopupPrefab;
    private HumanDecisionController humanDecisionController;
    private HumanActionController humanActionController;
    public HumanMovementController humanMovementController;
    public HumanResourceController humanResourceController;

    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;


    [NonSerialized] public const float speed = 4f;
    [NonSerialized] public GenderEnum GenderEnum;

    private void Awake()
    {
        GenderEnum = getRandomGender();

        humanResourceController = new HumanResourceController();
        humanMovementController = gameObject.AddComponent<HumanMovementController>();

        humanDecisionController = gameObject.AddComponent<HumanDecisionController>();
        humanActionController = gameObject.AddComponent<HumanActionController>();
        betweenEachDecisionMaking = gameObject.AddComponent<Countdown>();
    }

    private GenderEnum getRandomGender()
    {
        var values = Enum.GetValues(typeof(GenderEnum));
        return (GenderEnum) values.GetValue(random(0, values.Length - 1));
    }

    private void Update()
    {
        doAndWait(updateDecision, betweenEachDecisionMaking, durationBetweenEachDecisionMaking);
    }

    private void updateDecision()
    {
        humanActionController.doAction(humanDecisionController.getNewDecision());
    }
}

public enum GenderEnum
{
    MALE,
    FEMALE
}