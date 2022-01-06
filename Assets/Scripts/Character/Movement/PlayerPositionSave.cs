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
    public class PlayerPositionSerializable : SaveListenerSerializable<PlayerPositionSerializable>
    {
        public Vector3Serializable Position;
        public Vector3Serializable Rotation;

        public override string Key => "PlayerPosition";
    }

    private PlayerPositionSerializable _data = new PlayerPositionSerializable();

    protected IEnumerator Start()
    {
        Debug.Log(_data);

        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _data);
        _data.PreSave = OnSave;
        if (_data.Position != null)
            Character.Movement.Wrap(_data.Position, Quaternion.Euler(_data.Rotation));

        Debug.Log(_data);
    }

    private void OnSave(PlayerPositionSerializable data)
    {
        data.Position = Character.Position;
        data.Rotation = Character.Rotation.eulerAngles;
    }
}