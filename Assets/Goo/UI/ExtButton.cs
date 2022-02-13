using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.UI
{
    [Obsolete]
    public class ExtButton : Button
    {
        // idea
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public string text { get => _text.text; set => _text.text = value; }
        // public TMP_Text

        protected override void Reset()
        {
            base.Reset();

            _text = transform.GetComponentInChildren<TMP_Text>();
            if (_text == null)
            {
                GameObject go = transform.AddChild("ButtonText");
                _text = go.AddComponent<TMP_Text>();
            }

            _image = GetComponent<Image>();
            if (_image == null)
            {
                _text = gameObject.AddComponent<TMP_Text>();
            }

            if (name == "GameObject")
            {
                name = "Button";
            }
        }
    }
}