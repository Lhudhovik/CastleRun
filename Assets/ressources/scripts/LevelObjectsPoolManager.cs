using UnityEngine;
using System.Collections;

public class LevelObjectsPoolManager
{


		public static string USED = "used", NOTUSED = "notUsed";
		private static ArrayList lowObstacles;
		private static ArrayList highObstacles;
		private static ArrayList americanTrees, japaneseTrees, salixTrees, bushes;
		
		public static void Initialize (int elementsByType)
		{
				lowObstacles = new ArrayList ();
		
				highObstacles = new ArrayList ();
				
				americanTrees = new ArrayList ();

				japaneseTrees = new ArrayList ();

				salixTrees = new ArrayList ();

				bushes = new ArrayList ();
				for (int i = 0; i< elementsByType; i++) {
					
						lowObstacles.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().lowObstaclePrefab, new Vector3 (0, 0, 0), Quaternion.identity));
						((GameObject)lowObstacles [i]).SetActive (false);
						((GameObject)lowObstacles [i]).tag = NOTUSED;
						

						highObstacles.Add ((GameObject)Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().highObstaclePrefab));
						((GameObject)highObstacles [i]).SetActive (false);
						((GameObject)highObstacles [i]).tag = NOTUSED;

						
			
				}

				elementsByType = 12;
				float treeX, treeZ = 1;
				for (int i = 0; i< elementsByType; i++) {
						
						treeX = (i % 2 == 0 ? 1 : -1) * (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesX + Random.Range (0, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesVariationX));
						treeZ += Random.Range (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesZDistance * 0.5f, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesZDistance);
						americanTrees.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().trees [0], new Vector3 (treeX, 0, treeZ), Quaternion.identity));
						((GameObject)americanTrees [i]).SetActive (true);
						((GameObject)americanTrees [i]).transform.Rotate (new Vector3 (-90f, 0f, 0f));
						((GameObject)americanTrees [i]).tag = NOTUSED;
						treeX = (i % 2 == 0 ? 1 : -1) * (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesX + Random.Range (0, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesVariationX));
						GameObject.Find ("Level").GetComponent<LevelHandler> ().currentTrees.Add ((GameObject)americanTrees [i]);

						japaneseTrees.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().trees [1], new Vector3 (treeX, 0, treeZ), Quaternion.identity));
						((GameObject)japaneseTrees [i]).SetActive (true);
						((GameObject)japaneseTrees [i]).transform.Rotate (new Vector3 (-90f, 0f, 0f));
						((GameObject)japaneseTrees [i]).tag = NOTUSED;
						treeX = (i % 2 == 0 ? 1 : -1) * (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesX + Random.Range (0, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesVariationX));
						GameObject.Find ("Level").GetComponent<LevelHandler> ().currentTrees.Add ((GameObject)japaneseTrees [i]);
						

						treeZ += Random.Range (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesZDistance * 0.5f, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesZDistance);
						

						salixTrees.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().trees [2], new Vector3 (treeX, 0, treeZ), Quaternion.identity));
						((GameObject)salixTrees [i]).SetActive (true);
						((GameObject)salixTrees [i]).transform.Rotate (new Vector3 (-90f, 0f, 0f));
						((GameObject)salixTrees [i]).tag = NOTUSED;
						treeX = (i % 2 == 0 ? 1 : -1) * (GameObject.Find ("Level").GetComponent<LevelHandler> ().treesX + Random.Range (0, GameObject.Find ("Level").GetComponent<LevelHandler> ().treesVariationX));
						GameObject.Find ("Level").GetComponent<LevelHandler> ().currentTrees.Add ((GameObject)salixTrees [i]);
						
						bushes.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().trees [3], new Vector3 (treeX, 0, treeZ), Quaternion.identity));
						((GameObject)bushes [i]).SetActive (true);
						((GameObject)bushes [i]).transform.Rotate (new Vector3 (-90f, 0f, 0f));
						((GameObject)bushes [i]).tag = NOTUSED;
						GameObject.Find ("Level").GetComponent<LevelHandler> ().currentTrees.Add ((GameObject)bushes [i]);
						GameObject.Find ("Level").GetComponent<LevelHandler> ().lastTreeZ = ((GameObject)bushes [i]).transform.position.z;
			
			
			
				}

		
		}
		
		public static GameObject GetObstacle (bool isLow, bool active)
		{
				ArrayList checkedArrayList = isLow ? lowObstacles : highObstacles;
				

				for (int i= 0, length = checkedArrayList.Count; i<length; i++) {
			
						if (((GameObject)checkedArrayList [i]).tag == NOTUSED) {
								((GameObject)checkedArrayList [i]).SetActive (active);
								((GameObject)checkedArrayList [i]).tag = USED;
								return (GameObject)checkedArrayList [i];
						}
			
				}
		
				checkedArrayList.Add (Object.Instantiate (isLow ? GameObject.Find ("Level").GetComponent<LevelHandler> ().lowObstaclePrefab : GameObject.Find ("Level").GetComponent<LevelHandler> ().highObstaclePrefab, new Vector3 (0, 0, 0), Quaternion.identity));
				((GameObject)checkedArrayList [checkedArrayList.Count - 1]).SetActive (active);
				((GameObject)checkedArrayList [checkedArrayList.Count - 1]).tag = USED;
				return (GameObject)checkedArrayList [checkedArrayList.Count - 1];
		
		}

		public static GameObject GetTree (int type, bool active)
		{

				ArrayList checkedArrayList;
				switch (type) {

				case 1:
						checkedArrayList = japaneseTrees;
						break;
				case 2:
						checkedArrayList = salixTrees;
						break;
				case 3:
						checkedArrayList = bushes;
						break;
				default: 
						checkedArrayList = americanTrees;
						break;



				}

				for (int i= 0, length = checkedArrayList.Count; i<length; i++) {
			
						if (((GameObject)checkedArrayList [i]).tag == NOTUSED) {
								((GameObject)checkedArrayList [i]).SetActive (active);
								((GameObject)checkedArrayList [i]).tag = USED;
								return (GameObject)checkedArrayList [i];
						}
			
				}
		
				checkedArrayList.Add (Object.Instantiate (GameObject.Find ("Level").GetComponent<LevelHandler> ().trees [type], new Vector3 (0, 0, 0), Quaternion.identity));
				((GameObject)checkedArrayList [checkedArrayList.Count - 1]).SetActive (active);
				((GameObject)checkedArrayList [checkedArrayList.Count - 1]).transform.Rotate (new Vector3 (-90f, 0f, 0f));
				((GameObject)checkedArrayList [checkedArrayList.Count - 1]).tag = USED;
				return (GameObject)checkedArrayList [checkedArrayList.Count - 1];

		}

		public static void StockObject (GameObject stockedObject)
		{
				
					
				stockedObject.tag = NOTUSED;
				stockedObject.SetActive (false);
				if (stockedObject.GetComponent<AudioSource> ()) 
						stockedObject.GetComponent<AudioSource> ().Stop ();

				

		}

		public static int ObjectCount ()
		{
				return lowObstacles.Count + highObstacles.Count;

		}
		
		
	
}
