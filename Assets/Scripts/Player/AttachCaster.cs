using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		//若即将被击中则闪避
		if (other.tag == "BlueBullet")
		{
			Debug.Log("trigger bullet");
			Ray2D ray = new Ray2D();
			ray.direction = Quaternion.AngleAxis(20, Vector3.forward) * other.transform.right * -1;
			ray.origin = other.transform.position;

			//射线检测信息数组
			RaycastHit2D[] hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

			bool isPlayerInRay = false;   //是否检测到玩家
			foreach (RaycastHit2D hit in hitArray)
			{
				if (hit.collider.tag == "RedPlayer")
					isPlayerInRay = true;
			}

			if (isPlayerInRay == true)
			{
				Debug.Log("attack");
				transform.parent.SendMessage("Attack");
			}
		}
	}

}
