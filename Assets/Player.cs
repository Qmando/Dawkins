using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform body;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}

	Vector2 mouseOffset () {
		float x = Input.mousePosition.x - Screen.currentResolution.width/2;
		float y = Input.mousePosition.y - Screen.currentResolution.height/2;
		return new Vector2 (x, y);
	}

	void Move () {
		body.transform.Translate (mouseOffset () * speed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		Move ();
	}
}
