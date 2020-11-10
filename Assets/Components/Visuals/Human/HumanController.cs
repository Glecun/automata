using System;
using Unity.Mathematics;
using UnityEngine;
using static Utils;

public class HumanController : MonoBehaviour
{
    public HumanAnimationController humanAnimationController;
    public Animator animator = null;
    private HumanDecisionController humanDecisionController;
    private HumanActionController humanActionController;


    private Countdown betweenEachDecisionMaking;
    private const float durationBetweenEachDecisionMaking = 1f;

    private GameSceneController gameSceneController;
    [NonSerialized] public float speed = 4f;

    private void Awake()
    {
        humanAnimationController = new HumanAnimationController(animator);
        humanDecisionController = new HumanDecisionController();
        humanActionController = new HumanActionController(gameObject);

        betweenEachDecisionMaking = gameObject.AddComponent<Countdown>();
    }

    private void Start()
    {
        gameSceneController = GameObject.Find("GameSceneController").GetComponent<GameSceneController>();
    }

    private void Update()
    {
        waitOrDo(updateDecision, betweenEachDecisionMaking, durationBetweenEachDecisionMaking);
        humanAnimationController.updateAnimations();
    }

    private void updateDecision()
    {
        humanDecisionController.updateDecision();
        humanActionController.doAction(humanDecisionController.currentDecision);
    }

    public PathInProgress goTo(int2 end)
    {
        var start = getPosition();
        return gameSceneController.grid.calculatePath(start, end);
    }

    public int2 getPosition()
    {
        var position = gameObject.transform.position;
        return new int2((int) position.x, (int) position.y);
    }
}