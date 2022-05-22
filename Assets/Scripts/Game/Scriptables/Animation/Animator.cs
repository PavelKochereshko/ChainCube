using UnityEngine;

abstract class Animator<T> : ScriptableObject
{
    public abstract void Animate(T target);
}