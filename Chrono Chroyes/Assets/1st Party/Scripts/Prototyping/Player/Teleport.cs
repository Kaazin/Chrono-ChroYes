 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	ParticleSystem warpFX;

	public bool isTeleporting;

	public float warpDelay;

	public float warpMultiplier;
	AudioSource[] sounds;

	SkinnedMeshRenderer[] character;
	float timer;
	public float time;

	PlayerController PC;
	void Awake () 
	{
		warpFX = GameObject.Find ("WarpFX").GetComponent<ParticleSystem>();
		warpFX.Stop ();
		sounds = GetComponentsInChildren<AudioSource> ();
		character = GetComponentsInChildren<SkinnedMeshRenderer> ();
		PC = GetComponent<PlayerController> ();
	}

	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			timer += Time.deltaTime;
			Teleportion ();
		}
	}

	void Teleportion()
	{
		warpFX.Stop ();
		warpFX.Play ();

		StartCoroutine(Disappear(time));



	}
	IEnumerator Disappear(float time)
	{
		yield return new WaitForSeconds (time);
		sounds [0].PlayOneShot (sounds [0].clip);

		foreach (SkinnedMeshRenderer c in character) 
		{
			c.enabled = false;
		}
		PC.speed *= warpMultiplier;
		StartCoroutine (Appear ());
	}
	IEnumerator Appear()
	{
		yield return new WaitForSeconds (warpDelay);

		warpFX.Stop ();
		warpFX.Play ();
		sounds [0].PlayOneShot (sounds [0].clip);

		yield return new WaitForSeconds (warpDelay);

		foreach (SkinnedMeshRenderer c in character) 
		{
			c.enabled = true;
		}

		PC.speed /= warpMultiplier;

	}


		
}
