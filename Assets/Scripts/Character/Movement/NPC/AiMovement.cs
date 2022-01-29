using System.Collections;
using Goo.Saves;
using UnityEngine;

public class AiMovement : CharacterMouseClickMovement
{
    private class AiPathSave : AttributedSaveSerializable
    {
        public int NextPoint;

        public override string SubKey => "AiPath";
        public AiPathSave(string parentKey) : base(parentKey) { }
    }

    [SerializeField] private Transform[] _path;
    [SerializeField] const float _minDistance = 2f;

    private AiPathSave _value;

    public Transform NextPoint => _path[_value.NextPoint];
    public float DistanceToNext => Vector3.Distance(Character.Position, NextPoint.position);

    protected override void HandleInput()
    {
    }

    protected override IEnumerator Start()
    {
        _value = new AiPathSave(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _value);
        SetDestination();
    }

    protected override void AfterGameLoaded()
    {
    }

    protected override void Update()
    {
        base.Update();
        Movement();
    }

    private void Movement()
    {
        if (DistanceToNext < _minDistance)
        {
            NextTarget();
            SetDestination();
        }
    }

    private void SetDestination()
    {
        _agent.SetDestination(NextPoint.position);
    }

    private void NextTarget()
    {
        _value.NextPoint = (_value.NextPoint + 1) % _path.Length;
    }
}
