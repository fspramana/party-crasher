using UnityEngine;
using System.Collections;
using InControl;

public class CapsuleController : MonoBehaviour {

    public int deviceId = 0;
    public float moveSpeed = 6f;
    public float rotationSpeed = 80f;

    private Rigidbody playerRigidbody;
    private Vector3 movement;
    private Vector3 rotation;

    InputDevice controller;

    float speed;

    private Animator _animator;
    private Health _health;

    private bool isMoving;

    void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
        speed = moveSpeed;
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    void Start()
    {
        controller = InputManager.Devices[deviceId];
    }

    void FixedUpdate() {

        float leftHorizontal = controller.LeftStickX;
        float leftVertical = controller.LeftStickY;

        float rightHorizontal = controller.RightStickX;
        float rightVertical = controller.RightStickY;

        if(_health && _health.IsLiving()) Move(leftHorizontal, leftVertical);
        if (_health && _health.IsLiving()) if ( Mathf.Abs(rightHorizontal) >= 0.05f || Mathf.Abs(rightVertical)  >= 0.05f ) Rotate(rightHorizontal, rightVertical);
    }

    public void SetMovementSpeed (float slowFactorInPercent) {
        moveSpeed = speed - (speed*(slowFactorInPercent/100));
        controller = InputManager.Devices[deviceId];
    }

    public void SetupControllerDevice(int id) {
        deviceId = id;
        controller = InputManager.Devices[deviceId];
    }

    public InputDevice GetControllerDevice() {
        return controller;
    }

    void Move(float h, float v) {
        movement.Set(h, 0f, v);
        isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;
        _animator.SetBool("Walking", isMoving);

        movement = movement.normalized * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Rotate(float h, float v) {
        rotation.Set(h, 0f, v);

        Quaternion newRotation = Quaternion.LookRotation(rotation);
        Quaternion currentRotation = Quaternion.Lerp(playerRigidbody.rotation, newRotation, rotationSpeed * Time.deltaTime);

        playerRigidbody.MoveRotation(currentRotation);
    }
}
