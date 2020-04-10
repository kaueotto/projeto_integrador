using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnComandos : MonoBehaviour {

	private fade fade;
	private SoundController soundController;

	void Start()
	{
		soundController = FindObjectOfType (typeof(SoundController)) as SoundController;
		fade = FindObjectOfType (typeof(fade)) as fade;
	}

	//---------------------------------------------------------------------------------
	public void irParaCena(string nomeCena) // carrega cena
	{
		soundController.playButton();

		if (SceneManager.GetActiveScene ().name != "titulo" && SceneManager.GetActiveScene ().name != "fases") // evita mudar a musica quando eu tiver do titulo pra fase ou ao contrario 
		{
			soundController.audioMusic.clip = soundController.musicas [0]; 
			soundController.audioMusic.Play ();
		}

		StartCoroutine ("transicao", nomeCena);
	}

	//---------------------------------------------------------------------------------
	public void sair() // fecha o jogo
	{
		Application.Quit ();	
	}

	//---------------------------------------------------------------------------------
	public void jogarNovamente ()
	{
		soundController.playButton();
		int idCena = PlayerPrefs.GetInt("idTema");
		if (idCena != 0)
		{
			SceneManager.LoadScene (idCena.ToString ());
		}
	}

	IEnumerator transicao(string nomeCena)
	{
		fade.fadeIn ();
		yield return new WaitWhile (() => fade.telaPreta.color.a < 0.9f);
		SceneManager.LoadScene(nomeCena);
	}
}