using System;
using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private RuntimeAnimatorController[] animatorControllers = null;

    private HumanController humanController;

    private static readonly int IsMovingLabel = Animator.StringToHash("isMoving");
    public bool isMoving = false;

    private static readonly int isDoingLabel = Animator.StringToHash("isDoing");
    public bool isDoing = false;

    private void Start()
    {
        humanController = gameObject.GetComponent<HumanController>();
        switch (humanController.GenderEnum)
        {
            case GenderEnum.MALE:
                animator.runtimeAnimatorController = animatorControllers[0];
                break;
            case GenderEnum.FEMALE:
                animator.runtimeAnimatorController = animatorControllers[1];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        updateAnimations();
    }

    public void updateAnimations()
    {
        animator.SetBool(IsMovingLabel, isMoving);
        animator.SetBool(isDoingLabel, isDoing);
    }

    public void resetAnimations()
    {
        isDoing = false;
        isMoving = false;
    }
}