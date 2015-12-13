using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

class Wiggle : MonoBehaviour {

	public float magnitude = 0.4f;
	public float timeOffset = 1.0f;
	public float timeFactor = 4.0f;

	Vector3 offset;
	Dictionary<Transform, Vector3> a = new Dictionary<Transform, Vector3>();

	public IEnumerator Start() {
		while (true) {
			wobble (transform);
            yield return 0;
		}
	}

	void wobble(Transform transform, float time = 0.0f, int depth=0) {

		if (depth > 1)
			return;
		
		if (!a.ContainsKey (transform))
			a [transform] = transform.position;

		if (depth == 1)
		transform.localPosition += Time.deltaTime * magnitude * new Vector3 (
			(float)Math.Sin (timeFactor * Time.time + time),
			(float)Math.Cos (timeFactor * Time.time + time),
			0);

		foreach (Transform child in transform) {
			wobble (child, time + timeOffset, depth+1);
			time += timeOffset;
		}
	}
}
