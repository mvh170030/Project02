using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooter : MonoBehaviour
{
    public Slider _enemyHealthBar;
    public Transform target;
    public float _enemyHealth = 100;
    public float _enemyCurrentHealth;
    private float timer = 5f;
    public GameObject crosshair;

    public AudioClip enemyShot;
    AudioSource audioSource;

    RaycastHit hit;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] ParticleSystem _particleShot;

    public GameObject deadMenu;


    public void TakeDamage(int _damageToTake)
    {
        _enemyCurrentHealth -= _damageToTake;
        _enemyHealthBar.value = _enemyCurrentHealth;
        Debug.Log("Health: " + _enemyHealth);

        if (_enemyHealthBar.value == 0)
        {
            crosshair.SetActive(false);
            deadMenu.SetActive(true);
            Debug.Log("Game over! You won!");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Start()
    {
        _enemyCurrentHealth = _enemyHealth;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            //_particleShot.Play();
            EnemyShoot();
            audioSource.PlayOneShot(enemyShot, 1f);
        }

    }

    public void Update()
    {
        if (target != null)
        {
            // transform.LookAt(target);
            Vector3 targetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            this.transform.LookAt(targetPosition);
            
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
