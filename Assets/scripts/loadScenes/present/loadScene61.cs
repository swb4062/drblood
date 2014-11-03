using UnityEngine;
using System.Collections;

public class loadScene61 : MonoBehaviour {

	void Update()
	{
		if(Input.GetKeyUp("w"))
		{
			Application.LoadLevel("scene61");
		}
	}
}