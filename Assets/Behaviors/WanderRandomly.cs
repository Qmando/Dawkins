using UnityEngine;
using System.Linq;
using System.Collections;

public class WanderRandomly : MonoBehaviour
{
	public float maxVelocity = 1.0f;
	public float StepDistance = 20.0f; 
	public float MaxTurnAngle = 60.0f; 
	public bool randomDistance = true;
	public float MaxDistance = 60f;

	public IEnumerator Start() { 
		var cur = transform.position;


		while (true) { 
			cur = transform.position;
			Vector3 cellPos = GameObject.Find("Cell").transform.position;
			// infinite loop : keep setting a new goal StepDistance away from current position
			Vector3 initialDirection;
			Debug.Log (Vector3.Distance (cur, cellPos));
			// Chance to attack!
			if (Random.value < .2f) {
				initialDirection = cellPos - cur;
			}
			else {
				initialDirection = new Vector3 (Random.value, Random.value, 0);
			}
			float distance = StepDistance;
			if (randomDistance) {
				distance = (Random.value + .5f) * StepDistance;
			}
			var goal = GameObject.Find("Cell").transform.position + distance * initialDirection;

			var nextSwitch = Time.time + distance;

			// pause for a bit to simulate decisionmaking at each step	
			yield return new WaitForSeconds (0.3f);

			// every frame, incrementally move towards current goal for period of time
			while (Time.time < nextSwitch) { 

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