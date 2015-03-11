using UnityEngine;
using System.Collections;

public class PackBehavior : MonoBehaviour
{

		public float speed = 1;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (this.gameObject.activeSelf)
						transform.Translate (new Vector3 (0, 0, speed));	

		}

		
}
