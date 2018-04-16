using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	ParticleSystem warpParticles;

	void Awake () 
	{
		warpParticles = GetComponentInChildren<ParticleSystem> ();
	}

	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			warpParticles.Stop ();
			warpParticles.Play ();
		}
	}

	void Teleportation()
	{
		
	}
		
}
