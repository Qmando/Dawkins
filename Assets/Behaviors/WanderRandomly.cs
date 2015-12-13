using UnityEngine;
using System.Linq;
using System.Collections;

public class WanderRandomly : MonoBehaviour
{
	public float maxVelocity = 1.0f;
	public float StepDistance = 10.0f; 
	public float MaxTurnAngle = 60.0f; 
	public bool randomDistance = true;

	public IEnumerator Start() { 
		var cur = transform.position;

		while (true) { 
			// infinite loop : keep setting a new goal StepDistance away from current position
			var initialDirection = new Vector3 (Random.value, Random.value, 0);
			float distance = StepDistance;
			if (randomDistance) {
				distance = Random.value * StepDistance;
			}
			var goal = cur + distance * initialDirection;

			// pause for a bit to simulate decisionmaking at each step	
			yield return new WaitForSeconds (0.3f);

			// every frame, incrementally move towards current goal until 'close enough'
			while (Vector3.Distance (cur, goal) > 0.01f) { 

				// rotate current orientation to point towards goal
				var dir = Vector3.RotateTowards(transform.up, initialDirection, Time.deltaTime, 0.0f);
				// transform.rotation = Quaternion.LookRotation (initialDirection);
				var angle = Vector3.Angle(transform.up, dir);
				transform.RotateAround(transform.position, Vector3.forward, angle);

				// now that we're facing the correct way, in theory we just move forward locally
				var magnitude = maxVelocity * Time.deltaTime;
				magnitude = System.Math.Min (magnitude, Vector3.Distance (cur, goal));
				// var direction = (goal - cur).normalized * magnitude;
				// Debug.LogWarning(transform.position);
				transform.Translate (Vector3.up * magnitude);
				cur = transform.position;
				yield return 0;
			}			
		}
	}
}