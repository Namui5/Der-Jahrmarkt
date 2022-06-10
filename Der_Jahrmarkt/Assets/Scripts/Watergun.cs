using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]

public class Watergun : MonoBehaviour
{
	[SerializeField] protected float shootingForce;
	[SerializeField] protected Transform paintballSpawn;
	[SerializeField] private float recoilForce;
	[SerializeField] private float damage;
	
	private Rigidbody rigidBody;
	private XRGrabInteractable interactableWatergun;
	
	protected virtual void Awake()
	{
		interactableWatergun = GetComponent <XRGrabInteractable>();
		rigidBody = GetComponent <Rigidbody>();
		SetupInteractableWatergunEvents();
	}
	
	private void SetupInteractableWatergunEvents()
	{
		interactableWatergun.onSelectEntered.AddListener(PickUpWatergun);
		interactableWatergun.onSelectExited.AddListener(DropWatergun);
		interactableWatergun.onActivate.AddListener(StartShooting);
		interactableWatergun.onDeactivate.AddListener(StopShooting);
	}
	
	private void PickUpWatergun(XRBaseInteractor interactor)
	{
		interactor.GetComponent<MeshHider>().Hide();
	}
	
	private void DropWatergun(XRBaseInteractor interactor)
	{
		interactor.GetComponent<MeshHider>().Show();
	}
		
	protected virtual void StartShooting(XRBaseInteractor interactor)
	{
		//throw new NotImplementedException();
	}
		
	protected virtual void StopShooting(XRBaseInteractor interactor)
	{
		//throw new NotImplementedException();
	}
	
	protected virtual void Shoot()
	{
		ApplyRecoil();
	}
		
	private void ApplyRecoil()
	{
		rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
	}
	
	public float GetShootingForce()
	{
		return shootingForce;
	}
	
	public float GetDamage()
	{
		return damage;
	}
	
}
