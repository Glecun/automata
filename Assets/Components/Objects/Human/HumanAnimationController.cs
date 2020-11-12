using UnityEngine;

public class HumanAnimationController
{
    private readonly Animator animator;

    private static readonly int IsMovingLabel = Animator.StringToHash("isMoving");
    public bool isMoving = false;

    private static readonly int isDoingLabel = Animator.StringToHash("isDoing");
    public bool isDoing = false;


    public HumanAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void updateAnimations()
    {
        animator.SetBool(IsMovingLabel, isMoving);
        animator.SetBool(isDoingLabel, isDoing);
    }
}