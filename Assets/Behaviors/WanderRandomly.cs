using System;
using UnityEngine;
using System.Linq;
using System.Collections;

public class WanderRandomly : MonoBehaviour
{
	public float maxVelocity = 1.0f;
	public float StepDistance = 10.0f; 
	public float MaxTurnAngle = 60.0f; 

	public IEnumerator Start() { 
		var cur = gameObject.transform.position;

		while (true) { 
			// infinite loop : keep setting a new goal StepDistance away from current position
			var goal = cur + StepDistance * new Vector3 (
				           UnityEngine.Random.value,
				           UnityEngine.Random.value, 0);

			// every frame, incrementally move towards current goal until 'close enough'
			while (Vector3.Distance (cur, goal) > 0.01f) { 
				var magnitude = maxVelocity * Time.deltaTime;
				magnitude = Math.Min (magnitude, Vector3.Distance (cur, goal));
				var direction = (goal - cur).normalized * magnitude;
				gameObject.transform.Translate (direction);
				cur = gameObject.transform.position;
				yield return 0;
			}			
		}
	}
}