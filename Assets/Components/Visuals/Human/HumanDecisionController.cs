public enum Decision
{
    WAITING
}

public class HumanDecisionController
{
    public Decision currentDecision;

    public HumanDecisionController()
    {
        currentDecision = Decision.WAITING;
    }
}