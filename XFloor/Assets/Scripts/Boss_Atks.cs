using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Atks : MonoBehaviour {

	public GameObject projectile;
	public GameObject breathProjectile;
    public Transform[] spawnPoints;
	public Transform breathAtk;
    private int num_SpawnPoints;
	private Animator anim;
	public bool isIdle;
	public bool isAtk;
	public bool isBreath;
	public bool isResolved;
	public int rand;
	public float counter;
	void Awake() {
		anim = GetComponent<Animator>();
	}

	void Start() {
		isIdle = true;
		isAtk = false;
		isBreath = false;
		isResolved = false;
	}

	void Update() {
		if(isIdle) {
			rand = Random.Range(1,3);
			switch(rand) {
				case 1:
					isIdle = false;
					isBreath = true;
				break;
				case 2:
					isIdle = false;
					isBreath = true;
				break;
				case 3:
					isIdle = false;
					isBreath = true;
				break;
				default:
				break;
			} 
		} else if (!isIdle && !isAtk && !isBreath) {
			if (counter <= 2) {
				counter += Time.deltaTime;
			} else {
				counter = 0;
				isIdle = true;
			}
		}
		if(isAtk) {
			counter += Time.deltaTime;
			Atk(rand);
		}
		if (isBreath) {
			counter += Time.deltaTime;
			Atk(rand);
		}
	}


	void Atk(int pattern)	{
		if(counter <= 2) {
			anim.SetBool("Breath", true);
        	if (!isResolved) {
				for (int i = 0; i < spawnPoints.Length; i++)
				{
					if (pattern % 2 == 0 && i % 2 == 0) {
						Instantiate(projectile, spawnPoints[i].position, Quaternion.identity);
					}

					if (pattern % 2 != 0 && i % 2 != 0) {
						Instantiate(projectile, spawnPoints[i].position, Quaternion.identity);
					}
					
				}
				isResolved = true;
			}
		} else {
			counter = 0;
			isResolved = false;
			anim.SetBool("Breath", false);
			isBreath = false;
		}
		
    }

	void Breath() {
		anim.SetBool("Breath", true);
		if (counter <= 3) {
			Debug.Log(isResolved);
			if(!isResolved) {
				Instantiate(breathProjectile, breathAtk.position, Quaternion.identity);
				isResolved = true;
			}
		} else {
			counter = 0;
			isResolved = false;
			isBreath = false;
			anim.SetBool("Breath", false);
		}
	}


}
