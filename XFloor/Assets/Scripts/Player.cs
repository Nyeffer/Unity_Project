using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float m_moveSpeed = 1.0f;

	public float m_jumpForce = 10.0f;

	public bool m_isDead = false;
	private bool m_onGround = true;
	private bool m_stoppedJumping = true;

	private bool facingRight = true;
	private float m_origJumpForce;
	private Rigidbody m_rb;
	private Animator m_anim;

	public GameObject weapon;
	public Transform weaponHolder;

	public bool isAtk;

	void Awake() {
		m_origJumpForce = m_jumpForce;
		m_rb = gameObject.GetComponent<Rigidbody>();
		m_anim = gameObject.GetComponent<Animator>();
		m_isDead = false;
		isAtk = false;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.transform.position;
		// Player Move Left and Right
		if(Input.GetAxis("Horizontal") != 0 && !isAtk) {
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

		if(Input.GetButtonDown("Attack")) {
			m_anim.SetBool("Attack", true);
			isAtk = true;
			Instantiate(weapon, weaponHolder.transform.position, Quaternion.identity);
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
			Debug.Log("Land");
			m_onGround = true;
		} else {
			// TODO: Check for other collision objects / condition
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Trigger" || other.gameObject.tag == "Death") {
			m_isDead = true;
		}
	}
}
