using System.Collections;
using UnityEngine;

namespace Assets.Goo.SceneObjects.OpenMechanism
{
    public abstract class OpenMechanism : MonoBehaviour
    {
        // idea execute open mechanism in editor
        protected Coroutine _coroutine;

        [ContextMenu("Close")]
        public void Close()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(CloseAnimation());
        }

        [ContextMenu("Open")]
        public void Open()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(OpenAnimation());
        }

        protected abstract IEnumerator CloseAnimation();
        protected abstract IEnumerator OpenAnimation();
    }
}