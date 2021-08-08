using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterMemory : MonoBehaviour
{
    [SerializeField] private Light2D light;

    [SerializeField] private float lightAmount;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Memory")) {
            collision.GetComponent<IInteractable>().Interact();
            AddLight();
        }
    }

    private void AddLight()
    {
        light.intensity += lightAmount;

        light.pointLightInnerRadius += lightAmount;

        light.pointLightOuterRadius += lightAmount;
    }
}
