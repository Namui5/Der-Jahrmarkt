using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRSimpleInteractable))]

public class ResetScene : MonoBehaviour
{	
	private Rigidbody rigidBody;
	private XRSimpleInteractable interactableButton;
	

	
	protected virtual void Awake()
	{
		interactableButton = GetComponent <XRSimpleInteractable>();
		rigidBody = GetComponent <Rigidbody>();
		interactableButton.onSelectEntered.AddListener(PickUpButton);
	}
	
	
	private void PickUpButton(XRBaseInteractor interactor)
	{
		 Application.LoadLevel(Application.loadedLevel);
	}
	
}
