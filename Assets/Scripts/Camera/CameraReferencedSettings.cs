using UnityEngine;

public interface ICameraSettings
{
    Vector3 Offset { get; }
    Quaternion Rotation { get; }
    float Smooth { get; }
    bool FollowPlayerRotation { get; }
    float FieldOfView { get; }

    void ConfigCamera(Camera camera);
}

[RequireComponent(typeof(Camera))]
public class CameraReferencedSettings : MonoBehaviour, ICameraSettings
{
    [SerializeField] private float _smooth = 0.1f;
    [SerializeField] private bool _followPlayerRotation = false;
    [SerializeField] private Camera _camera;

    public Vector3 Offset => transform.localPosition;
    public Quaternion Rotation => transform.localRotation;
    public float Smooth => _smooth;
    public bool FollowPlayerRotation => _followPlayerRotation;
    public float FieldOfView => _camera.fieldOfView;

    public void ConfigCamera(Camera camera)
    {
        camera.transform.localPosition = Offset;
        camera.transform.localRotation = Rotation;
        camera.fieldOfView = FieldOfView;
    }

    protected virtual void Awake()
    {
        _camera.enabled = false;
        gameObject.SetActive(false);
    }

    protected virtual void Reset()
    {
        Awake();
    }
}
