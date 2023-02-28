using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    [SerializeField] private int collectibleNumber;

    //[SerializeField] EnvironmentManager environmentManager;
    // The target (cylinder) position.
    //[SerializeField] private Transform target;

    void Update()
    {

        // Move our position a step closer to the target.
        var step = speed * Time.deltaTime; // calculate distance to move
        if (GameManager.gameManager.PlayerReference)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.gameManager.PlayerReference.position, step);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (GameManager.gameManager.collectiblesAchieved != collectibleNumber)
            {
                GameManager.gameManager.collectiblesAchieved = collectibleNumber;
            }
            //Debug.Log("collectibles " + GameManager.gameManager.collectiblesAchieved);
            Destroy(gameObject);
        }
    }
}
