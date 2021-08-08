using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    private InteractableInstance interactable;


    private void Start()
    {
        interactable = GetComponent<InteractableInstance>();
        interactable.OnInteract += AddMemory;
    }

    private void AddMemory()
    {
        interactable.OnInteract -= AddMemory;

        gameObject.SetActive(false);
    }

}
