using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour 
{
	Transform player;

	NavMeshAgent agent;

	PlayerController pc;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		agent = GetComponent<NavMeshAgent> ();


		pc = player.gameObject.GetComponent<PlayerController> ();
		pc.enemies.Add (this);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!pc.stop)
		agent.SetDestination (player.position);
	}
}
