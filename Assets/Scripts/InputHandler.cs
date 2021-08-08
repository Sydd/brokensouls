using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public GameObject player;
    public WarpSystem warpSystem;

    CharacterMovement playerMovement;


    public float timeToDetectJump = 1f;

    public float timeToDetectShoot = 0.2f;
    private float elapsedTimeJumpingDetection, elapsedTimeShootingDetection;


    void Start()
    {
        playerMovement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.activeSelf)
        {
            if (!warpSystem.changinWorld)
            {

                playerMovement.Move(Input.GetAxis("Horizontal"));

                if (elapsedTimeJumpingDetection > timeToDetectJump && Input.GetKey(KeyCode.Space))
                {

                    playerMovement.Jump();

                    elapsedTimeJumpingDetection = 0;

                }
                else
                {
                    elapsedTimeJumpingDetection += Time.deltaTime;
                }
            }
            else
            {
                playerMovement.Move(0);
            }
            if ((elapsedTimeShootingDetection > timeToDetectShoot) && (Input.GetKeyDown(KeyCode.Q)))
            {
                warpSystem.Warp();

                elapsedTimeShootingDetection = 0;

            }
            else
            {

                elapsedTimeShootingDetection += Time.deltaTime;

            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

}
