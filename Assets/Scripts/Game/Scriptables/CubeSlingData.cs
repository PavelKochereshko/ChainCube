using UnityEngine;

[CreateAssetMenu(menuName = "Game/CubeSling")]
class CubeSlingData : ScriptableObject
{
    public float PushForce => _pushForce;

    [Header("Parameters")]
    [SerializeField] private float _pushForce;
}