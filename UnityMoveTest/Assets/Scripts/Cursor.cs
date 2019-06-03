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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{

    UniMoveController move;
    bool canMove;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData = new PointerEventData(null);


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
        move.CameraPosition = "Front";

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
            Vector3 newPosition = new Vector3(move.Position.x, move.Position.y, -315);
            transform.localPosition = newPosition;
        }

        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector3 newPosition = new Vector3(move.Position.x, move.Position.y, -315);
        transform.localPosition = newPosition;

        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        if (move.GetButtonDown(PSMoveButton.Move))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (results.Count > 0)
            {
                results[0].gameObject.GetComponentInParent<MainMenuButton>().goToScene();
            }
        }
    }
}
