using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletMover))]
[RequireComponent(typeof(BulletDamageSwitcher))]
[RequireComponent(typeof(BulletSpriteSwitcher))]
public class Bullet : MonoBehaviour
{
    public void Activate(Quaternion rotation, Vector3 spawnPosition)
    {
        gameObject.SetActive(true);
        transform.rotation = rotation;
        transform.position = spawnPosition;
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
