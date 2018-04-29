using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour {
	[SerializeField] private int damage = 15;
	// Use this for initialization
	public int GetDamege(){
		return damage;
	}
}
