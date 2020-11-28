using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public enum Decision
{
    NONE,
    JUST_BORN,
    WAITING,
    GATHER_WOOD,
    GATHER_FOOD
}

public class HumanDecisionController : MonoBehaviour
{
    [SerializeField] public HumanActionController humanActionController;

    private TownHall townHall;
    private bool justBornActionDone = false;

    private void Update()
    {
        townHall = Utils.initWhenFound(townHall, () => GameObject.Find("TownHall").GetComponent<TownHall>());
        checkIfJustBornActionDone();
    }

    private void checkIfJustBornActionDone()
    {
        if (justBornActionDone) return;
        var humanJustBornAction = GetComponent<HumanJustBornAction>();
        if (humanJustBornAction != null && humanJustBornAction.isFinished())
        {
            justBornActionDone = true;
        }
    }

    public Decision getNewDecision(Decision currentDecision)
    {
        if (!justBornActionDone)
        {
            return Decision.JUST_BORN;
        }

        if (townHall != null)
        {
            var decisions = new List<(Decision, bool)>
            {
                (Decision.GATHER_WOOD, townHall.getResource(ResourceEnum.WOOD).amount <= 50),
                (Decision.GATHER_FOOD, townHall.getResource(ResourceEnum.FOOD).amount <= 50)
            };

            var decisionsPossible = decisions
                .FindAll(aDecision => aDecision.Item2)
                .OrderBy(a => Guid.NewGuid()).ToList()
                .Select(aDecision => aDecision.Item1).ToList();

            if (decisionsPossible.Contains(currentDecision))
            {
                return currentDecision;
            }

            if (!decisionsPossible.IsEmpty())
            {
                return decisionsPossible.First();
            }
        }

        return Decision.WAITING;
    }
}