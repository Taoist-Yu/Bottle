using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Object = UnityEngine.Object;

class BulletEmitter : Object
{
	Player player;
	private GameObject bulletInstant;


	public BulletEmitter(Player _player)
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

