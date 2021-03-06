using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Entity {
	

	public float gravity = 20;
	public float walkSpeed = 8;
	public float runSpeed = 12;
	public float acceleration = 30;
	public float jumpHeight = 12;
	public float slideDeceleration = 10;

	private float initiateSlideThreshold = 9;
	

	private float animationSpeed;
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private float moveDirX;

	private bool jumping;
	private bool sliding;
	private bool wallHolding;
	private bool stopSliding;


	private PlayerPhysics playerPhysics;
	private Animator animator;
	private GameManager manager;
	

	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator>();
		manager = Camera.main.GetComponent<GameManager>();
		animator.SetLayerWeight(1,1);
	}
	
	void Update () {

		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}
		

		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			if (wallHolding) {
				wallHolding = false;
				animator.SetBool("Wall Hold", false);
			}
			

			if (jumping) {
				jumping = false;
				animator.SetBool("Jumping",false);
			}
			

			if (sliding) {
				if (Mathf.Abs(currentSpeed) < .25f || stopSliding) {
					stopSliding = false;
					sliding = false;
					animator.SetBool("Sliding",false);
					playerPhysics.ResetCollider();
				}
			}
			
		
			if (Input.GetButtonDown("Slide")) {
				if (Mathf.Abs(currentSpeed) > initiateSlideThreshold) {
					sliding = true;
					animator.SetBool("Sliding",true);
					targetSpeed = 0;
					
					playerPhysics.SetCollider(new Vector3(10.3f,1.5f,3), new Vector3(.35f,.75f,0));
				}
			}
		}
		else {
			if (!wallHolding) {
				if (playerPhysics.canWallHold) {
					wallHolding = true;
					animator.SetBool("Wall Hold", true);
				}
			}
		}
		
	
		if (Input.GetButtonDown("Jump")) {
			if (sliding) {
				stopSliding = true;
			}
			else if (playerPhysics.grounded || wallHolding) {
				amountToMove.y = jumpHeight;
				jumping = true;
				animator.SetBool("Jumping",true);
				
				if (wallHolding) {
					wallHolding = false;
					animator.SetBool("Wall Hold", false);
				}
			}
		}
		

		animationSpeed = IncrementTowards(animationSpeed,Mathf.Abs(targetSpeed),acceleration);
		animator.SetFloat("Speed",animationSpeed);
		
	
		moveDirX = Input.GetAxisRaw("Horizontal");
		if (!sliding) {
			float speed = (Input.GetButton("Run"))?runSpeed:walkSpeed;
			targetSpeed = moveDirX * speed;
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
			

			if (moveDirX !=0 && !wallHolding) {
				transform.eulerAngles = (moveDirX>0)?Vector3.up * 180:Vector3.zero;
			}
		}
		else {
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed,slideDeceleration);
		}
		
	

		amountToMove.x = currentSpeed;
		
		if (wallHolding) {
			amountToMove.x = 0;
			if (Input.GetAxisRaw("Vertical") != -1) {
				amountToMove.y = 0;	
			}
		}
		
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime, moveDirX);
	
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Checkpoint") {
			manager.SetCheckpoint(c.transform.position);
		}
		if (c.tag == "Finish") {
			manager.EndLevel();
		}
	}
	

	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); 
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; 
		}
	}
}
