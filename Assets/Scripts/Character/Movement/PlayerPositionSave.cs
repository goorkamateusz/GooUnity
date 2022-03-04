using System;
using UnityEngine;
using Assets.Goo.Characters;
using Goo.Saves;

[Serializable]
public class Vector3Serializable
{
    public float[] Array { get; set; }

    public Vector3Serializable(Vector3 vector)
    {
        Array = new float[] { vector.x, vector.y, vector.z };
    }

    public Vector3 GetVector()
    {
        return new Vector3(Array[0], Array[1], Array[2]);
    }

    public static implicit operator Vector3(Vector3Serializable v) => v.GetVector();
    public static implicit operator Vector3Serializable(Vector3 v) => new Vector3Serializable(v);
}

public class PlayerPositionSave : CharacterComponent
{
    public class PlayerPositionSerializable : AttributedSaveSerializable
    {
        public Vector3Serializable Position;
        public Vector3Serializable Rotation;

        public override string SubKey => "position";

        public PlayerPositionSerializable(string parentKey) : base(parentKey) { }
    }

    private PlayerPositionSerializable _data;

    protected override void OnStart()
    {
        _data = new PlayerPositionSerializable(Character.Id);
        PreSave();
    }

    protected override void AfterGameLoaded()
    {
        SaveManager.Instance.PreSave += PreSave;
        SaveManager.Instance.Load(ref _data);
        Character.Movement.Wrap(_data.Position, Quaternion.Euler(_data.Rotation));
    }

    private void PreSave()
    {
        _data.Position = Character.Position;
        _data.Rotation = Character.Rotation.eulerAngles;
    }
}