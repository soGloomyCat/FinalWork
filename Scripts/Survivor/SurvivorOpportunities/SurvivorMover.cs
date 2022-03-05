using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorMover : MonoBehaviour
{
    private Vector2 _mouseCoordinates;
    private float _rotateAngle;

    public float RotateAngle => _rotateAngle;

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            _mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _rotateAngle = Mathf.Atan2(_mouseCoordinates.x, _mouseCoordinates.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -Mathf.Clamp(_rotateAngle, -60, 60));
        }
    }
}
