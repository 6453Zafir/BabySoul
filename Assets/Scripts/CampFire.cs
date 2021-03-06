﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    enum State
    {
        Activate,
        Unactivate
    }

    private SpriteRenderer _spriteRenderer
    {
        get
        {
            return GetComponentInChildren<SpriteRenderer>();
        }
    }

    public Animator Anim;
    // Use this for initialization
    void Start ()
	{
       // Unactivate();
       // _fireState = State.Unactivate;

    }

    public void Blink()
    {
        _spriteRenderer.color = _spriteRenderer.color.a == 0.0f
            ? new Color(1, 1, 1, 1)
            : new Color(1, 1, 1, 0);
    }

    public void Light()
    {
        _spriteRenderer.color =
            new Color(1, 1, 1, 1);

    }
    public void Activate()
    {
        _spriteRenderer.color = new Color(1,1,1,1);
        Anim.SetBool("Activate", true);
        _fireState = State.Activate;;
    }

    public void Unactivate()
    {
        //_spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        Anim.SetBool("Activate", false);

        _fireState = State.Unactivate;

    }
    private State _fireState = State.Unactivate;
	// Update is called once per frame
	void Update () {
		
	}
}
