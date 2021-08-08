using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVING : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movingx());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator movingx()
    {
        Vector3 pos = transform.position;
        while (true)
        {
            pos = transform.position;
            pos.y += Mathf.Sin( Time.time) * Time.deltaTime;
            transform.position = pos;
            yield return new WaitForEndOfFrame();
        }
    }
}
