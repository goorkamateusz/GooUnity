using UnityEngine;

public class Character3rdPersonRotate : CharacterComponent
{
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private Character3rdPersonMovement _movement;
    [SerializeField] private float _characterAngelVelocity;

    protected virtual void Update()
    {
        Vector2 difference = new Vector2(1 - 2 * Input.mousePosition.x / Screen.width,
                                         1 - 2 * Input.mousePosition.y / Screen.height);

        float angle = difference.x * difference.x * difference.x * Time.deltaTime;

        transform.Rotate(Vector3.up, -angle * _characterAngelVelocity);
    }
}
