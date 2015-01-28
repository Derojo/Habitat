using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// Movement
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	// Health
	public float maxHealth = 100;
	public float curHealth = 100;
	private GameObject _enemyHealthHUD = null;
	// Attack
	public float attackTimer = 0.0f;
	public float coolDown = 2.0f;
	public float damage = 25f;
	
	private Transform myTransform;
	public bool follow;
	
	void Awake () 
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		if (go != null) 
		{
			target = go.transform;
		}
		maxDistance = 2;
		_enemyHealthHUD = Instantiate(Resources.Load("EnemyHealth")) as GameObject;
		_enemyHealthHUD.GetComponent<HoverFollow> ().target = this.transform;

	}

	void Update() {
		if (follow) 
		{
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			if (Vector3.Distance (target.position, myTransform.position) > maxDistance) 
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
		
		if (attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
		}
		
		if (attackTimer < 0) 
		{
			attackTimer = 0;
		}
		
		if(attackTimer == 0)
		{
			Attack();
			attackTimer = coolDown;
		}

		// Health overlay
		_enemyHealthHUD.GetComponent<GUIText>().text = ""+curHealth;
		
		
	}
	
	void Attack()
	{
		//calculating distance between target en player
		float distance = Vector3.Distance (target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		
		
		
		//getting health script
		PlayerController health = target.GetComponent<PlayerController>();
		if(health)
		{
			if(distance <= 2.5f && direction > 0)
			{
				health.ReceiveDamage(-10);
			}
		}
		
	}

	void OnTriggerEnter (Collider other) 
	{
		follow = true;

	}

	void OnTriggerExit (Collider other)
	{
		 follow = false;
	}

	public void ReceiveDamage ( float amt)
	{
		curHealth += amt;
		
		if (curHealth < 0)
		{
			curHealth = 0;
		}
		
		
		if (curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
		
		if (maxHealth < 1) {
			maxHealth = 1;
		}
		
		
	}

}
