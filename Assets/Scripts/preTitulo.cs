using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preTitulo : MonoBehaviour {

	private fade fade;

	public int tempoEspera;

	void Start () {

		fade = FindObjectOfType (typeof(fade)) as fade;

		StartCoroutine ("esperar"); 
	}
	
	IEnumerator esperar()
	{
		yield return new WaitForSeconds (tempoEspera);
		fade.fadeIn();

		yield return new WaitWhile (() => fade.telaPreta.color.a < 0.9f); // faz esperar a transição, quando passar do 9 ele va para a cena titulo

		SceneManager.LoadScene ("titulo");
	}
}
