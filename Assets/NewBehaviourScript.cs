using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class NewBehaviourScript : MonoBehaviour { 

	/// The current Player the Spawner is spawning creatures for
	public GameObject player { get; set; }

	// The max bounds of the spawning area
	public Rect SpawnArea { get; set; }
	// The minimum distance to spawn from -- this prevents spawning too closely
	public Rect SpawnClearArea { get; set; }

	public int Density { get; set;}

	public GameObject[] InstanceTypes { get; set; }

	void Start() { 
		Density = 100;
		SpawnArea = new Rect (0, 0, Screen.width, Screen.height);
		SpawnClearArea = new Rect (0, 0, Screen.width / 2, Screen.height / 2);
		player = GameObject.Find ("Cell");
		InstanceTypes = new GameObject[] { 
			player
		};
		Debug.LogWarning ("asdfa");
		StartCoroutine(dostuff());
	}

	System.Random rand = new System.Random();
		
	GameObject CreateCreature() { 
		var creature = Instantiate (InstanceTypes [rand.Next() % InstanceTypes.Length]);
		creature.tag = "creature";
		creature.transform.position = player.transform.position + new Vector3(
			(float)rand.NextDouble () * (float)SpawnArea.width - SpawnArea.width / 2,
			(float)rand.NextDouble () * (float)SpawnArea.height - SpawnArea.height / 2,
			0);
		Debug.LogWarning (new { Creature = creature.transform.position });		
		return creature;
	}

	IEnumerator dostuff() { 
		// start by initially populating the space around the current Player
		for (int i = 0; i < Density; i++)
			CreateCreature ();

		while (true) { 
			for (int i = GameObject.FindGameObjectsWithTag ("creature").Length; i < Density; i++)
				CreateCreature ();

			Debug.LogWarning (new { Player = player.transform.position });
			yield return 0;
		}
	}
}