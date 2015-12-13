using UnityEngine;
using System.Linq;
using System.Collections;

public class DoodadSpawner : MonoBehaviour
{
	/// The current Player the Spawner is spawning creatures for
	public GameObject player { get; set; }
	public string doodadTag = "component";

	public float SpawnRadius = 40.0f;
	// The max bounds of the spawning area
	public Rect SpawnArea { get; set; }
	// The minimum distance to spawn from -- this prevents spawning too closely
	public Rect SpawnClearArea { get; set; }

	public int Density = 20;
	public GameObject[] InstanceTypes;

	void Start() {
		var w = SpawnRadius;
		SpawnArea = new Rect (-w / 2, -w / 2, w, w);
		SpawnClearArea = new Rect (SpawnArea.xMin / 2, SpawnArea.yMin / 2, SpawnArea.width / 2, SpawnArea.height / 2);
		player = GameObject.Find ("Cell");
		StartCoroutine(dostuff());
	}

	GameObject CreateCreature(bool avoidNearby = true) {
		var creature = Instantiate (InstanceTypes [Random.Range(0,InstanceTypes.Length)]);
		creature.tag = doodadTag;
		creature.transform.Rotate (Vector3.forward, UnityEngine.Random.value  * 360);
		creature.transform.position = player.transform.position + genOffset (avoidNearby);
		return creature;
	}

	Vector3 genOffset(bool avoidNearby) {
		Vector3 v = Vector3.zero;
		do {
			v = new Vector3 (
				Random.value * (float)SpawnArea.width + SpawnArea.xMin,
				Random.value * (float)SpawnArea.height + SpawnArea.yMin,
				0);
		} while(avoidNearby && SpawnClearArea.Contains (v)) ;
		return v;
	}
	IEnumerator dostuff() {
		// start by initially populating the space around the current Player
		for (int i = 0; i < Density; i++)
			CreateCreature (false);

		while (true) {
			for (int i = GameObject.FindGameObjectsWithTag (doodadTag).Length; i < Density; i++)
				CreateCreature ();

			var to_delete =
				from x in GameObject.FindGameObjectsWithTag (doodadTag)
				where Vector3.Distance (x.transform.position, player.transform.position) > this.SpawnRadius
				select x;

			foreach (var x in to_delete)
				GameObject.Destroy (x);

			yield return 0;
		}
	}
}
