using UnityEngine;
using System.Collections;

public class ShootingAction : Action {

	public Vector3 velocity;
	public float speed;
	// Use this for initialization
	public static ShootingAction GetAction(float speed){
		ShootingAction sa = ScriptableObject.CreateInstance<ShootingAction> ();
		sa.speed = speed;
		return sa;
	}
	public override void Start () {
		enable = true;
		destroy = false;
	}

	public override void FixedUpdate(){
		if(enable)
			gameobject.GetComponent<Rigidbody> ().velocity = gameobject.transform.TransformDirection (Vector3.forward * speed);
		if(gameobject.GetComponent<CollisionController> ().flag)
			destroy = true;
	}
}
