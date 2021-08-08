using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WarpSystem : MonoBehaviour
{


    // Start is called before the first frame update


    [SerializeField] private Light2D lightWorld;

    public float shadowLight;

    public float normalLight;

    public float speedLight;

    public List<GameObject> normalWorld;

    public List<GameObject> shadowWorld;

    private bool shadowWorldActive;
    
    public bool changinWorld;


    private void Start()
    {
        ActivateNormalWorld();
    }

    public void Warp()
    {
        if (!changinWorld)
        {
            if (shadowWorldActive) ActivateNormalWorld();
            else ActivateShadowWorld();
        }

    }
    // Update is called once per frame

    void ActivateShadowWorld()
    {

        StartCoroutine(TurnOffLight());

    }

    
    void ActivateNormalWorld()
    {

        StartCoroutine(TurnOnLight());


    }

    IEnumerator TurnOnLight()
    {
        changinWorld = true;
        while (lightWorld.intensity < normalLight)
        {
            lightWorld.intensity += speedLight * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        changinWorld = false;

        shadowWorld.ForEach(elemnt =>
        {
            elemnt.SetActive(false);
        });

        normalWorld.ForEach(element =>
        {
            element.SetActive(true);
        });

        shadowWorldActive = false;
    }


    IEnumerator TurnOffLight()
    {
        changinWorld = true;
        while (lightWorld.intensity > shadowLight)
        {
            lightWorld.intensity -= speedLight * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        changinWorld = false;


        normalWorld.ForEach(element =>
        {
            element.SetActive(false);
        });

        shadowWorld.ForEach(elemnt =>
        {
            elemnt.SetActive(true);
        });

        shadowWorldActive = true;


    }

}
