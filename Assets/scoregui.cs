using UnityEngine;
using System.Collections;

public class scoregui : MonoBehaviour {
	public int score = 0;
	public GameObject cell;

	string genTopText(int score){
		string text = "KILLS -";
		string score_str = score.ToString ();
		text += score_str;
		return text;
	}

	void OnGUI() {
		// Black background
		Color black = new Color(0, 0, 0);
		Texture2D black_tex = new Texture2D(1, 1);
		black_tex.SetPixel(0, 0, black);
		black_tex.Apply();

		// Top line
		GUI.skin.label.normal.background = black_tex;
		GUI.skin.label.fontSize = 40;
		string top_text = genTopText (score);
		GUI.Label (new Rect (10, 10, 200, 70), top_text);


	}


	void setscore() {
		score += 1;
	}
}