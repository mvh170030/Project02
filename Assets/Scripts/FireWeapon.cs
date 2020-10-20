using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] int weaponDamage = 20;

    [SerializeField] ParticleSystem _particleShot;
    RaycastHit objectHit; // store info about raycast hit

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            _particleShot.Play();
        }
    }

    // fire weapon using raycast
    void Shoot()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        // cast ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);
        // do raycast
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
                if (enemyShooter != null)
            {
                Debug.Log("Hit enemy!");
                enemyShooter.TakeDamage(weaponDamage);
            }
        } else
        {
            Debug.Log("Missed!");
        }
    }
}
