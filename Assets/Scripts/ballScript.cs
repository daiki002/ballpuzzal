using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ballScript : MonoBehaviour
{
	public GameObject ballPrefab;
	public Sprite[] ballSprites;

	private GameObject firstBall; //最初にドラッグしたボール
	private GameObject lastBall; //最後にドラッグしたボール
	private string currentName; //名前判定用のstring変数

	List<GameObject> removableBallList = new List<GameObject>();

	public GameObject scoreGUI;
	private int point = 100;

	BtnMg BtnMg;

	public GameObject exchangeButton;

	public bool isPlaying = true;

	bool isStartGame;

	[SerializeField] GameObject panel;


	// Start is called before the first frame update

	void Start()
	{
		StartCoroutine(DropBall(50));
		isStartGame = false;
	}

	void startgame()
    {
        if (isStartGame)
        {
		}
	}

	public void SetActivefalse()
	{
		panel.SetActive(false);
		isStartGame = true;
	}

	

	void Update()
	{
		if (isPlaying)
		{
			if (Input.GetMouseButtonDown(0) && firstBall == null)
			{
				OnDragStart();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				OnDragEnd();
			}
			else if (firstBall != null)
			{
				OnDragging();
			}
		}
	}

	private void OnDragStart()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (hit.collider != null)
		{
			GameObject hitObj = hit.collider.gameObject;
			string ballName = hitObj.name;
			if (ballName.StartsWith("Ball"))
			{
				firstBall = hitObj;
				lastBall = hitObj;
				currentName = hitObj.name;
				removableBallList = new List<GameObject>();
				PushToList(hitObj);
			}
		}
	}

	private void OnDragging()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null)
		{
			GameObject hitObj = hit.collider.gameObject;

			//同じブロックをクリックしている時
			if (hitObj.name == currentName && lastBall != hitObj)
			{
				float distance = Vector2.Distance(hitObj.transform.position, lastBall.transform.position);
				if (distance < 1.0f)
				{
					//削除対象のオブジェクトを格納
					lastBall = hitObj;
					PushToList(hitObj);
				}
			}
		}
	}

	private void OnDragEnd()
	{
		int remove_cnt = removableBallList.Count;
		if (remove_cnt >= 3)
		{
			for (int i = 0; i < remove_cnt; i++)
			{
				Destroy(removableBallList[i]);
			}
			//remove_cnt*100だけスコアの加点
			scoreGUI.SendMessage("AddPoint", point * remove_cnt);

			StartCoroutine(DropBall(remove_cnt));
		}
		else
		{
			for (int i = 0; i < remove_cnt; i++)
			{
				ChangeColor(removableBallList[i], 1.0f);
			}
		}
		firstBall = null;
		lastBall = null;
	}

	IEnumerator DropBall(int count)
	{
		//********** 追記 **********//
		if (count == 50)
		{
			StartCoroutine("RestrictPush");
		}
		//********** 追記 **********//
		for (int i = 0; i < count; i++)
		{
			Vector2 pos = new Vector2(Random.Range(-0.3f, 0.3f), 1.5f);
			GameObject ball = Instantiate(ballPrefab, pos,
				Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;
			int spriteId = Random.Range(0, 5);
			ball.name = "Ball" + spriteId;
			SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
			spriteObj.sprite = ballSprites[spriteId];
			yield return new WaitForSeconds(0.05f);
		}
	}

	//********** 追記 **********//
	IEnumerator RestrictPush()
	{
		exchangeButton.GetComponent<Button>().interactable = false;
		yield return new WaitForSeconds(5.0f);
		exchangeButton.GetComponent<Button>().interactable = true;
	}
	//********** 追記 **********//

	void PushToList(GameObject obj)
	{
		removableBallList.Add(obj);
		ChangeColor(obj, 0.5f);
	}

	void ChangeColor(GameObject obj, float transparency)
	{
		SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>();
		ballTexture.color = new Color(ballTexture.color.r, ballTexture.color.g, ballTexture.color.b, transparency);
	}


}