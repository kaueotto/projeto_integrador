using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class faseScene : MonoBehaviour {

	private SoundController soundController;
	private fade fade;

	public Text nomeFaseTxt;
	public Button btnJogar;

	[Header("configuracao das paginas")]
	public GameObject[] btnPaginacao;
	public GameObject[] painelFases;
	public bool ativarBtnPaginacao;
	private int idPagina;

	//---------------------------------------------------------------------------------
	void Start () {

		fade = FindObjectOfType (typeof(fade)) as fade;
		soundController = FindObjectOfType (typeof(SoundController)) as SoundController;

		onOffBotoesPaineis ();
	}

	//---------------------------------------------------------------------------------
	void onOffBotoesPaineis() 
	{

		btnJogar.interactable = false;

		foreach (GameObject p in painelFases)
		{
			p.SetActive (false);
		}

		painelFases [0].SetActive (true);

		if (painelFases.Length > 1) {
			ativarBtnPaginacao = true;
		} else
		{
			ativarBtnPaginacao = false;
		}

		foreach (GameObject b in btnPaginacao) 
		{
			b.SetActive (ativarBtnPaginacao);
		}
	}

	//---------------------------------------------------------------------------------
	public void jogar()
	{
		soundController.playButton ();
		soundController.audioMusic.clip = soundController.musicas [1];
		soundController.audioMusic.Play ();

		int idCena = PlayerPrefs.GetInt("idTema");
		if (idCena != 0)
		{
			StartCoroutine("transicao", idCena.ToString ());
			//SceneManager.LoadScene (idCena.ToString ());
		
		}
	}	

	public void btnPagina(int i)
	{
		soundController.playButton ();

		idPagina += i;
		if (idPagina < 0) {
			idPagina = painelFases.Length - 1;
		} else {
			if (idPagina >= painelFases.Length) {
				idPagina = 0;
		}

			btnJogar.interactable = false;
			nomeFaseTxt.color = Color.white;
			nomeFaseTxt.text = "Selecione uma fase!";


			foreach (GameObject p in painelFases)
			{
				p.SetActive (false);
			}

			painelFases [idPagina].SetActive (true);

		}
			
	}

	IEnumerator transicao(string nomeCena)
	{
		fade.fadeIn ();
		yield return new WaitWhile (() => fade.telaPreta.color.a < 0.9f);
		SceneManager.LoadScene(nomeCena);
	}
}