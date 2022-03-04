using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterComponentsManager
{
    [SerializeField] private GameObject[] _compontentNodes;

    public void InitializeComponents(Character character)
    {
        foreach (var node in _compontentNodes)
        {
            var abilities = node.GetComponents<ICharacterComponent>();
            foreach (var ability in abilities)
            {
                ability.InjectCharacter(character);
            }
        }
    }

    internal void FindComponents(Transform transform)
    {
        var nodes = new List<GameObject>();
        AgragateChildrenWithComponent<ICharacterComponent>(transform, ref nodes);
        _compontentNodes = nodes.ToArray();
    }

    private void AgragateChildrenWithComponent<T>(Transform parent, ref List<GameObject> nodes)
    {
        if (parent.TryGetComponent<T>(out _))
            nodes.Add(parent.gameObject);

        for (int i = 0; i < parent.childCount; ++i)
            AgragateChildrenWithComponent<T>(parent.GetChild(i), ref nodes);
    }
}
