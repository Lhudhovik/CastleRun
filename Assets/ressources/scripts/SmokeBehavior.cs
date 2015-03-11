using UnityEngine;

using System.Collections;

public class SmokeBehavior : MonoBehaviour
{


		private const string BASE_SOUND_NAME = "particle_base";
		public float FREQUENCY_MODIFICATION_CEIL;
		private int _frequencyMultiplier = 1;
		private float _lastTime = 0f;
		public Vector3 speed = new Vector3 ();
		public AudioClip[] audioClips;
		public float soundGenerationFrequency = 3.5f;//average
		
		
		// Use this for initialization
		void Start ()
		{
	

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (GameObject.Find ("Player").GetComponent<PlayerBehavior> ().gameStarted) {

						if (Mathf.Abs (GameObject.Find ("Player").transform.position.z - this.transform.position.z) < FREQUENCY_MODIFICATION_CEIL) 

								_frequencyMultiplier = 2;
						else
								_frequencyMultiplier = 1;

						transform.Translate (speed);
				}
		}

		void FixedUpdate ()
		{
			
				PlayMonsterSound ();
	

		}
		
		private void PlayMonsterSound ()
		{

				if (Time.fixedTime - _lastTime > soundGenerationFrequency / _frequencyMultiplier + 5) {
						int i = GetComponents<AudioSource> ().Length - 1;
						while (i>-1) {
				
								if (GetComponents<AudioSource> () [i].clip.name != BASE_SOUND_NAME && audioClips != null && audioClips.Length > 0) {
					
										int audioClipIndex;
										do {
						
												audioClipIndex = Random.Range (0, audioClips.Length - 1);
						
						
										} while(audioClips.Length>1 && GetComponents<AudioSource> () [i].clip.name == audioClips [audioClipIndex].name);
										
										GetComponents<AudioSource> () [i].Stop ();
										GetComponents<AudioSource> () [i].clip = audioClips [audioClipIndex];
										GetComponents<AudioSource> () [i].loop = false;
										GetComponents<AudioSource> () [i].Play ();
					
										break;
								}
								i--;
						}
						_lastTime = Time.fixedTime;
				}

		}
}
