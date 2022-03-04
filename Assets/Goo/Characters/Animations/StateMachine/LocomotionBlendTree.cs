using UnityEngine;

namespace Assets.Goo.Characters.Animations.StateMachine
{
    public class LocomotionBlendTree : CharacterStateMachineBehaviour
    {
        [SerializeField] private string _xAxisName = "X";
        [SerializeField] private string _yAxisName = "Y";

        private Vector3 normalizedVelocity;

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            normalizedVelocity = Character.Movement.NormalizedVelocity;
            animator.SetFloat(_xAxisName, normalizedVelocity.x);
            animator.SetFloat(_yAxisName, normalizedVelocity.z);
        }
    }
}