using UnityEngine;
using System.Collections;

public class loadScene41 : MonoBehaviour {

	void Update()
	{
		if(Input.GetKeyUp("w"))
		{
			Application.LoadLevel("scene41left");
		}
	}
}