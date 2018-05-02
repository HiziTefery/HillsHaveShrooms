using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerClass : MonoBehaviour {
	[SerializeField] private int maxSanity = 100;
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private int healthDepletion = 1;

	CharacterController charCtrl; 
	
	private int currentHealth;	
	private int currentSanity;

	HealthMushroomClass healthShroom;
	PoisonMushroomClass poisonShroom;
	EnemyClass enemy;

	public Text healthText;
	public Text sanityText;
	public Light flashlight;

	private RaycastHit hit;
	private string tagCheck = "enemy";
	private float timestamp = 0.0f; 
	private bool raycasting = false;
	private bool attacking = false;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentSanity = maxSanity;
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		if (sceneName == "Forest")
		{
			StartCoroutine(DecreaseHealth());
			sanityText.text = "Sanity: " + maxSanity.ToString();
			
		}
		healthText.text = "Health: " + maxHealth.ToString();
		charCtrl = GetComponent<CharacterController>();
	}

	IEnumerator DecreaseHealth(){
		while(true){
			currentHealth  -= healthDepletion;
			Debug.Log("Health" + currentHealth.ToString());
			healthText.text = "Health: " + currentHealth.ToString();						
			yield return new WaitForSeconds(1); 
		}
	}


	private void OnTriggerEnter(Collider other) {

		//  COLLECTING BATTERY
		if(other.gameObject.tag == "batery"){
			// other.gameObject.GetType
			Destroy(other.gameObject);			
		}
		
		// COLLECTING HEALTH MUSHROOM
		else if(other.gameObject.tag == "healthMushroom"){
			Debug.Log("Health:" + currentHealth.ToString());
			healthShroom = other.gameObject.GetComponent<HealthMushroomClass>();
			if (currentHealth < maxHealth - healthShroom.GetHealth())
			{
				currentHealth += healthShroom.GetHealth();
			}
			else{currentHealth = maxHealth;}
			healthText.text = "Health: " + currentHealth.ToString();
		
			Destroy(other.gameObject);
			Debug.Log("Health:" + currentHealth.ToString());
		}

		// COLLECTING POISON MUSHROOM
		else if(other.gameObject.tag == "poisonMushroom"){
			Debug.Log("Sanity:" + currentSanity.ToString());
			poisonShroom = other.gameObject.GetComponent<PoisonMushroomClass>();
			currentSanity -= poisonShroom.GetPoison();
			if (currentSanity <= 0)
			{
				SceneManager.LoadScene(1);
			}
			sanityText.text = "Sanity: " + currentSanity.ToString();			
			Destroy(other.gameObject);
			Debug.Log("Sanity:" + currentSanity.ToString());
		}

		// ENEMY CLLISION
		else if(other.gameObject.tag == "enemy"){
			Debug.Log("Damage dealt to player");
			enemy = other.gameObject.GetComponent<EnemyClass>();
			currentHealth -= enemy.GetDamege();
			healthText.text = "Health: " + currentHealth.ToString();
			Destroy(other.gameObject);
		}
	}	

	// Update is called once per frame
	void Update () {
		// currentHealth -= healthDepletion *  Mathf.FloorToInt(Time.deltaTime);
		// currentHealth = currentHealth - healthDepletion * Mathf.RoundToInt(Time.deltaTime);
		if(currentHealth <= 0){
			// DIE - load ui element or animation of player dying
		}

		if(Input.GetKeyDown(KeyCode.F)){
			if (!attacking)
			{	
				 StartCoroutine(FadeInOrOut(true));
			}
			else{
				StartCoroutine(FadeInOrOut(false));
			}
		}
	}

	IEnumerator FadeInOrOut(bool fadeIn){
		attacking = !attacking;
		print(attacking.ToString());
		float duration = 0.2f;//time you want it to run
		float interval = 0.01f;//interval time between iterations of while loop
		while (duration >= 0.0f) {
			flashlight.intensity += fadeIn ? 1.5f : -1.5f; 
			duration -= interval;
			yield return new WaitForSeconds(interval);//the coroutine will wait for 0.01 secs
		}
	}	

	void FixedUpdate() {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 pos = transform.position + charCtrl.center;
		if (Physics.SphereCast(pos, charCtrl.height, transform.forward, out hit,20) && hit.transform.tag == tagCheck && attacking)
		{
			if (!raycasting){
				raycasting = true;
				timestamp = Time.time + 0.5f;
			}
			
			print(hit.distance.ToString());
			print("Atttacking:" + attacking.ToString());
		}
		else{
				raycasting = false;
		}
		if (raycasting && Time.time >= timestamp) {
			Destroy(hit.transform.gameObject);
			Debug.Log("Dead");
		}
	}
}
