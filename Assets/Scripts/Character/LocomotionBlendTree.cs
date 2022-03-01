using UnityEngine;

public class LocomotionBlendTree : CharacterStateMachineBehaviour
{
    [SerializeField] private string _xAxisName = "X";
    [SerializeField] private string _yAxisName = "Y";

    private CharacterMovement movement;
    private Character3rdPersonMovement movement3rdPerson; // todo create needed interfce in CharacterMovement
    private AnimatorHandler animator; // todo temporary

    protected override void Initialize()
    {
        movement = Character.Movement;
        movement3rdPerson = Character.Movement as Character3rdPersonMovement;
        this.animator = Character.AnimatorHandler;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movement3rdPerson.Movement.Direction.magnitude > 0.01f)
        {
            animator.SetFloat(_xAxisName, movement3rdPerson.Movement.Direction.x);
            animator.SetFloat(_yAxisName, movement3rdPerson.Movement.Direction.z);
        }
        else
        {
            this.animator.Animator.Play("Idle", 0.5f);
        }
    }
}
