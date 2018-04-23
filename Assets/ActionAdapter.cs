using UnityEngine;
using System.Collections;

public class ActionAdapter {

	public Action action;

	public ActionAdapter(Action action){
		this.action = action;
	}

	public void update(){
		if (action.type == 0) {
			action.Update ();
		} else {
			action.FixedUpdate ();
		}
	}
}
