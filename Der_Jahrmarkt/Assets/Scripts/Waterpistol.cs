using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Waterpistol : Watergun
{
	[SerializeField] private Projectile PaintballPrefab;
	
	protected override void StartShooting(XRBaseInteractor interactor)
	{
		base.StartShooting(interactor);
		Shoot();
	}
	
	protected override void Shoot()
	{
		base.Shoot();
		Projectile projectileInstance = Instantiate(PaintballPrefab, paintballSpawn.position, paintballSpawn.rotation);
		projectileInstance.Init(this);
		projectileInstance.Launch();
	}
	
	protected override void StopShooting(XRBaseInteractor interactor)
	{
		base.StopShooting(interactor);
	}
}
