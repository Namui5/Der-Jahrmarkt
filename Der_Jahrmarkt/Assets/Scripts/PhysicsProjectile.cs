using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsProjectile : Projectile
{
	[SerializeField] private float lifeTime;
	private Rigidbody rigidBody;
	
	private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	public override void Init(Watergun watergun) 
    {
        base.Init(watergun);
		Destroy(gameObject, lifeTime);
    }
	
	public override void Launch()
	{
		base.Launch();
		rigidBody.AddRelativeForce(Vector3.forward * watergun.GetShootingForce(), ForceMode.Impulse);
    }
	
	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		ITakeDamage[] damageTakers = other.GetComponentsInChildren<ITakeDamage>();
		
		foreach (var taker in damageTakers)
		{
			taker.TakeDamage(watergun, this, transform.position);
		}
		string tag = other.gameObject.tag;
		
		if( tag == "10" || tag == "20" || tag == "30" )
		{
			int score = int.Parse(tag);
			other.gameObject.tag = "shot";
			ScoreManager_Schiessstand.instance.AddPoint(score);
		}
		
    }
		
}
