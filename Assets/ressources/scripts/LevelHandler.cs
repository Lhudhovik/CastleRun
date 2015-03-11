using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{

		// Use this for initialization
		public GameObject lowObstaclePrefab, highObstaclePrefab;
		public GameObject trigger1, trigger2;
		public GameObject[] trees;
		public ArrayList currentObstacles, currentTrees;
		public int prefabCount = 1, maxObjectCount = 5;
		public float obstacleZDistanceRangeMin = -3, obstacleZDistanceRangeMax = 15, obstacleLowX = 20f, obstacleHighX = -2.5f;
		public float difficultyFactor = 1.5f;
		public float treesVariationX = 3f, treesX = 2.5f, treesZDistance = 8;
		private float obstacleNewX, obstacleNewZ;
		public float lastTreeZ, lastTreeX = 0;

		void Start ()
		{
				
				currentObstacles = new ArrayList ();
				currentTrees = new ArrayList ();
				LevelObjectsPoolManager.Initialize (prefabCount);
				trigger1.transform.position = new Vector3 (0, 0, obstacleZDistanceRangeMax * 0.5f);
				trigger2.transform.position = new Vector3 (0, 0, obstacleZDistanceRangeMax);
		}
	
		// Update is called once per frame
		void Update ()
		{
				//Debug.Log (ObstaclePoolManager.ObjectCount ());
				
				CheckObstaclesPosition ();
				CheckTreesPosition ();
				GameObject.Find ("Ground").transform.position = new Vector3 (0, 0, GameObject.Find ("Player").transform.position.z - 2.5f);

	
		}

		private void AddObstacle (bool isLow)
		{
				GameObject newObstacle = LevelObjectsPoolManager.GetObstacle (isLow, false);
				currentObstacles.Add (newObstacle);
				if (isLow == false) {
						newObstacle.transform.localEulerAngles = new Vector3 (0, Random.Range (0, 2) == 0 ? 180 : 0, 0);
				} else
						newObstacle.transform.localEulerAngles = new Vector3 (0, -90, 0);


				obstacleNewX = isLow ? obstacleLowX : (newObstacle.transform.rotation.y != 0 ? -obstacleHighX : obstacleHighX);
				obstacleNewZ = currentObstacles.Count > 1 ? ((GameObject)currentObstacles [currentObstacles.Count - 2]).transform.position.z + Random.Range (obstacleZDistanceRangeMax * 0.5f, obstacleZDistanceRangeMax * 0.65f) : GameObject.Find ("Player").transform.position.z + obstacleZDistanceRangeMax; 	



				newObstacle.transform.position = new Vector3 (obstacleNewX, 0, obstacleNewZ / Random.Range (1f, difficultyFactor));
				newObstacle.SetActive (true);
				newObstacle.GetComponent<AudioSource> ().Play ();
		}

		private void AddTree (int type)
		{
				
				GameObject newTree = LevelObjectsPoolManager.GetTree (type, false);
				float nextX, nextZ;		
				currentTrees.Add (newTree);
				
		
		
				nextX = lastTreeX = (lastTreeX > 0 ? -1 : 1) * (treesX + Random.Range (0, treesVariationX));
				nextZ = lastTreeZ = lastTreeZ + Random.Range (treesZDistance * 0.5f, treesZDistance);
			
		
				newTree.transform.position = new Vector3 (nextX, 0, nextZ);
				newTree.SetActive (true);
		}

		private void CheckObstaclesPosition ()
		{

				for (int i = 0, length = currentObstacles.Count; i< length; i++) {
					
						if (((GameObject)currentObstacles [i]).transform.position.z < GameObject.Find ("Player").transform.position.z + obstacleZDistanceRangeMin) {

								LevelObjectsPoolManager.StockObject ((GameObject)currentObstacles [i]);
								currentObstacles.Remove (currentObstacles [i]);
								i--;
								length--;
								continue;
						}
						
				}
			



		}

		private void CheckTreesPosition ()
		{
		
				for (int i = 0, length = currentTrees.Count; i< length; i++) {
			
						if (((GameObject)currentTrees [i]).transform.position.z < GameObject.Find ("Player").transform.position.z + obstacleZDistanceRangeMin) {
				
								LevelObjectsPoolManager.StockObject ((GameObject)currentTrees [i]);
								currentTrees.Remove (currentTrees [i]);
								i--;
								length--;
								AddTree (Random.Range (0, 4));
								continue;
						}
						
				}
		
		
		
		
		}

		public void OnTriggerReached (GameObject trigger)
		{
				if (GameObject.Find ("Canvas"))
						GameObject.DestroyObject (GameObject.Find ("Canvas"));
				if (currentObstacles.Count < maxObjectCount)
						AddObstacle (Random.Range (0, 2) == 0 ? false : true);
				trigger.transform.position = new Vector3 (0, 0, GameObject.Find ("Player").transform.position.z + obstacleZDistanceRangeMax);

		}

}
