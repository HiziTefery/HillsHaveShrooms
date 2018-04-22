using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		agent.SetDestination(target.position);	
	}
}
