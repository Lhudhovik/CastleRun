using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObstacleTrigger : MonoBehaviour
{
		
		public Collider[] allowedColliders;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter (Collider triggerCollider)
		{

				for (int i = 0, length = allowedColliders.Length; i< length; i++) {


						if (allowedColliders [i] == triggerCollider) {

								GameObject.Find ("Level").GetComponent<LevelHandler> ().OnTriggerReached (this.gameObject);
								return;
						}

				}

						
		}
}
