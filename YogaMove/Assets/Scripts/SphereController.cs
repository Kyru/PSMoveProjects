using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    UniMoveController move;
    float positionZ;
    bool alreadyStarted;
    bool canMove;

    public UniMoveController Move
    {
        get { return move; }
        set { move = value; }
    }

    public void AlternativeStart()
    {
        positionZ = transform.position.z;
        canMove = false;
        alreadyStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move.GetButtonDown(PSMoveButton.Start))
        {
            canMove = true;
            if (canMove)
            {
                if (!alreadyStarted)
                {
                    Messenger.Broadcast(GameEvent.START_GAME);
                    alreadyStarted = true;
                }
            }
        }
        if (canMove)
        {
            Vector3 newPosition = new Vector3(move.Position.x / 20, move.Position.y / 10, positionZ);
            transform.localPosition = newPosition;
        }
    }
}
