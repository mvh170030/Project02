using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform target;
    public int _enemyHealth = 100;
    private float timer = 5f;
    RaycastHit hit;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] ParticleSystem _particleShot;

    public GameObject deadMenu;


    public void TakeDamage(int _damageToTake)
    {
        _enemyHealth -= _damageToTake;
        Debug.Log("Health: " + _enemyHealth);

        if (_enemyHealth == 0)
        {
            deadMenu.SetActive(true);
            Debug.Log("Game over! You won!");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Start()
    {
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            _particleShot.Play();
            EnemyShoot();
        }

    }

    public void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
            
        }

    }

    public void EnemyShoot()
    {
        Vector3 rayDirection = transform.forward;
        // cast ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 1f);
        // do raycast
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit), shootDistance);
        if (Physics.Raycast(rayOrigin.position, rayDirection, out hit, shootDistance))
        {
            PlayerController playerCont = hit.transform.gameObject.GetComponent<PlayerController>();
            if (playerCont != null)
            {
                Debug.Log("Player hit!");
                // enemyShooter.TakeDamage(weaponDamage);
                GameObject getPlayer = GameObject.Find("FPSCharacter");
                PlayerController playerControl = getPlayer.GetComponent<PlayerController>();
                playerControl._currentHealth -= 20;
                playerControl._healthBar.value = playerControl._currentHealth;
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}
