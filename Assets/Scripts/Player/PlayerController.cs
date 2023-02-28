using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject bulletPrefab;
	private GameObject bullet;
	[SerializeField] private GameObject shotPos;
	//[SerializeField] private GameManager GM;
	private float cooldownUntilNextPress;
	[SerializeField] float coolDownBetweenShoot;

	[SerializeField] private Animator animDeath;
	[SerializeField] private Animator exhaustAnim;

	private bool isDead;

	private Rigidbody2D rb;
	[SerializeField] private int takeDmgOnCollision = 50;
	[SerializeField] private int takeDmgBullet = 10;

	//stamina
	[SerializeField] private float runSpeed = 15f;

	[SerializeField] private float walkSpeed = 10f;

	private float fatigueTimer = 0f;

	private bool isFatigued;
	private bool isRunning;
	private float exponentialPenaltySprint = 1f;

	[Range(1f, 100f)]
	public float stamina = 100f;

	private float speed;
	[SerializeField] private float reChargeRate = 1f;
	[SerializeField] private float drainRateSpeed = 15f;
	//stamina

	[SerializeField] SliderBar _healthBar;
	[SerializeField] SliderBar _staminaBar;


	[Header("Audio")]
	[SerializeField] AudioSource playerSource;
	[SerializeField] AudioClip explodeDeath;
	[SerializeField] AudioClip shoot;
	[SerializeField] AudioClip gotHit;
	[SerializeField] AudioClip collectibleAchieved;

	void Start()
	{
		speed = walkSpeed;
		rb = gameObject.GetComponent<Rigidbody2D>();
		//GM = FindObjectOfType<GameManager>();
	}

	private void MovementManager()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(speed * inputX, speed * inputY, 0);

		movement *= Time.deltaTime;

		transform.Translate(movement);
	}
	private void ShootManager()
	{
		if (Input.GetKeyUp(KeyCode.Space) && cooldownUntilNextPress < Time.time)
		{
			bullet = Instantiate(bulletPrefab, shotPos.transform.position, shotPos.transform.rotation, GameManager.gameManager.bulletContainer.transform);
			cooldownUntilNextPress = Time.time + coolDownBetweenShoot;
			playerSource.PlayOneShot(shoot);
		}
	}



	protected void StaminaSystem()
	{

		//sprint
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (stamina > 0 && !isFatigued)
			{
				speed = runSpeed;
				isRunning = true;

				exhaustAnim.SetBool("isSprinting", true);
			}
			else

			if (isRunning || isFatigued)
			{
				speed = walkSpeed;
				isRunning = false;
				exhaustAnim.SetBool("isSprinting", false);
				exponentialPenaltySprint = 1;
			}

			exponentialPenaltySprint += Time.deltaTime / 20f;

		}

		//sprint
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (isRunning || isFatigued)
			{
				speed = walkSpeed;
				isRunning = false;
				exhaustAnim.SetBool("isSprinting", false);
			}
		}




		if ((!Input.GetKey(KeyCode.LeftShift)) && exponentialPenaltySprint > 1)
		{
			exponentialPenaltySprint -= Time.deltaTime / 20f;
			if (exponentialPenaltySprint < 1)
			{
				exponentialPenaltySprint = 1f;
			}
		}




		//sprint
		if (isRunning)
		{
			//Debug.Log("stamina before removing" + stamina);
			stamina -= (Time.deltaTime * drainRateSpeed * exponentialPenaltySprint);
			_staminaBar.SetSlider((int)stamina);
			//Debug.Log("stamina after removing" + stamina);
			stamina += Time.deltaTime * reChargeRate;
            _staminaBar.SetSlider((int)stamina);
		}
		else if (!isFatigued)
		{
			stamina += Time.deltaTime * reChargeRate;
			_staminaBar.SetSlider((int)stamina);
		}



		//if (!isRunning && stamina > 0 && stamina <100 )
		//{
		//  stamina += Time.deltaTime * reChargeRate;
		//}

		if (stamina <= 0f && fatigueTimer <= 3)
		{
			fatigueTimer += Time.deltaTime;
			isFatigued = true;
		}
		else

		if (fatigueTimer >= 3)
		{
			stamina += Time.deltaTime * reChargeRate;
			_staminaBar.SetSlider((int)stamina);
			isFatigued = false;
			fatigueTimer = 0;
		}

		if (stamina < 0f)
		{
			stamina = 0f;
			_staminaBar.SetSlider((int)stamina);
		}

		if (stamina > 100f)
		{
			stamina = 100f;
			_staminaBar.SetSlider((int)stamina);
		}


	}

	private void Update()
    {
		StaminaSystem();
		//Debug.Log(stamina);
		MovementManager();
        ShootManager();
        if (GameManager.gameManager._playerHealth.Health == 0 && !isDead)
        {
			//Instantiate(explosionPrefab,transform.position, transform.rotation, GameManager.gameManager.explosionContainer.transform);
			playerSource.PlayOneShot(explodeDeath);
			StartCoroutine(PlayAnimationCoroutineDeath());
            isDead = true;
        }
		/*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            exhaustAnim.SetBool("isSprinting", true); 
        }else if (!Input.GetKey(KeyCode.LeftShift))
        {
            exhaustAnim.SetBool("isSprinting", false);
        }*/
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
		_healthBar.SetSlider(GameManager.gameManager._playerHealth.Health);
		playerSource.PlayOneShot(gotHit);
		//Debug.Log(GameManager.gameManager._playerHealth.Health);
	}

    private void PlayerHeal(int heal)
    {
        GameManager.gameManager._playerHealth.HealUnit(heal);

		_healthBar.SetSlider(GameManager.gameManager._playerHealth.Health);
		//Debug.Log(GameManager.gameManager._playerHealth.Health);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Enemy"))
        {
            PlayerTakeDmg(takeDmgOnCollision);
			collision.collider.enabled = false;
			//Debug.Log(GameManager.gameManager._playerHealth.Health);
		}
		else if (collision.collider.CompareTag("horizontalWalls"))
        {
			PlayerTakeDmg(100);

		}
    }
	GameObject checkIfStillLast;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            PlayerTakeDmg(takeDmgBullet);
            other.enabled = false;
		}
		if (other.CompareTag("Collectible"))
		{

			if (checkIfStillLast != other.gameObject)
			{
				playerSource.PlayOneShot(collectibleAchieved);
				PlayerHeal(100);
				checkIfStillLast = other.gameObject;
			}
			//other.enabled = false;

		}

	}

    public IEnumerator PlayAnimationCoroutineDeath()
    {
        animDeath.SetBool("isKilled", true);
        // Wait one frame to let the animator enter the next animation first
        yield return null;

        
        yield return new WaitUntil(() => animDeath.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
		Destroy(gameObject);
    }
}
