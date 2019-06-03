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
    [SerializeField] GameObject trailGood;
    [SerializeField] GameObject lightsaberEdge;
    [SerializeField] Material lightsaberBlue;
    [SerializeField] Material lightsaberRed;
    [SerializeField] Material lightsaberGreen;
    [SerializeField] Material lightsaberPurple;
    public AudioControl audioControl;
    private Renderer trailGoodRenderer;
    private Renderer edgeRenderer;
    UniMoveController move;
    bool canMove;
    bool lightsaberOn;
    bool gameOver;
    bool loopClipPlaying;
    bool startGame;
    public int lightsaberID;

    public UniMoveController Move
    {
        get { return move; }
        set { move = value; }
    }

    void Start()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, endGame);
    }

    public void AlternativeStart()     // called in TwoMovesTest as an alternative start method
    {
        lightsaberOn = false;
        animator.SetBool("On", lightsaberOn);
        trailRenderer.emitting = lightsaberOn;
        trailGood.SetActive(lightsaberOn);

        trailGoodRenderer = trailGood.GetComponent<Renderer>();
        edgeRenderer = lightsaberEdge.GetComponent<Renderer>();
        trailGoodRenderer.material = lightsaberBlue;
        edgeRenderer.material = lightsaberBlue;

        gameOver = false;
        loopClipPlaying = false;
        startGame = true;
    }

    void Update()
    {
        if (move == null) this.gameObject.SetActive(false);

        if (!gameOver)
        {
            if (move != null)
            {
                if (move.GetButtonDown(PSMoveButton.Move))
                {
                    audioControl.GetComponent<AudioControl>().ActivateAudioClip();

                    if (startGame)
                    {
                        Messenger.Broadcast(GameEvent.START_GAME);
                        startGame = false;
                    }

                    lightsaberOn = !lightsaberOn;
                    animator.SetBool("On", lightsaberOn);
                    //trailRenderer.emitting = lightsaberOn;
                    //trailGood.SetActive(lightsaberOn);
                    //lightsaberEdge.SetActive(lightsaberOn);
                    //Invoke("delayLightsaberEdge", 1f);

                    //StartCoroutine("ActivateRumble");
                }
                if (move.GetButtonDown(PSMoveButton.Square))
                {
                    trailGoodRenderer.material = lightsaberPurple;
                    edgeRenderer.material = lightsaberPurple;
                }
                if (move.GetButtonDown(PSMoveButton.Triangle))
                {
                    trailGoodRenderer.material = lightsaberGreen;
                    edgeRenderer.material = lightsaberGreen;
                }
                if (move.GetButtonDown(PSMoveButton.Circle))
                {
                    trailGoodRenderer.material = lightsaberRed;
                    edgeRenderer.material = lightsaberRed;
                }
                if (move.GetButtonDown(PSMoveButton.Cross))
                {
                    trailGoodRenderer.material = lightsaberBlue;
                    edgeRenderer.material = lightsaberBlue;
                }

                if (move.GetButtonDown(PSMoveButton.Select))
                {
                    move.ResetOrientation();
                }

                if (move.GetButtonDown(PSMoveButton.Start))
                {
                    canMove = !canMove;
                }

                if (lightsaberOn && !loopClipPlaying)
                {
                    audioControl.GetComponent<AudioControl>().LoopAudioClip();
                    loopClipPlaying = true;
                }

                if (!lightsaberOn && loopClipPlaying)
                {
                    audioControl.GetComponent<AudioControl>().LoopAudioClipStop();
                    loopClipPlaying = false;
                }

                if (canMove)
                {
                    transform.localRotation = move.Orientation;
                    transform.localPosition = move.Position;
                    //audioControl.GetComponent<AudioControl>().SwingAudioClip();
                }
            }
        }
    }

    void ActivateLightSaber()
    {
        if (move != null)
        {
            lightsaberEdge.SetActive(lightsaberOn);
            trailGood.SetActive(lightsaberOn);
            move.SetRumble(0f);
        }
    }

    void ActivateRumble()
    {
        if (move != null) move.SetRumble(1f);
    }

    void DeactivateLightSaber()
    {
        if (move != null)
        {
            move.SetRumble(1f);
            trailGood.SetActive(lightsaberOn);
        }
    }

    void DeactivateRumble()
    {
        if (move != null) move.SetRumble(0f);
    }

    /* <F> No se usa porque se ha pasado a la clase
        void cubeDestroyed()
        {
            move.SetRumble(1f);
            Invoke("DeactivateRumble", 0.8f);
        }
    */
    void endGame()
    {
        gameOver = true;
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, endGame);
    }
}
