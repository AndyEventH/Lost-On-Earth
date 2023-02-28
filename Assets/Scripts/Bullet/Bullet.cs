using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Animator anim;
    Rigidbody2D rb;
    private GameManager GM;
    Quaternion newRotation = new Quaternion(0, -180, 0, 0);
    [SerializeField] float thrust;
    Vector3 nextPos;
    private bool isEnemy;
    private GameObject cam;
    private float cameraSize;
    private float bulletSize;

    //private float safetyDestroy = 10f;
    // Start is called before the first frame update
    void Start()
    {
        bulletSize = gameObject.GetComponent<CircleCollider2D>().radius;
        cameraSize = Camera.main.orthographicSize;
        cam = GameObject.Find("CM vcam1");
        if (newRotation == transform.rotation)
        {
            isEnemy = true;
        }
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayAnimationCoroutineShoot());
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!(cam.transform.position.x - cameraSize*2- bulletSize < transform.position.x && cam.transform.position.x + cameraSize*2+ bulletSize > transform.position.x))
        {
            Destroy(gameObject);
        }
        if (!isEnemy)
        {
            nextPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else
        {
            nextPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        //transform.position = Vector3.Lerp(transform.position, nextPos, Speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, nextPos, thrust * Time.deltaTime);

    }
    //private void OnCollisionEnter(Collision other)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            StartCoroutine(PlayAnimationCoroutineHit());

        }
        else if (other.CompareTag("Player") && isEnemy)
        {
            StartCoroutine(PlayAnimationCoroutineHit());

        }
    }
    public IEnumerator PlayAnimationCoroutineHit()
    {
        anim.SetBool("isHit", true);
        // Wait one frame to let the animator enter the next animation first
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        Destroy(gameObject);
    }
    public IEnumerator PlayAnimationCoroutineShoot()
    {
        anim.SetBool("isShooting", true);
        // Wait one frame to let the animator enter the next animation first
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f); 
        anim.SetBool("isShooting", false);
        
    }


}
