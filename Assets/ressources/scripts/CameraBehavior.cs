using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{

		public float Test = 5f;
		private PlayerBehavior player;
		private bool isShaking = false;
		public float shakeSpeed = 0.5f;
		public float yVariationSpeed = 0.5f;
		public float shakeAngleMin = -2f, shakeAngleMax = 2f, yVariationMin = -2f, yVariationMax = 2f;
		private float originalY;
		// Use this for initialization
		void Start ()
		{
				originalY = transform.position.y;
		}
	
		// Update is called once per frame
		void Update ()
		{
	

				if (player == null)
						player = GameObject.Find ("Player").GetComponent<PlayerBehavior> ();
				if (player.state != (int)PlayerBehavior.States.Still && player.state != (int)PlayerBehavior.States.FallingBack && player.state != (int)PlayerBehavior.States.FallingForward)
						isShaking = true;
				else
						isShaking = false;

				if (isShaking) {
					
					
						Shake ();

				}
		}

		void Shake ()
		{

				float cameraAngleZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z; 
				
				if (cameraAngleZ < shakeAngleMin || cameraAngleZ > shakeAngleMax) {

						
						shakeSpeed = -shakeSpeed;
						
				} 
				if (transform.position.y > originalY + yVariationMax || transform.position.y < originalY + yVariationMin) {
						yVariationSpeed = -yVariationSpeed;
				}
				

				transform.Rotate (new Vector3 (0, 0, shakeSpeed));		
				transform.Translate (new Vector3 (0, yVariationSpeed, 0));                   
				
		}
}
