using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animDeath;

    [SerializeField] private Animator exhaustAnim;


    [SerializeField] private float speedMin = 20;
    [SerializeField] private float speedMax = 25;
    [SerializeField] private float minTimeBetweenMovement =1;
    [SerializeField] private float maxTimeBetweenMovement = 4;
    [SerializeField] private float minTimeBetweenShoot = 0.1f;
    [SerializeField] private float maxTimeBetweenShoot = 0.6f;
    [SerializeField] private float minTimeBetweenSpeed = 1;
    [SerializeField] private float maxTimeBetweenSpeed = 4;
    Quaternion newRotation = new Quaternion(0, -180, 0, 0);
    private Vector3 direction;
    private float cameraSize;
    private float randompointY;
    private bool isDead;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject collPrefab;
    //[SerializeField] private GameObject explosionPrefab;


    private GameObject bullet;
    
    Vector3 newPosition;
    private GameObject cam;
    private bool isShooting;
    [SerializeField] private GameObject shotPos;
    private float enemySize;
    [SerializeField] private int takeDmgOnCollision = 100;
    [SerializeField] private int takeDmgBullet = 50;
    // Start is called before the first frame update

    public UnitHealth _enemyHealth = new UnitHealth(100, 100);

    List<float> speeds;
    float randomSpeed;

    [SerializeField] SliderBar _healthBar;
    [SerializeField] private int enemyNumber;


    [Header("Audio")]
    [SerializeField] AudioSource enemySource;
    [SerializeField] AudioClip explodeDeath;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip gotHit;
    //private float safetyDestroy = 10f;
    void Start()
    {
        speeds = new List<float> { speedMin, speedMax };
        enemySize = gameObject.GetComponent<CapsuleCollider2D>().size.x;
        StartCoroutine(RandomizeSpeed());
        StartCoroutine(RandomizeMovement());
        StartCoroutine(RandomShoot());
        //float randompointY = Random.Range(-5.4f, 5.4f);
        cameraSize = Camera.main.orthographicSize;
        cam = GameObject.Find("CM vcam1");
        //StartCoroutine(RandomPositionMovement());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cam.transform.position.x-cameraSize*2- enemySize < transform.position.x && cam.transform.position.x+cameraSize*2+ enemySize > transform.position.x)
        {
            //Debug.Log("1 "+(cam.transform.position.x - cameraSize));
            //Debug.Log("2 " + transform.position.x);
            //Debug.Log("3 " + (cam.transform.position.x + cameraSize));
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
        if(transform.position.x< cam.transform.position.x - cameraSize * 2- enemySize)
        {
            Destroy(gameObject);
        }
        //newPosition = new Vector3(0, randompointY, 0);
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, Random.Range(speedMin.y, speedMax.y) * Time.deltaTime);

        Vector3 movement = new Vector3(randomSpeed * 0.1f, (randomSpeed/3) * direction.y, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        if(_enemyHealth.Health == 0 && !isDead)
        {
            //Instantiate(explosionPrefab,transform.position, transform.rotation, GameManager.gameManager.explosionContainer.transform);
            enemySource.PlayOneShot(explodeDeath);
            StartCoroutine(PlayAnimationCoroutineDeath());
            isDead = true;
        }
    }
    private IEnumerator RandomizeSpeed()
    {
        while (true)
        {
            if (Random.value > 0.5)
            {
                randomSpeed = speedMin;
                exhaustAnim.SetBool("isSprinting", false);
            }
            else
            {
                randomSpeed = speedMax;
                exhaustAnim.SetBool("isSprinting", true);
            }
            //Debug.Log(randomSpeed);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpeed, maxTimeBetweenSpeed));
        }
    }
    private IEnumerator RandomizeMovement()
    {
        while (true)
        {
            float[] randomDirection = new float[] { 0.1f, -0.1f, 0 };
            direction.y = randomDirection[Random.Range(0, randomDirection.Length)];
            /*if (Random.value < .2)
            {
                direction.y = 0.1f;
            }
            else if (Random.value < .4)
            {
                direction.y = -0.1f;
            }
            else
            {
                direction.y = 0f;
            }*/
            yield return new WaitForSeconds(Random.Range(minTimeBetweenMovement, maxTimeBetweenMovement));
        }
    }

    private IEnumerator RandomShoot()
    {
        while (true)
        {
            if (isShooting)
            {

                bullet = Instantiate(bulletPrefab, shotPos.transform.position, newRotation, GameManager.gameManager.bulletContainer.transform);
                enemySource.PlayOneShot(shoot);
            }
            yield return new WaitForSeconds(Random.Range(minTimeBetweenShoot, maxTimeBetweenShoot));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction.y *= -1;

        if (collision.collider.CompareTag("Player"))
        {
            EnemyTakeDmg(takeDmgOnCollision);
            //collision.collider.enabled = false;
            //Debug.Log(_enemyHealth.Health);
        }
    }


    private void EnemyTakeDmg(int dmg)
    {
        _enemyHealth.DmgUnit(dmg);
        _healthBar.SetSlider(_enemyHealth.Health);
        enemySource.PlayOneShot(gotHit);
        //Debug.Log(_enemyHealth.Health);
    }

    private void EnemyHeal(int heal)
    {
        _enemyHealth.HealUnit(heal);
        _healthBar.SetSlider(_enemyHealth.Health);
        //Debug.Log(_enemyHealth.Health);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            EnemyTakeDmg(takeDmgBullet);
            other.enabled = false;
        }
    }


    public IEnumerator PlayAnimationCoroutineDeath()
    {
        animDeath.SetBool("isKilled", true);
        // Wait one frame to let the animator enter the next animation first
        yield return null;

       
        yield return new WaitUntil(() => animDeath.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        //Debug.Log("enemy controller " + (enemyNumber - 1));
        GameManager.gameManager.enemiesKilled[enemyNumber-1]++;
        if ((!GameManager.gameManager.isCollected[enemyNumber - 1]) && GameManager.gameManager.enemiesKilled[enemyNumber - 1] >= GameManager.gameManager.randomDropRateCollectible[enemyNumber - 1])
        {
            Instantiate(collPrefab, shotPos.transform.position, newRotation, GameManager.gameManager.collectibleContainer.transform);
            GameManager.gameManager.isCollected[enemyNumber - 1] = true;
            //GameManager.gameManager.updateNeededCollectibles();
        }
        Destroy(gameObject);
    }

}
