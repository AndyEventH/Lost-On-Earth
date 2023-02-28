using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] GameObject enemyPrefab3;
    [SerializeField] BoxCollider2D bc;
    [SerializeField] float rangeMin;
    [SerializeField] float rangeMax;
    Vector2 cubeSize;
    Vector2 cubeCenter;
    private GameObject cam;
    private float cameraSize;

    private void Awake()
    {
        Transform cubeTrans = bc.GetComponent<Transform>();
        cubeCenter = cubeTrans.position;

        // Multiply by scale because it does affect the size of the collider
        cubeSize.x = cubeTrans.localScale.x * bc.size.x;
        cubeSize.y = cubeTrans.localScale.y * bc.size.y;
    }



    private void Start()
    {

        cameraSize = Camera.main.orthographicSize;
        cam = GameObject.Find("CM vcam1");
        for (int i = 0; i < Random.Range(rangeMin, rangeMax);i++) {
            Quaternion newRotation =  new Quaternion(0,-180,0,0);
            if (GameManager.gameManager.collectiblesAchieved == 0)
            {
                Vector3 newPos = new Vector3(transform.position.x + i * 9, GetRandomPositionY(enemyPrefab), 0);
                Instantiate(enemyPrefab, newPos, newRotation, transform.parent);
            }
            else if (GameManager.gameManager.collectiblesAchieved == 1)
            {
                Vector3 newPos = new Vector3(transform.position.x + i * 9, GetRandomPositionY(enemyPrefab2), 0);
                Instantiate(enemyPrefab2, newPos, newRotation, transform.parent);
            }
            else if (GameManager.gameManager.collectiblesAchieved == 2)
            {
                Vector3 newPos = new Vector3(transform.position.x + i * 9, GetRandomPositionY(enemyPrefab3), 0);
                Instantiate(enemyPrefab3, newPos, newRotation, transform.parent);
            }
        }
    }

    private void Update()
    {
        if (transform.position.x < cam.transform.position.x - cameraSize * 2)
        {
            Destroy(gameObject);
        }
    }


    private float GetRandomPositionY(GameObject prefab)
    {

        float prefabSizeY = prefab.GetComponent<CapsuleCollider2D>().size.y;
        float randomPosition = Random.Range(-cubeSize.y / 2+ prefabSizeY, cubeSize.y / 2- prefabSizeY);

        return cubeCenter.y + randomPosition;
    }
}
