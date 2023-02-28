using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	//Variables
	private float cameraSize;
	private Vector3 playerSize;
	private Vector3 exhaustShip3Size;
	[SerializeField] private float Speed = 5;
	//[SerializeField] private float xPosMovement = 1;
	private Vector3 nextPos;
	public Transform Player;
	public Transform ExhaustShip3;

	private void Start()
    {
		cameraSize = Camera.main.orthographicSize;
		playerSize = Player.GetComponent<CapsuleCollider2D>().size;
		exhaustShip3Size = ExhaustShip3.GetComponent<CapsuleCollider2D>().size;
		//Debug.Log(cameraSize);

	}

    //Camera movement
    private void FixedUpdate()
	{
		nextPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, nextPos, Speed * Time.deltaTime);
		if(Player && Player.position.x < transform.position.x- cameraSize*2+ playerSize.x + exhaustShip3Size.x*2)
        {
			//Player.transform.parent = transform;
            Player.position = new Vector3(transform.position.x- cameraSize * 2 + playerSize.x + exhaustShip3Size.x*2, Player.position.y,Player.position.z);
        }
        else if
		(Player && Player.position.x > transform.position.x + cameraSize * 2 - playerSize.x)
		{
			//Player.transform.parent = transform;
            Player.position = new Vector3(transform.position.x + cameraSize * 2 - playerSize.x, Player.position.y, Player.position.z);
            
        }
	}

}
