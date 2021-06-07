using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void PressEvent();

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _upForce;
    [SerializeField] private float _forwardSpeed;

    [HideInInspector] public event PressEvent OnPress;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GameManager.Instance.Player = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FlyFroward();

#if UNITY_IOS || UNITY_ANDROID
         
        // Check if any fingers are touching the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If so check if it was a tap
            if (touch.phase == TouchPhase.Began)
                OnPress.Invoke();
        }

#else
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            OnPress.Invoke();
#endif
    }

    /// <summary>
    /// Change the velocity to be up
    /// </summary>
    private void Tap()
    {
        // I also tried using AddForce, however, that was more unpredictable
        _rb.velocity = Vector3.up * _upForce;
    }

    /// <summary>
    /// The constant forward force
    /// </summary>
    private void FlyFroward()
    {
        // get the rigidbody velocity and only change the X value
        Vector3 newVel = _rb.velocity;
        newVel.x = _forwardSpeed;

        // I did this so the player falls a bit faster when nose diving
        newVel += (transform.forward * (transform.eulerAngles.x / 100)) * _forwardSpeed * Time.deltaTime;
        _rb.velocity = newVel;
    }

    private void OnEnable()
    {
        OnPress += Tap;
    }

    private void OnDisable()
    {
        OnPress -= Tap;
    }
}
