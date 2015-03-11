using UnityEngine;
using System.Collections;

public class BodyColliderBehavior : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
	
		}
	
		void OnTriggerEnter (Collider collider)
		{
				
		
				if (collider.gameObject.tag == "obstacle") {
						GameObject.Find ("Player").GetComponent<PlayerBehavior> ().Fall (this.gameObject);
						if (gameObject.name == "HeadCollider") {
								GameObject.Find ("Player").GetComponent<AudioSource> ().Stop ();
								GameObject.Find ("Player").GetComponent<AudioSource> ().clip = GameObject.Find ("Player").GetComponent<PlayerBehavior> ().hitClip;
								GameObject.Find ("Player").GetComponent<AudioSource> ().loop = true;
								GameObject.Find ("Player").GetComponent<AudioSource> ().Play ();

						}
							
				}
				
		
		}
}
