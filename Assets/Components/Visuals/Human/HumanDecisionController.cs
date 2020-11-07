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

    public void updateDecision()
    {
        currentDecision = Decision.WAITING;
    }
}