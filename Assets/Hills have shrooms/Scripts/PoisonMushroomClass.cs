using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushroomClass : MonoBehaviour {
	[SerializeField] private int poison =  10;
	// Use this for initialization
	public int GetPoison(){
		return poison;
	}
}
