using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMushroomClass : MonoBehaviour {
	[SerializeField] private int health = 15;
	// Use this for initialization
	public int GetHealth(){
		return health;
	}
	
}