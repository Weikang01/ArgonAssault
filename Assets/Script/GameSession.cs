using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    public float playerScore = 0f;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = playerScore.ToString();
	}
}
