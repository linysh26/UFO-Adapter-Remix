using UnityEngine;
using System.Collections;

public class ActionGroup : ActionManager, ActionCallBack1{

	public FirstController scene_controller;
	// Use this for initialization
	void Start () {
		scene_controller = (FirstController)Director.getInstance ().currentSceneController;
	}
	
	// Update is called once per frame
	protected new void Update(){
		if (Input.GetMouseButtonDown (0) && Time.time > Player.Instance.next_fire && scene_controller.status ) {
			Bullet bullet = Player.Instance.shoot ();
			ShootingAction sa = ShootingAction.GetAction (bullet.speed);
			sa.type = 1;
			RunAction(bullet.getBullet(), sa, this);
			Player.Instance.next_fire += Player.Instance.fire_rate * Time.deltaTime;
		}
		if(Time.time > ClayPigeonFirer.Instance.next_fire && !scene_controller.isGameOver){
			ClayPigeonFirer.Instance.transform.LookAt (new Vector3(7, Random.Range(1, 10), Random.Range(6, 15)));
			ClayPigeon clayPigeon = ClayPigeonFirer.Instance.fireClayPigeon (RoundController.Instance);
			ShootingAction sa = ShootingAction.GetAction (clayPigeon.speed);
			sa.type = 1;
			RunAction (clayPigeon.getClayPigeon (), sa, this);
			ClayPigeonFirer.Instance.next_fire += ClayPigeonFirer.Instance.fire_rate * Time.deltaTime;
		}
		base.Update ();
	}

	public void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed, int intParam = 0, string strParam = null, Object objectParam = null){
		
	}
}
