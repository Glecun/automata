using UnityEngine;

public class HumanAnimationController
{
    private readonly Animator animator;

    private static readonly int IsMovingLabel = Animator.StringToHash("isMoving");
    public bool isMoving;

    public HumanAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void updateAnimations()
    {
        animator.SetBool(IsMovingLabel, isMoving);
    }
}