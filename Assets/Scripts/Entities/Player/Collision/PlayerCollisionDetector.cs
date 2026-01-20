using UnityEngine;
using System;
public class PlayerCollisionDetector : MonoBehaviour
{
    public event Action<bool> OnGroundedChanged;
    private void Start()
    {
        OnGroundedChanged?.Invoke(true);
    }
}
