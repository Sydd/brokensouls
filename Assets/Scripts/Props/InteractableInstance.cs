using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableInstance : MonoBehaviour, IInteractable
{

    public System.Action OnInteract { get; set; }

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
