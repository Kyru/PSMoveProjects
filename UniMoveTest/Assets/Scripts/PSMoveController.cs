using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSMoveController : MonoBehaviour
{

    private List<UniMoveController> moves = new List<UniMoveController>();
    [SerializeField] List<GameObject> players;

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

        // Iterate through all connections (USB and Bluetooth)
        for (int i = 0; i < count; i++)
        {
            UniMoveController move = gameObject.AddComponent<UniMoveController>();  // It's a MonoBehaviour, so we can't just call a constructor


            // Remember to initialize!
            if (!move.Init(i))
            {
                Destroy(move);  // If it failed to initialize, destroy and continue on
                continue;
            }



            // This example program only uses Bluetooth-connected controllers
            PSMoveConnectionType conn = move.ConnectionType;
            if (conn == PSMoveConnectionType.Unknown || conn == PSMoveConnectionType.USB)
            {
                Destroy(move);
            }
            else
            {
                moves.Add(move);
                move.InitOrientation();
                move.ResetOrientation();
                
                if(i == 0) move.SetLED(Color.magenta);          // <F> player 1
                else move.SetLED(Color.cyan);                   // <F> player 2

                players[i].GetComponent<Spaceship>().Move = move;
                players[i].GetComponent<Spaceship>().AlternativeStart();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
