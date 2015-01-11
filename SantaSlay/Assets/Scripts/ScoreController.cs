using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreController : MonoBehaviour {
	
	public Text elfText;
	public Text timerText;
	public Text notificationText;
	public AudioClip gainPointSound;
	public AudioClip losePointSound;
	public MyCharacterController character;

	private int score;
	private float counter;
	private bool gameOver = false;
	
	// Use this for initialization
	void Start () {
		score = 0;
		counter = 1050;
		elfText.text = "Score: " + score;
		timerText.text = "Time Until Christmas: " + (int) counter;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver){
			if (counter > 0) {
				counter -= Time.deltaTime;
				timerText.text = "Time Until Christmas: " + (int) counter;
			} else {
				gameOver = true;
				if(score > 50){
					timerText.text = "";
					elfText.text = "";
					notificationText.text = "GAMEOVER\nScore: " + score + "\nYOU WIN!!";
				}else{
					timerText.text = "";
					elfText.text = "";
					notificationText.text = "GAMEOVER\nScore: " + score + "\nYOU LOSE :(";
				}
				character.gameOver();
			}
		}
		
	}
	
	public void addScore (int x)
	{
		if(!gameOver){
			score += x;
			elfText.text = "Score: " + score;
			if (x < 0) 
			{
				AudioSource.PlayClipAtPoint (losePointSound, transform.position, 0.5f);
			} 
			else if (x > 0) 
			{
				AudioSource.PlayClipAtPoint (gainPointSound, transform.position, 1.5f);
			}
		}
	}
}
