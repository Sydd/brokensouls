using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    private void Update()
    {
        if (player.activeSelf)
        {

            // transform.Translate(new Vector3(player.transform.position.x, transform.position.y) * Time.deltaTime);

           // Vector3 amount = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
            transform.position = new Vector3(transform.position.x, player.transform.position.y);
        }
    }
}
