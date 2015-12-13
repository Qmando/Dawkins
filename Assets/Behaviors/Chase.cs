using System.Collections.Generic;
using UnityEngine;

public class Chase : WanderRandomly { 

	public float ChaseProbability = 0.2f;

	public override IEnumerable<Vector3> Goal() { 
		while (true) { 
			var goal = GameObject.Find("Cell").transform.position;
			if (Random.value < ChaseProbability)
				yield return (goal - transform.position).normalized * RandomStepDistance;
			else
				yield return base.Goal ().GetEnumerator ().Current;
		}
	}
}
