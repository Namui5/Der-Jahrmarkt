using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Watergun watergun;
	
	public virtual void Init(Watergun watergun)
	{
		this.watergun = watergun;
	}
	
	public virtual void Launch()
	{
	
	}
}
