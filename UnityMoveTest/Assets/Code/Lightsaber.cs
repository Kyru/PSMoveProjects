/**
 * UniMove API - A Unity plugin for the PlayStation Move motion controller
 * Copyright (C) 2012, 2013, Copenhagen Game Collective (http://www.cphgc.org)
 * 					         Patrick Jarnfelt
 * 					         Douglas Wilson (http://www.doougle.net)
 * 
 * 
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *    1. Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *    2. Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 **/

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Lightsaber : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TrailRenderer trailRenderer;
    UniMoveController move;
    bool canMove;
    bool lightsaberOn;


    void Start()
    {
        /* NOTE! We recommend that you limit the maximum frequency between frames.
		 * This is because the controllers use Update() and not FixedUpdate(),
		 * and yet need to update often enough to respond sufficiently fast.
		 * Unity advises to keep this value "between 1/10th and 1/3th of a second."
		 * However, even 100 milliseconds could seem slightly sluggish, so you
		 * might want to experiment w/ reducing this value even more.
		 * Obviously, this should only be relevant in case your framerare is starting
		 * to lag. Most of the time, Update() should be called very regularly.
		 */

        Time.maximumDeltaTime = 0.1f;

        int count = UniMoveController.GetNumConnected();

        Debug.Log("count = " + count);

        move = gameObject.AddComponent<UniMoveController>();  // It's a MonoBehaviour, so we can't just call a constructor

        // Remember to initialize!
        if (!move.Init(0))  // <F> 0 es el primer y único mando
        {
            Destroy(move);  // If it failed to initialize, destroy and continue on
        }

        // This example program only uses Bluetooth-connected controllers
        PSMoveConnectionType conn = move.ConnectionType;
        if (conn == PSMoveConnectionType.Unknown || conn == PSMoveConnectionType.USB)
        {
            Destroy(move);
        }
        else
        {
            // Start all controllers with a white LED
            move.SetLED(Color.magenta);

            transform.localRotation = move.Orientation;
            transform.localPosition = move.Position;
        }

        lightsaberOn = false;
        animator.SetBool("On", lightsaberOn);
        trailRenderer.emitting = lightsaberOn;

    }

    void Update()
    {
        if (move.GetButtonDown(PSMoveButton.Circle))
        {
            Debug.Log("Circle click");
            canMove = !canMove;
        }

        if (move.GetButtonDown(PSMoveButton.Move))
        {
            lightsaberOn = !lightsaberOn;
            animator.SetBool("On", lightsaberOn);
            trailRenderer.emitting = lightsaberOn;
            StartCoroutine("ActivateRumble");
        }
        if (canMove)
        {
            transform.localRotation = move.Orientation;
            transform.localPosition = move.Position;
        }
    }

    IEnumerator ActivateRumble(){
        move.SetRumble(1f);
        yield return new WaitForSeconds(0.6f);
        move.SetRumble(0f);
    }
}
