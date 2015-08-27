using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public CharacterController motor;

	private float speed = 4f;
	private float tempX;
	private float tempY;
	private Vector3 tempPos;
	private bool jumping = false;
	private bool canJump = true;
	private float timer = 0;
	private float jumpCD = 0;
	private bool moving = false;
	private Transform trans;
    /*
     * TO DO:
     * Movement --- DONE
     *      Should use the CharacterController which is already attached to this GameObject
     *      Be able to move left and right
     *      Collide with/be stopped by walls
     *      Not move too quickly or slowly
     *          Remember that movement happens every frame
     * Jumping/Falling --- DONE
     *      Fall to the ground, and not through it
     *      Able to jump
     *      Can reach platforms to the right, but not the one on the left
     *      Only able to jump while standing on the ground
     * Input --- DONE
     *      Ideally, use the KeyboardInput script which is already attached to this GameObject
     *      A & D for left and right movement
     *      Space for jumping
     * Moving Platform 
     *      When standing on the platform, the character should stay on it/move relative to the moving platform
     *      When not standing on the platform, revert to normal behavior
     * Enemy
     *      If the character touches the enemy, he should "die"
     *      
     * 
     * 
     * 
     * Variables you might want:
     *      References to the CharacterController and Keyboard input classes
     *      Speed values for moving, falling, and jumping
     *      A value to keep track of the player's movement speed and direction
     *      You will probably need to use the Update function as well as create functions for moving platforms and enemies
     */
	void Update(){
		//if(this.gameObject.GetComponent<KeyboardInput>.xAxis())

		if (!jumping) {
			Fall ();
		} else {
			motor.Move(new Vector3(0, 5*speed * Time.deltaTime, 0));
			if(timer == 10){
				jumping = false;
				timer = 0;
			}
			timer++;
		}
		if (Input.GetAxis ("Horizontal")<0) {//move left
			Debug.Log("left");
			motor.Move(new Vector3((-1) * speed * Time.deltaTime, 0, 0));
			moving = false;
		}
		if (Input.GetAxis ("Horizontal")>0) {//move right
			Debug.Log("right");
			motor.Move(new Vector3(speed * Time.deltaTime, 0, 0));
			moving = false;
		}
		if (!jumping) {
			if (this.gameObject.GetComponent<KeyboardInput> ().JumpButtonPressed) {
				Debug.Log ("jumped");
				if (jumpCD==0) {
					jumping = true;
					jumpCD=20;
				}
			}
			if(jumpCD>0){
				jumpCD--;
			}
		}

		if (moving) {
			tempPos = trans.transform.position;
			tempPos.y = 1.5f;
			this.transform.position = tempPos;
		}
	}

	void Fall(){
		motor.Move(new Vector3(0, (-5) * speed * Time.deltaTime, 0));
	}

	void OnPlatform(Transform t){
		moving = true;
		trans = t;
	}

	void OffPlatform(Transform t){
		moving = false;
	}
	void KillPlayer(){
		Application.LoadLevel(0);
		Debug.Log ("asdf");
	}

}