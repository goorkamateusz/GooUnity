using UnityEngine;
using Assets.Goo.Characters.General;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.Characters.Animations.StateMachine
{
    public class CharacterStateMachineBehaviour : StateMachineBehaviour
    {
        private Character character;

        public Character Character => character;

        /// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Character == null)
                LinkToCharacter(animator);
        }

        /// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        /// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        /// OnStateMove is called right after Animator.OnAnimatorMove()
        /// Implement code that processes and affects root motion
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        /// OnStateIK is called right after Animator.OnAnimatorIK()
        /// Implement code that sets up animation IK (inverse kinematics)
        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        private void LinkToCharacter(Animator animator)
        {
            var reference = animator.GetComponent<CharacterReferenceComponent>();
            if (reference)
            {
                character = reference.Character;
                Initialize();
            }
#if UNITY_EDITOR
            else
            {
                this.LogError($"StateMachineBehaviour need {typeof(CharacterReferenceComponent)} script");
            }
#endif
        }

        protected virtual void Initialize() { }
    }
}