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
		Density = 20;
		var w = 40.0f;
		SpawnArea = new Rect (-w / 2, -w / 2, w, w);
		SpawnClearArea = new Rect (SpawnArea.xMin / 2, SpawnArea.yMin / 2, SpawnArea.width / 2, SpawnArea.height / 2);
		player = GameObject.Find ("Cell");
		InstanceTypes = new GameObject[] { 
			GameObject.Find("flagella")
		};
		Debug.LogWarning ("asdfa");
		StartCoroutine(dostuff());
	}

	System.Random rand = new System.Random();
		
	GameObject CreateCreature(bool avoidNearby = true) { 
		var creature = Instantiate (InstanceTypes [rand.Next() % InstanceTypes.Length]);
		creature.tag = "creature";
		creature.transform.Rotate (Vector3.forward, (float)rand.NextDouble () * 360);
		creature.transform.position = player.transform.position + genOffset (avoidNearby);
		Debug.LogWarning (new { Creature = creature.transform.position });		
		return creature;
	}

	Vector3 genOffset(bool avoidNearby) { 
		Vector3 v = Vector3.zero;
		do { 
			v = new Vector3 (
				(float)rand.NextDouble () * (float)SpawnArea.width + SpawnArea.xMin,
				(float)rand.NextDouble () * (float)SpawnArea.height + SpawnArea.yMin,
				0);
		} while(avoidNearby && SpawnClearArea.Contains (v)) ;
		return v;
	}
	IEnumerator dostuff() { 
		// start by initially populating the space around the current Player
		for (int i = 0; i < Density; i++)
			CreateCreature (false);

		while (true) { 
			for (int i = GameObject.FindGameObjectsWithTag ("creature").Length; i < Density; i++)
				CreateCreature ();

			var to_delete = 
				from x in GameObject.FindGameObjectsWithTag ("creature")
				where Vector3.Distance (x.transform.position, player.transform.position) > 15
				select x;

			foreach (var x in to_delete)
				GameObject.Destroy (x);

			yield return 0;
		}
	}
}