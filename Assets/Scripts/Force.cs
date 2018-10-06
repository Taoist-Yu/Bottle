using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Force
{
	public float lineForce;
	public float angleForce;
	public Vector3 DirVec;

	public Force(float lineForce, float angleForce, Vector3 DirVec)
	{
		this.lineForce = lineForce;
		this.angleForce = angleForce;
		this.DirVec = DirVec;
	}
}

