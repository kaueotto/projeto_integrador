using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class faseInfo : MonoBehaviour {

	private SoundController soundController;
	private faseScene faseScene;

	[Header("configuracao da fase")]
	public int idTema;
	public string nomeTema;
	public Color corTema;
	public bool requerNotaMinima;
	public int notaMinimaNessesaria;

	[Header("configuracao das estrelas")]
	public int notaMin1Estrala;
	public int notaMin2Estralas;

	[Header("configuracao do botao")]
	public Text idTemaTxt;
	public GameObject[] estrela;

	private int notaFinal;
	private Button btnTema;

	//---------------------------------------------------------------------------------
	void Start () {

		soundController = FindObjectOfType (typeof(SoundController)) as SoundController;
		notaFinal = PlayerPrefs.GetInt ("notaFinal_" + idTema.ToString ());
		faseScene = FindObjectOfType (typeof(faseScene)) as faseScene;

		idTemaTxt.text = idTema.ToString ();

		estrelas(); // chama o metodo estrela para ver quantas estrelas ganhamos

		btnTema = GetComponent<Button> ();

		verificarNotaMinima();
	}

	void verificarNotaMinima()
	{
		btnTema.interactable = false;
		if (requerNotaMinima == true) 
		{
			int notaTemaAnterior = PlayerPrefs.GetInt ("notaFinal_" + (idTema - 1).ToString ());
			if (notaTemaAnterior >= notaMinimaNessesaria)
			{
				btnTema.interactable = true;
			}
		} 
		else {
			btnTema.interactable = true;
		}
	}

	//---------------------------------------------------------------------------------
	public void selecionarFase()
	{
		soundController.playButton ();

		faseScene.nomeFaseTxt.text = nomeTema;
		faseScene.nomeFaseTxt.color = corTema;

		PlayerPrefs.SetInt ("idTema", idTema);
		PlayerPrefs.SetString ("nomeTema", nomeTema);
		PlayerPrefs.SetInt ("notaMin1Estrala", notaMin1Estrala);
		PlayerPrefs.SetInt ("notaMin2Estralas", notaMin2Estralas);

		faseScene.btnJogar.interactable = true;
	}

	//---------------------------------------------------------------------------------
	public void estrelas()
	{
		foreach (GameObject e in estrela)
		{
			e.SetActive (false);
		}

		int nEstrelas = 0;

		if (notaFinal == 10) {
			nEstrelas = 3;
		} else if (notaFinal >= notaMin2Estralas) {
			nEstrelas = 2;
		}
		else if (notaFinal >= notaMin1Estrala){
			nEstrelas = 1;
		}


		for (int i = 0; i < nEstrelas; i++ )
		{
			estrela[i].SetActive(true); 
		}

	}

}
