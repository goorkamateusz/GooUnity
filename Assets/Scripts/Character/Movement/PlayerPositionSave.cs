using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Vector3Serializable
{
    public float[] Array { get; set; }

    public Vector3Serializable(Vector3 vector)
    {
        Array = new float[]{vector.x, vector.y, vector.z};
    }

    public Vector3 GetVector()
    {
        return new Vector3(Array[0], Array[1], Array[2]);
    }

    public static implicit operator Vector3(Vector3Serializable v) => v.GetVector();
    public static implicit operator Vector3Serializable(Vector3 v) => new Vector3Serializable(v);
}

public class PlayerPositionSave : Ability
{
    public class PlayerPositionSerializable : CharacterSaveSerializable<PlayerPositionSerializable>
    {
        public Vector3Serializable Position;
        public Vector3Serializable Rotation;

        public override string LatestVersion => "0.0.0";
        public override string SubKey => "position";

        public PlayerPositionSerializable(string parentKey) : base(parentKey) { }
    }

    private PlayerPositionSerializable _data;

    protected IEnumerator Start()
    {
        _data = new PlayerPositionSerializable("player1");
        yield return SaveManager.Wait();
        OnSave(_data);
        SaveManager.Instance.Load(ref _data);
        _data.PreSave = OnSave;
        Character.Movement.Wrap(_data.Position, Quaternion.Euler(_data.Rotation));
    }

    private void OnSave(PlayerPositionSerializable data)
    {
        data.Position = Character.Position;
        data.Rotation = Character.Rotation.eulerAngles;
    }
}