using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WanderRandomly : MonoBehaviour
{
	public float maxVelocity = 1.0f;
	public float StepDistance = 20.0f;
	public float MaxTurnAngle = 60.0f;
	public bool randomDistance = true;
	public float MaxDistance = 60f;

	public float RandomStepDistance { 
		get { 
			return randomDistance ? (Random.value + 0.5f) * StepDistance : StepDistance; 
		} 
	}

	public virtual System.Collections.Generic.IEnumerable<Vector3> Goal() {
		while (true)
			yield return transform.position + RandomStepDistance * new Vector3 (Random.value - 0.5f, Random.value - 0.5f, 0);
	}

	/// Given a goal, plot a sequence of intermediate steps to get there.
	/// This possibly results in an infinite sequence if we are physically unable to reach the goal.
	public IEnumerable<Vector3> getSteps(Vector3 goal) {
		var cur = transform.position;
		while(Vector3.Distance (cur, goal) > 0.01f) { 
			transform.rotation = Quaternion.RotateTowards (
				transform.rotation, 
				Quaternion.LookRotation (Vector3.forward, (goal - cur).normalized),
				200 * Time.deltaTime);
			var magnitude = maxVelocity * Time.deltaTime;
			magnitude = System.Math.Min (magnitude, Vector3.Distance (cur, goal));
			cur = transform.position += (goal - cur).normalized * magnitude;
			yield return cur;
		}
	}

	public IEnumerator Start () {
		foreach (var goal in Goal()) { 
			// pause for a bit to simulate decisionmaking at each step
			yield return new WaitForSeconds (0.3f);
			// Since we're not guaranteed of ever reaching our goal, give up after getting stuck for 300 to 500 steps
			foreach (var step in getSteps(goal).Take(Random.Range(300,500)))
				yield return 0;
		}
	}
}
