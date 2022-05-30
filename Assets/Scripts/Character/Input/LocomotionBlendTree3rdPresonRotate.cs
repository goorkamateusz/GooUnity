using UnityEngine;
using Goo.Characters;
using Goo.Characters.Animations.StateMachine;

public class LocomotionBlendTree3rdPresonRotate : CharacterStateMachineBehaviour
{
    [SerializeField] private string _xAxisName = "X";
    [SerializeField] private string _yAxisName = "Y";

    private Vector3 normalizedVelocity;
    private Character3rdPersonRotate _rotate;

    protected override void Initialize()
    {
        base.Initialize();
        _rotate = Character.Components.GetComponent<Character3rdPersonRotate>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        normalizedVelocity = Character.Movement.NormalizedVelocity;
        // animator.SetFloat(_xAxisName, _rotate.HorizontalAngle / 90f + normalizedVelocity.x);
        animator.SetFloat(_xAxisName, normalizedVelocity.x);
        animator.SetFloat(_yAxisName, normalizedVelocity.z);
    }
}
