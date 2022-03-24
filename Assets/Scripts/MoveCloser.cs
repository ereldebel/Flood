using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloser : MonoBehaviour
{
	void Start()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			var pos = transform.GetChild(i).position;
			var y = pos.y - 8;
			pos.y = 0;
			transform.GetChild(i).position = pos.normalized * 50 + Vector3.up * y;
		}
	}
}