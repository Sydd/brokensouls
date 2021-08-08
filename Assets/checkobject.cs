using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkobject : MonoBehaviour
{
    public Transform objetToLook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float elapsedTime;
    // Update is called once per frame
    void Update()
    {

        if (elapsedTime > 0.5f)
        {
            Vector3 dirFromAtoB = (objetToLook.transform.position - this.transform.position).normalized;
            float dotProd = Vector3.Dot(dirFromAtoB, this.transform.up * -1f);

            Debug.Log(dotProd);
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;

    }
}
