using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClayPigeonFirer : Singleton<ClayPigeonFirer>{

	List<ClayPigeon> free;
	List<ClayPigeon> used;
	public float next_fire;
	public float fire_rate;
	public bool levelUp;

	FirstController scene_controller;

	public ClayPigeonFirer(){}

	// Use this for initialization
	void Start () {
		next_fire = 0;
		fire_rate = 50;
		levelUp = true;
		free = new List<ClayPigeon> ();
		used = new List<ClayPigeon> ();
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
	}
	
	// Update is called once per frame
	void Update () {
		if (used.ToArray ().Length > 0) {
			foreach (ClayPigeon pigeon in used) {
				if (pigeon.isFree || scene_controller.isGameOver) {
					recollectClayPigeon (pigeon);
				} else
					pigeon.Update ();
			}
		}
	}

	public ClayPigeon fireClayPigeon(RoundController current_round){
		ClayPigeon clay_pigeon;
		if (free.ToArray().Length == 0) {
			clay_pigeon = new ClayPigeon ();
			GameObject horse = Instantiate (Resources.Load ("Horse")) as GameObject;
			horse.AddComponent<Rigidbody> ();
			horse.GetComponent<Rigidbody> ().useGravity = false;
			clay_pigeon.setGameObject(horse);
			this.free.Add (clay_pigeon);
		}
		int round = current_round.current_round;
		clay_pigeon = this.free[0];
		clay_pigeon.setClayPigeon (10 + round / 10, round / 10, round > 10 ? Random.Range (1, 2) : 0, this.transform);
		used.Add (clay_pigeon);
		free.Remove (clay_pigeon);
		current_round.rest--;
		return clay_pigeon;
	}

	public void recollectClayPigeon(ClayPigeon clay_pigeon){
		this.used.Remove (clay_pigeon);
		this.free.Add (clay_pigeon);
		clay_pigeon.beCollect ();
	}

	public void Restart(){
		this.next_fire = Time.time;
		this.next_fire += fire_rate * Time.deltaTime;
		this.fire_rate = 50;
	}
}
