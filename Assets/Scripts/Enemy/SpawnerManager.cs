using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    private GameObject cam;
    [SerializeField] float timeMin;
    [SerializeField] float timeMax;
    [SerializeField] GameObject EnemySpawnerPrefab;
    [SerializeField] Transform EnemySpawners;
    [SerializeField] float distanceFromCam;

    private float cameraSize;
    // Start is called before the first frame update
    void Start()
    {

        cameraSize = Camera.main.orthographicSize;
        cam = GameObject.Find("CM vcam1");
        StartCoroutine(WaitAndSpawn(Random.Range(timeMin, timeMax)));
    }

    // Update is called once per frame
    void Update()
    {


    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Vector3 newPosition = new Vector3(cam.transform.position.x + distanceFromCam, 0, 0);
            /*foreach (Transform child in EnemySpawners.transform) {
                GameObject.Destroy(child.gameObject);
            }*/
            Instantiate(EnemySpawnerPrefab, newPosition, transform.rotation, EnemySpawners.transform);
        }
    }
}
