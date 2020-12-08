using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeScript : MonoBehaviour
{
	private float time = 60;
	public ballScript BallScript;
	//********** 追記 **********// 
	public GameObject exchangeButton;
	public GameObject gameOverText;
	//********** 追記 **********// 

	void Start()
	{
		//********** 追記 **********// 
		gameOverText.SetActive(false);
		//********** 追記 **********// 
		GetComponent<Text>().text = ((int)time).ToString();
	}

	void Update()
	{
		time -= Time.deltaTime;
		//********** 追記 **********// 
		if (time < 0)
		{
			StartCoroutine("GameOver");
		}
		//********** 追記 **********// 
		if (time < 0) time = 0;
		GetComponent<Text>().text = ((int)time).ToString();
	}
	//********** 追記 **********// 
	IEnumerator GameOver()
	{
		gameOverText.SetActive(true);
		exchangeButton.GetComponent<Button>().interactable = false;
		BallScript.isPlaying = false;
		yield return new WaitForSeconds(2.0f);
		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("Titel");
		}
	}
}

