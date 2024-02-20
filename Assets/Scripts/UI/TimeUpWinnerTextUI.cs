using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpWinnerTextUI : MonoBehaviour
{
	MyFirstARGame.GlobalClientState globalObj;
	Text timeUpText;

	void Start()
	{
		GameObject globalState = GameObject.Find("GlobalState");
		globalObj = globalState.GetComponent<MyFirstARGame.GlobalClientState>();
		timeUpText = gameObject.GetComponent<Text>();
	}

	void Update()
	{
		timeUpText.text = globalObj.gameOverText;
	}

}
