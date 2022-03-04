using System;
using UnityEngine;

namespace Assets.Goo.EditorTools
{
    public class LabledListAttribute : PropertyAttribute
    {
        public readonly Type Type;

        public LabledListAttribute(Type type) { Type = type; }
    }
}