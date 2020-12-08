using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
	private int score = 0;

	void Start()
	{
		//初期スコア(0点)を表示
		GetComponent<Text>().text = "Score: " + score.ToString();
	}
	//ballScriptからSendMessageで呼ばれるスコア加算用メソッド
	public void AddPoint(int point)
	{
		score = score + point;
		GetComponent<Text>().text = "Score: " + score.ToString();
	}
}
