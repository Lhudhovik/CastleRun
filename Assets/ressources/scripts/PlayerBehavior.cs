using UnityEngine;
using UnityEditor;
using System.Collections;
using AssemblyCSharp;

public class PlayerBehavior : MonoBehaviour
{

		
		public enum States
		{
				Still = 0,
				StartingRun = 1,
				Running = 2,
				Sliding = 3,
				Jumping = 4,
				FallingBack = 5,
				FallingForward = 6
		}
		public AudioClip jumpClip, runClip, slideClip, hitClip, groundhitClip ;
		public bool gameStarted = false;
		private Animator animator;
		private string animatorParameterState = "state", animatorParameterSlide = "slide", animatorParameterJump = "jump";
		public Vector3 deceleration = new Vector3 (0, 0, 1f), velocityMax = new Vector3 (0, 1, 50), acceleration = new Vector3 (0, 0, 1f);
		private Vector3 velocity = new Vector3 (0, 0, 0);
	public string nameObject;
		//private float currentJumpTime = 1, maxJumpTime = 2;
		public float maxJumpHeight = 30;
		public float maxSightAngleLeft = -30, maxSightAngleRight = 30;
		public float maxSightAngleUp = -93, maxSightAngleDown = 93;
		public bool cameraLocked = false;
		public int state = (int)States.Still;
		
		
		
		// Use this for initialization
		void Start ()
		{
				animator = GetComponent<Animator> ();
				
				
				
		}
	
		// Update is called once per frame
		void Update ()
		{	
				//Debug.Log (state);
				if (Input.GetButton ("ExitGame"))
						Application.Quit ();
				if (gameStarted) {
						if (Input.GetButton ("LockCamera"))
								cameraLocked = !cameraLocked;
			
						if (!cameraLocked && GameObject.Find ("PlayerCamera").camera)
								MoveCamera ();
			
						if (Input.GetButton ("Run") && state != (int)States.FallingBack && state != (int)States.FallingForward) {
								if (GetComponent<AudioSource> ().clip != runClip && GetComponent<AudioSource> ().clip != slideClip && state == (int)States.Running) {

										GetComponent<AudioSource> ().Stop ();
										GetComponent<AudioSource> ().clip = runClip;
										GetComponent<AudioSource> ().loop = true;
										GetComponent<AudioSource> ().PlayDelayed (0.2f);
										//GetComponent<AudioSource> ().Play ();
								}
								Run ();
				
				
						} else if (velocity.z > 0 && ((state == (int)States.FallingBack && state == (int)States.FallingForward) || (Input.GetButtonDown ("Slide") == false && Input.GetButtonDown ("Jump") == false))) {
								velocity.z -= deceleration.z / 100f;
				
						}
			
			
						if (Input.GetButtonDown ("Jump") && (state == (int)States.StartingRun || state == (int)States.Running || state == (int)States.Sliding))
								Jump ();
			
						if (Input.GetButtonDown ("Slide") && (state == (int)States.StartingRun || state == (int)States.Running || state == (int)States.Jumping)) {
								Slide ();
				
						}
			
			
						UpdatePosition ();
				} else if (Input.GetButton ("Run"))
						gameStarted = true;
				
				
		}
		
		private void Jump ()
		{

				if (transform.position.y == 0) {

						velocity.y = velocityMax.y;
						state = (int)States.Jumping;
						animator.SetTrigger (animatorParameterJump);
						GetComponent<AudioSource> ().Stop ();
						GetComponent<AudioSource> ().clip = jumpClip;
						GetComponent<AudioSource> ().loop = false;
						GetComponent<AudioSource> ().Play ();
				}
				

				Run ();
		
		
		}
	
		private void Run ()
		{

				if (state == (int)States.Still) {
						state = (int)States.Running;
						animator.SetInteger (animatorParameterState, state);
				}
				if (velocity.z < velocityMax.z / 100f)
						velocity.z += acceleration.z / 100f;
				else
						velocity.z = velocityMax.z / 100f;

		}

		private void Slide ()
		{
				
				animator.SetTrigger (animatorParameterSlide);
				GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().clip = slideClip;
				GetComponent<AudioSource> ().loop = false;
				GetComponent<AudioSource> ().Play ();
				Run ();
		}

		private void MoveCamera ()
		{
				float angleDown = maxSightAngleDown;
				if (state == (int)States.Running)
						angleDown = maxSightAngleDown * 0.5f;
			
				float horizontalRotation = Utils.FloatRemap (Input.mousePosition.x, 0, GameObject.Find ("PlayerCamera").camera.pixelWidth, maxSightAngleLeft, maxSightAngleRight);
				float verticalRotatation = Utils.FloatRemap (Input.mousePosition.y, 0, GameObject.Find ("PlayerCamera").camera.pixelHeight, angleDown, maxSightAngleUp);
				if (horizontalRotation < 0)
						horizontalRotation += 360;
				if (verticalRotatation < 0)
						verticalRotatation += 360;


				GameObject.Find ("PlayerCamera").transform.localEulerAngles = new Vector3 (verticalRotatation, horizontalRotation, 0);
				
		}

		private void UpdatePosition ()
		{
			
				
				if (velocity.z < 0) {
						velocity.z = 0;
						if (state != (int)States.Still && state != (int)States.FallingForward && state != (int)States.FallingBack) {

								state = (int)States.Still;
								GetComponent<AudioSource> ().Stop ();
				
								animator.SetInteger (animatorParameterState, state);
						}
						
				}
						
				if (state == (int)States.Jumping || state == (int)States.FallingForward || state == (int)States.FallingBack)		
						velocity.y -= deceleration.y; 
				
				if (transform.position.y < 0) {
						velocity.y = 0;
						transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
						if (state != (int)States.Running && state != (int)States.FallingForward && state != (int)States.FallingBack) {

								state = (int)States.Running;
								
						}
				}
								
						
					
				
				
				transform.Translate (velocity);
				
			
		}

		public void EndFall ()
		{
				

				Application.LoadLevel (Application.loadedLevelName);

		}

		public void EndJump ()
		{
				/*if (GetComponent<AudioSource> ().clip != hitClip && GetComponent<AudioSource> ().clip != groundhitClip) {

						GetComponent<AudioSource> ().Stop ();
						GetComponent<AudioSource> ().clip = runClip;
						GetComponent<AudioSource> ().loop = true;
						GetComponent<AudioSource> ().Play ();
				}*/
		}

		public void EndSlide ()
		{
				GetComponent<AudioSource> ().Stop ();
				GetComponent<AudioSource> ().clip = runClip;
				GetComponent<AudioSource> ().loop = true;
				GetComponent<AudioSource> ().Play ();
		}

		public void Fall (GameObject touchedCollider)
		{
				Debug.Log ("touch");
				state = touchedCollider.name == "LegsCollider" ? (int)States.FallingForward : (int)States.FallingBack;
				deceleration.z /= 2f;
				animator.SetInteger (animatorParameterState, state);
				
		}

	
}
