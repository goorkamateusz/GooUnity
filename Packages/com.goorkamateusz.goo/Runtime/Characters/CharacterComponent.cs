using System.Collections;
using UnityEngine;

namespace Goo.Characters
{
    public class CharacterComponent<T> : MonoBehaviour, ICharacterComponent where T : Character
    {
        protected T Character { get; private set; }

        public void InjectCharacter(Character character)
        {
            Character = character as T;
        }

        protected virtual IEnumerator Start()
        {
            OnStart();
            enabled = false;
            AfterGameLoaded();
            enabled = true;
            yield break;
        }

        protected virtual void OnStart() { }
        protected virtual void AfterGameLoaded() { }
    }

    public class CharacterComponent : CharacterComponent<Character>
    {
    }

    public class PlayerComponent : CharacterComponent<Player>
    {
    }
}