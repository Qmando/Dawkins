using UnityEngine;
using System.Collections;

public class bodycell : Component {



	void attach (GameObject x) {
		GameObject.Find ("Camera").SendMessage ("zoomOut");
		base.attach (x);
	}

	void addSpeed () {
		this.attachedTo.SendMessage ("addSpeed");
	}

	void decreaseSpeed () {
		this.attachedTo.SendMessage ("decreaseSpeed");
	}
		
}
