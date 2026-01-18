using UnityEngine;
using System;

public class PlayerCollisionDetector : MonoBehaviour
{
    public event Action<bool> OnGroundedChanged;
    
    // En top-down siempre estamos "en el suelo"
    private void Start()
    {
        OnGroundedChanged?.Invoke(true);
    }
}