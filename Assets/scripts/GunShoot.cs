using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {

		bool shoot = Input.GetButtonDown ("Fire1");
		shoot = Input.GetButtonDown ("Fire1");
		
		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			
			if (weapon != null)
			{
				weapon.Attack(false);
			}
		}
	
	}
}
