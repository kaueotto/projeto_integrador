using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioSource audioMusic, audioFX;
	public AudioClip somAcerto, somErro,somBotao, vinheta3estrelas;
	public AudioClip[] musicas;
	//----------------------------------------------------------------------------------

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	//---------------------------------------------------------------------------------
	void Start () 
	{	
		carregarPreferencias ();
		audioMusic.clip = musicas [0];
		audioMusic.Play ();
	}
	
	//---------------------------------------------------------------------------------

	public void playAcerto () 
	{
		audioFX.clip = somAcerto;
		audioFX.Play();
	}

	//---------------------------------------------------------------------------------

	public void playErro () 
	{
		audioFX.clip = somErro;
		audioFX.Play();
	}

	//---------------------------------------------------------------------------------
	public void playButton()
	{
		audioFX.clip = somBotao;
		audioFX.Play();
	}

	public void playVinheta()
	{
		audioFX.clip = vinheta3estrelas;
		audioFX.Play();
	}

	//---------------------------------------------------------------------------------

	void carregarPreferencias()
	{
		if (PlayerPrefs.GetInt ("valoresDefault") == 0) // verifica se ha registro dos valores iniciais de configuração, se nao tiver grava os valores iniciais
		{
			PlayerPrefs.SetInt ("valoresDefault", 1);
			PlayerPrefs.SetInt ("onOffMusica", 1);
			PlayerPrefs.SetInt ("onOffEfetios", 1);
			PlayerPrefs.SetFloat ("volumeMusica", 1);
			PlayerPrefs.SetFloat ("volumeEfeitos", 1);
		}

		int onOffMusica = PlayerPrefs.GetInt ("onOffMusica"); // carrega os valores de configuração de volume e musica
		int onOffEfetios = PlayerPrefs.GetInt ("onOffEfetios");
		float volumeMusica = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		bool tocarMusica = false;
		bool tocarEfeitos = false;

		if (onOffMusica == 1) {
			tocarMusica = true;
		}

		if (onOffEfetios == 1) {
			tocarEfeitos = true;
		}

		audioMusic.mute = !tocarMusica;
		audioFX.mute = !tocarEfeitos;

		audioMusic.volume = volumeMusica;
		audioFX.volume = volumeEfeitos;

	}
}
