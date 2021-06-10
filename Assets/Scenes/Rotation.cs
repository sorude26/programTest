using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] Vector3 _rotationAngle;
    [SerializeField] float _rotationSpeed = 1f;
    
    void Update()
    {
        transform.Rotate(_rotationAngle * _rotationSpeed);
    }
}
