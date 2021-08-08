using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Configuration")]
     public float dampTime = 0.15f;

     private Vector3 velocity = Vector3.zero;
    public Vector3 offSet;

    public Transform target;

    public float leftLimit;

    public float rightLimit;

    public float topLimit;

    public float bottomLimit;




     Camera cam;

     void Start(){
         cam = GetComponent<Camera>();
     }
 
    
     // Update is called once per frame
     void FixedUpdate () 
     {
         if (target)

         {

             Vector3 point = cam.WorldToViewportPoint(target.position);

             Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));

             Vector3 destination = transform.position + delta + offSet;

             destination.x = Mathf.Clamp(destination.x, leftLimit, rightLimit);

             destination.y = Mathf.Clamp(destination.y, bottomLimit, topLimit);

             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

         }
     
     }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(new Vector3(leftLimit,topLimit,1), new Vector3(rightLimit,topLimit,1));
        

        Gizmos.DrawLine(new Vector3(leftLimit,topLimit,1), new Vector3(leftLimit,bottomLimit,1));
        
        Gizmos.DrawLine(new Vector3(rightLimit,topLimit,1), new Vector3(rightLimit,bottomLimit,1));
        
        Gizmos.DrawLine(new Vector3(rightLimit,bottomLimit,1), new Vector3(leftLimit,bottomLimit,1));
 
    }
 
 }
