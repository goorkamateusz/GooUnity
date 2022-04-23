using UnityEngine;
using Assets.Goo.Characters.Animations.StateMachine;

public class RotationPose : CharacterStateMachineBehaviour
{
    private const int MAX_VALUE = 90;
    [SerializeField] private string _leftState = "left turn";
    [SerializeField] private string _rightState = "right turn";
    [SerializeField] private string _idleState = "None";
    [SerializeField] private string _float = "Turn";

    private Character3rdPersonRotate _rotate;
    private float angle;
    private float timer;

    protected override void Initialize()
    {
        base.Initialize();
        _rotate = Character.Components.GetComponent<Character3rdPersonRotate>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angle = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float angleChange = _rotate.HorizontalAngleChange;

        if (angleChange != 0)
        {
            angle += angleChange;
            float abs = Mathf.Abs(angle);
            timer = 0;

            if (angle > 0)
            {
                animator.CrossFadeInFixedTime(_rightState, 0, layerIndex);
            }
            else
            {
                animator.CrossFadeInFixedTime(_leftState, 0, layerIndex);
            }

            animator.SetFloat(_float, abs / MAX_VALUE);

            if (abs > MAX_VALUE)
                angle = 0f;
        }
    }
}