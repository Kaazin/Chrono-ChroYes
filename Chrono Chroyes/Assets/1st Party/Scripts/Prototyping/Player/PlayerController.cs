using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	public float speed = 25f; //the speed of the rigidbody
	public Vector3 offset;		//the camera offset
	public Vector3 climbOffset;

	bool slow = false;		//is time slowed down?
	public bool stop;		//is time halted?
	Rigidbody rb;			//the rigidbody on the player
	public Vector3 dir;		//the direction of the player
	Animator anim;			//the animator attached to the player
	public bool climbing;	//is the player walking up a vertical surface?

	public float timeSpeed = 1;	//the speed of time for the player
	public Quaternion prevRot;
	public float flightSpeed;
	public List <EnemyAI> enemies;
	public float maxFlightSpeed = 10;
	void Awake()
	{
		//assign the references
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		enemies = new List<EnemyAI> ();

	}

	void Update()
	{
		
		//if the player is not climbing
		if(!climbing)	//put the camera at the normal offset
			Camera.main.transform.position = this.transform.position + offset;
		//otherwise
		else 	//put the camera at the climbing offset
			Camera.main.transform.position = this.transform.position + climbOffset;

		//arrow key input axis variables
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		//assign the direction vector  and multiply it by fixed deltatime * speed * timespeed/ timescale
		dir = new Vector3(h, dir.y, v)
			* Time.fixedDeltaTime
			* speed
			* timeSpeed / Time.timeScale;
		
		rb.velocity = transform.rotation * dir;

		//if the space bar is pressed and we are not in stopped time
		/*if (Input.GetKeyDown(KeyCode.Space) && !stop)
		{
			//make slow the opposite of slow and slow the time down
			slow = !slow;
			Time.timeScale = slow ? .1f : 1f;
		}*/

		//if we qre in stopped time and time is not being slowed down

		if (Input.GetKeyDown(KeyCode.Tab) && !slow)
		{
			//make stop the opposite and stop time
			stop = !stop;
			anim.speed = 0;

		}


		//if stop is false and slow is true
		/*if (slow && !stop) 
		{
			//change the animation and timespeed of the player so he moves like normal
			anim.speed = 10;
			timeSpeed = 1f;
		}*/
		//otherwise if time is stopped and not slowed down

		//else if neitehr stop nor slow are true
		if(!slow && !stop)
		{
			//make time function as normal
			anim.speed = 1;
			timeSpeed = 1f;

		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			Ascend ();
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			Descend ();
		}
	}

	/*void OnTriggerEnter(Collider col)
	{
		if (col.tag == "CanClimb") 
		{
			prevRot = transform.rotation;
		}
	}*/
	void OnTriggerStay(Collider col)
	{
		if (col.tag == "CanClimb" && Input.GetKeyDown(KeyCode.X) && !climbing) 
		{
			climbing = true;
			rb.useGravity = false;
			prevRot = transform.rotation;
			transform.rotation = col.transform.rotation;

		}

		else if (col.tag == "CanClimb" && Input.GetKeyDown(KeyCode.X) && climbing) 
		{
			climbing = false;
			rb.useGravity = true;
			transform.rotation = prevRot;

		}

			

	}	void OnTriggerExit(Collider col)
	{

	}

	void Fly()
	{
		rb.useGravity = false;
	}

	void Ascend()
	{
		if(dir.y < maxFlightSpeed)
		dir.y += flightSpeed * Time.deltaTime;
	}

	void Descend()
	{
		if(dir.y > maxFlightSpeed)
			
		dir.y -= flightSpeed * Time.deltaTime;
	}
}
