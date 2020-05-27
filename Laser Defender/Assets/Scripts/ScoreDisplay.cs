using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;


public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
