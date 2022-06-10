using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Waterpistol : Watergun
{
	[SerializeField] private GameObject PaintballPrefab;
	
	protected override void StartShooting(XRBaseInteractor interactor)
	{
		base.StartShooting(interactor);
		Shoot();
	}
	
	protected override void Shoot()
	{
		base.Shoot();
		GameObject projectileInstance = Instantiate(PaintballPrefab, paintballSpawn.position, paintballSpawn.rotation);
	}
	
	protected override void StopShooting(XRBaseInteractor interactor)
	{
		base.StopShooting(interactor);
	}
}
