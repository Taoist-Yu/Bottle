using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Object = UnityEngine.Object;



/*控制玩家子弹发射的类*/
class BulletEmitter : Object
{
	Player player;                                   //玩家实例
	private GameObject bulletInstant;                //子弹实例（针对小子弹）

	//构造函数传入玩家实例
	public BulletEmitter(Player _player)
	{
		this.player = _player;
	}

	//供玩家调用的子弹发射方法
	public void EmitBullet(BulletType bullet_type, GameObject bullet)
	{
		switch (bullet_type)
		{
			case BulletType.small:
				EmitSmallBullet(bullet);
				break;
			case BulletType.Thunder:
				EmitThunderBullet(bullet);
				break;
			case BulletType.bounce:
				EmitBounceBullet(bullet);
				break;
			case BulletType.fission:
				EmitFissionBullet(bullet);
				break;
		}

		if (player.residueCount > 0)
		{
			player.residueCount--;
			if (player.residueCount == 0)
				player.ResetBullet();
		}

	}

	private void EmitSmallBullet(GameObject bullet)
	{
		if (bulletInstant != null)
		{
			DestroyImmediate(bulletInstant, true);
		}

		bulletInstant = (GameObject)Instantiate(bullet,
						player.transform.position + 0.2f * player.transform.right,
						Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

	private void EmitThunderBullet(GameObject bullet)
	{
		Instantiate(bullet,
			player.transform.position + 0.2f * player.transform.right,
			Quaternion.Euler(player.transform.eulerAngles + new Vector3(0,0,20)));
	}

	private void EmitBounceBullet(GameObject bullet)
	{
		Instantiate(bullet,
			player.transform.position + 0.2f * player.transform.right,
			Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

	private void EmitFissionBullet(GameObject bullet)
	{
		Instantiate(bullet,
			player.transform.position + 0.2f * player.transform.right,
			Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

}

class AIBulletEmitter : Object
{
	AIPlayer player;
	private GameObject bulletInstant;


	public AIBulletEmitter(AIPlayer _player)
	{
		this.player = _player;
	}

	public void EmitBullet(BulletType bullet_type, GameObject bullet)
	{
		switch (bullet_type)
		{
			case BulletType.small:
				EmitSmallBullet(bullet);
				break;
			case BulletType.Thunder:
				EmitThunderBullet(bullet);
				break;
			case BulletType.bounce:
				EmitBounceBullet(bullet);
				break;
			case BulletType.fission:
				EmitFissionBullet(bullet);
				break;
		}

		if (player.residueCount > 0)
		{
			player.residueCount--;
			if (player.residueCount == 0)
				player.ResetBullet();
		}

	}

	private void EmitSmallBullet(GameObject bullet)
	{
		if (bulletInstant != null)
		{
			DestroyImmediate(bulletInstant, true);
		}

		bulletInstant = (GameObject)Instantiate(bullet,
						player.transform.position + 0.2f * player.transform.right,
						Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

	private void EmitThunderBullet(GameObject bullet)
	{
		Debug.Log("闪电功能尚未完善");
	}

	private void EmitBounceBullet(GameObject bullet)
	{
		Instantiate(bullet,
			player.transform.position + 0.2f * player.transform.right,
			Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

	private void EmitFissionBullet(GameObject bullet)
	{
		Instantiate(bullet,
			player.transform.position + 0.2f * player.transform.right,
			Quaternion.Euler(player.transform.eulerAngles + new Vector3(0, 0, 180)));
	}

}

