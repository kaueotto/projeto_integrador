﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour {

	public GameObject painelTransicao;
	public Image telaPreta;

	public Color[] corTransicao;
	public float step; // determina a velociade da transição

	void Start () {
		
	}
 
	public void fadeIn () 
	{
		painelTransicao.SetActive (true);
		StartCoroutine ("fadeI");
	}

	public void fadeOut () 
	{
		StartCoroutine ("fadeO");
	}

	IEnumerator fadeI()
	{
		for (float i = 0; i <= 1; i += step) 
		{
			telaPreta.color = Color.Lerp (corTransicao [0], corTransicao [1], i);
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator fadeO()
	{
		for (float i = 0; i <= 1; i += step) 
		{
			telaPreta.color = Color.Lerp (corTransicao [1], corTransicao [0], i);
			yield return new WaitForEndOfFrame ();
		}

		painelTransicao.SetActive (false);
	}

}