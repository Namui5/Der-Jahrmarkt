using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsDamage : MonoBehaviour, ITakeDamage
{
	private Rigidbody rigidBody;
	
	private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

	public void TakeDamage(Watergun watergun, Projectile projectile, Vector3 contactPoint)
    {
        rigidBody.AddForce(projectile.transform.forward * watergun.GetShootingForce(), ForceMode.Impulse);
    
    }
}
