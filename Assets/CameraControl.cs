using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public Transform follow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = follow.position + new Vector3 (0, 0, -15);
	}

	void zoomOut() {
		this.GetComponent<Camera> ().orthographicSize += 2;
	}
}
