using UnityEngine;

public class LocomotionBlendTree : StateMachineBehaviour
{
    // todo seperate CharacterStateMachine
    [SerializeField] private string _XAxisName;
    [SerializeField] private string _YAxisName;

    private Character character;
    private Character3rdPersonMovement movement;
    private AnimatorHandler animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
    }

    private void Initialize(Animator animator)
    {
        if (character == null)
        {
            var charAnimator = animator.GetComponent<CharacterAnimatorReference>();
            if (charAnimator != null)
            {
                character = charAnimator.Character;
                movement = character.Movement as Character3rdPersonMovement;
                this.animator = character.AnimatorHandler;
            }
            #if UNITY_EDITOR
            else
            {
                Debug.LogWarning("StateMachineBehaviour need CharacterAnimatorReference script");
            }
            #endif
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movement.Movement.Direction.magnitude > 0.01f)
        {
            animator.SetFloat(_XAxisName, movement.Movement.Direction.x);
            animator.SetFloat(_YAxisName, movement.Movement.Direction.z);
        }
        else
        {
            this.animator.Animator.Play("Idle", 0.5f); // todo temporary
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
