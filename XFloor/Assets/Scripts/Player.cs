using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float m_moveSpeed = 1.0f;
	public float m_jumpForce = 10.0f;
	public GameObject projectile;
	public GameObject groundCheck;
	public GameObject frontCheck;
	public Transform SpawnPoint;
	public bool isAtk;

	public bool isDig;
	public bool m_isDead = false;
	public bool is_walking;


	private bool m_onGround = true;
	private bool m_stoppedJumping = true;
	private bool facingRight = true;
	private float m_origJumpForce;
	private Rigidbody m_rb;
	private Animator m_anim;
	public float time_Atk;
	public float time_Dig;

	private int max_health;
	public int cur_health;

	

	void Awake() {
		m_origJumpForce = m_jumpForce;
		m_rb = gameObject.GetComponent<Rigidbody>();
		m_anim = gameObject.GetComponent<Animator>();
		m_isDead = false;
		isAtk = false;
		isDig = false;
		is_walking = false;
		groundCheck.SetActive(false);
		frontCheck.SetActive(false);
		SetMaxhealth(100);
	}
	void Start () {
		time_Atk = 2;
		time_Dig = 0;
		cur_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
		if(cur_health <= 0) {
			m_isDead = true;
		} 
		is_walking = false;
		Vector3 pos = gameObject.transform.position;
		// Player Move Left and Right
		if(Input.GetAxis("Horizontal") != 0 && !isAtk && !isDig) {
			is_walking = true;
			pos.x += Input.GetAxis("Horizontal") * m_moveSpeed * Time.deltaTime;
			gameObject.transform.position = pos;
			if(Input.GetAxis("Horizontal") > 0 && !facingRight && !isAtk) {
				Flip();
			} else if (Input.GetAxis("Horizontal") < 0 && facingRight && !isAtk){
				Flip();
			}
			m_anim.SetBool("Walk", true);
			//SetFloat("Speed", Mathf.Abs(pos.x));
		} else {
			m_anim.SetBool("Walk", false);
			gameObject.transform.position = pos;
		}

		Debug.Log(cur_health);

		if(Input.GetButtonDown("Jump") && m_onGround) {
			m_onGround = false;
			m_stoppedJumping = false;
			m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
		}

		if(Input.GetButtonUp("Jump") && !m_stoppedJumping) {
			if(m_rb.velocity.y > 0) {
				Vector3 velocity = m_rb.velocity;
				velocity.y = 0;
				m_rb.velocity = velocity;
			}
			m_stoppedJumping = true;
			m_jumpForce = m_origJumpForce;
		}

		if(Input.GetButton("Dig") && m_onGround) {
			m_anim.SetBool("Dig", true);
			isDig = true;
			if (time_Dig >= 2) {
				groundCheck.SetActive(true);
				m_rb.AddForce(Vector3.up * 5,  ForceMode.Impulse);
				time_Dig = 0;
				m_anim.SetBool("Dig", false);
			} else {
				m_anim.SetBool("Dig", true);
				time_Dig += Time.deltaTime;
				isDig = false;
			}
		} else if (Input.GetButtonUp("Dig") && m_onGround) {
			isDig = false;
			m_anim.SetBool("Dig", false);
			groundCheck.SetActive(false);
		}

		if(Input.GetButton("FrontDig") && m_onGround) {
			m_anim.SetBool("FrontDig", true);
			isDig = true;
			if (time_Dig >= 2) {
				frontCheck.SetActive(true);
				m_rb.AddForce(Vector3.up * 5,  ForceMode.Impulse);
				time_Dig = 0;
				m_anim.SetBool("FrontDig", false);
			} else {
				m_anim.SetBool("FrontDig", true);
				time_Dig += Time.deltaTime;
				isDig = false;
				frontCheck.SetActive(false);
			}
		} else if (Input.GetButtonUp("FrontDig") && m_onGround) {
			isDig = false;
			m_anim.SetBool("FrontDig", false);
			frontCheck.SetActive(false);
		}

		if(Input.GetButton("Attack")) {
			m_anim.SetBool("Attack", true);
			isAtk = true;
			if (time_Atk <= 1) {
				time_Atk += Time.deltaTime;
			} else {
				Fire();
				time_Atk = 0;
			}
		} else if (Input.GetButtonUp("Attack")) {
			isAtk = false;
			m_anim.SetBool("Attack", false);
		}

		

	}

	void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "Ground") {
			m_onGround = true;
		} else {
			// TODO: Check for other collision objects / condition
		}

		if(other.gameObject.tag == "Boundary" && transform.position.y >= 0) {
			Vector3 pos = gameObject.transform.position;
			pos.x *= -1;
			gameObject.transform.position = pos;
		}

		if(other.gameObject.tag == "Enemy") {
			TakeDamage(10);
			m_rb.AddForce(Vector3.up * m_jumpForce/2, ForceMode.Impulse);
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Trigger" || other.gameObject.tag == "Death") {
			m_isDead = true;
		}

		if(other.gameObject.tag == "Boundary") {
			Vector3 pos = gameObject.transform.position;
			pos.x *= -1;
			gameObject.transform.position = pos;
		}
		if(other.gameObject.tag == "Limit") {
			Vector3 pos = gameObject.transform.position;
			pos.y *= -1;
			gameObject.transform.position = pos;
		}
		if(other.gameObject.tag == "End") {
			SceneManager.LoadScene("Main", LoadSceneMode.Single);
		}
	}

	void Fire() {
		GameObject fire = Instantiate(projectile, SpawnPoint.transform.position, Quaternion.identity);
			fire.GetComponent<Projectile>().ChangeDirection(new Vector3(facingRight? 1 : -1, 0, 0));
			if(!facingRight) {
				fire.transform.localScale *= -1;
			} 
	}

	public void SetMaxhealth(int health) {
		max_health = health;
	}

	public int UpdateHealth(int Hp) {
		cur_health = Hp;
		return cur_health;
	}

	public int TakeDamage(int dam) {
		cur_health -= dam;
		return cur_health;
	}
}
