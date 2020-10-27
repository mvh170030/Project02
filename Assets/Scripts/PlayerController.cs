using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    public Slider _healthBar;
    public float _health = 100;
    public float _currentHealth;

    public GameObject deadMenu;
    public GameObject crosshair;

    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
        _currentHealth = _health;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Took 20 damage!");
            _currentHealth -= 20;
            _healthBar.value = _currentHealth;
        }
        if (other.gameObject.CompareTag("HealthUp") && _currentHealth < 100)
        {
            Debug.Log("Gained 20 health!");
            _currentHealth += 20;
            _healthBar.value = _currentHealth;
            Destroy(GameObject.FindWithTag("HealthUp"));
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeed * 2;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeed / 2;
        }

        // all died
        if (_currentHealth == 0)
        {
            deadMenu.SetActive(true);
            Debug.Log("Game over! You died");
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
    }


    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }
}
