using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Transform follow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = follow.position + new Vector3 (0, 0, -10);
	}
}
