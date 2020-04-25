﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5)] private float speed = 1;
    [SerializeField] [Range(0, 2)] private float rotationDuration = 0.3f;

    [SerializeField] [Range(1, 4)] int controlSharpness = 1;

    [SerializeField] private Vector2 velocity;

    private Rigidbody2D _rigidbody2D;
    private TweenerCore<Quaternion, Quaternion, NoOptions> _rotationTweener;

    void Awake()
    {
        Hub.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var movementDirection = Vector2.ClampMagnitude(new Vector2(
            Mathf.Abs(Mathf.Pow(x, controlSharpness)) * Mathf.Sign(x),
            Mathf.Abs(Mathf.Pow(y, controlSharpness)) * Mathf.Sign(y)), 1);
        _rigidbody2D.velocity = movementDirection * speed;

        if (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg - 90f;
            var rot = Quaternion.AngleAxis(angle, Vector3.forward);

            //transform.rotation = rot;
            // if (_rotationTweener == null)
            // {
            _rotationTweener = transform.DORotateQuaternion(rot, rotationDuration);
            // }
            // else
            // {
            //     _rotationTweener = _rotationTweener.ChangeEndValue(rot, rotationDuration);
            // }
        }
    }
}