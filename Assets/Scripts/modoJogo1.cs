using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modoJogo1 : MonoBehaviour {

	[Header("configuracao dos textos")]
	public Text nomeTemaTxt;
	public Text perguntaTxt;
	public Image perguntaImg;
	public Text infoRespostasTxt;
	public Text notaFinalTxt;
	public Text msg1Txt;
	public Text msg2Txt;

	[Header("configuracao dos textos alternativa")]
	public Text altAtxt;
	public Text altBtxt;
	public Text altCtxt;
	public Text altDtxt;

	[Header("configuracao das barras")]
	public GameObject barraProgresso;
	public GameObject barraTempo;

	[Header("configuracao dos botoes")]
	public Button[] botoes;
	public Color corAcerto, corErro;

	[Header("configuracao do Modo de Jogo")]
	public bool perguntaComImg;
	public  bool perguntasAleatorias;
	public bool utilizarAlternativas;
	public bool jogarComtempo;
	public float tempoResponder;
	public  bool mostrarCorreta;
	public int qtdPiscar;

	[Header("configuracao das perguntas")]
	public string[] perguntas;
	public Sprite[] perguntasImg;
	public string[] correta;
	public int qtdPerguntas;
	public List<int> listaPerguntas;

	[Header("configuracao das alternativas")]
	public string[] alternativaA;
	public string[] alternativaB;
	public string[] alternativaC;
	public string[] alternativaD;

	[Header("configuracao dos paineis")]
	public GameObject[] paineis;
	public GameObject[] estrela;

	[Header("configuracao das mensagens")]
	public string[] mensagem1;
	public string[] mensagem2;
	public Color[] corMensagem;

	//-----------------------------------------------------------------

	private int idResponder, qtdAcertos, notaMin1Estrala, notaMin2Estralas, nEstrelas, idTema, idBtnCorreto;
	private float qtdRespondida, percProgresso, percTempo, notaFinal, valorQuestao,TempTime;
	private bool exibindoCorrreta, testeFinalizado;

	private SoundController soundController;

	void Start () {

		soundController = FindObjectOfType (typeof(SoundController)) as SoundController;

		idTema = PlayerPrefs.GetInt ("idTema");
		notaMin1Estrala = PlayerPrefs.GetInt ("notaMin1Estrala");
		notaMin2Estralas = PlayerPrefs.GetInt ("notaMin2Estralas");

		nomeTemaTxt.text = PlayerPrefs.GetString ("nomeTema");

		barraTempo.SetActive (false);

		if (perguntaComImg == true) 
		{
			montarListaPerguntaImg ();
		} 
		else {
			montarListaPergunta ();
		}

		progressoBarra ();
		controleBarraTempo ();



		paineis [0].SetActive (true);
		paineis [1].SetActive (false);

	}
	
	//---------------------------------------------------------------------------------
	void Update () {

		if (jogarComtempo == true && exibindoCorrreta == false && testeFinalizado == false)
		{
			TempTime += Time.deltaTime;
			controleBarraTempo ();

			if(TempTime >= tempoResponder){ proximaPergunta (); }
		}
	}

	//---------------------------------------------------------------------------------
	public void montarListaPergunta() // esta função é para mostrar a lista de perguntas a serem respondidas 
	{

		if (qtdPerguntas > perguntas.Length) { qtdPerguntas = perguntas.Length; } // faz validação da quantidade de perguntas para o teste em relação a qtd de perguntas existentes

		valorQuestao = 10 / (float)qtdPerguntas;

		if (perguntasAleatorias == true) // caso de modo de jogo com perguntas aleatorias
		{
			bool addPergunta = true;

				if (qtdPerguntas > perguntas.Length) { qtdPerguntas = perguntas.Length; }

			valorQuestao = 10 / (float)qtdPerguntas;

			while (listaPerguntas.Count < qtdPerguntas) {
				addPergunta = true;
				int rand = Random.Range (0, perguntas.Length);
			
				foreach (int idP in listaPerguntas) 
				{
					if (idP == rand)
					{
						addPergunta = false;
					}
				}
					
				if (addPergunta == true) {
					listaPerguntas.Add (rand);
				}
			}
		}// caso de modo de jogo com perguntas não aleatorias
		else 
		{
			for (int i = 0; i < qtdPerguntas; i++) 
			{
			listaPerguntas.Add (i);
			}
		}

			perguntaTxt.text = perguntas [listaPerguntas [idResponder]];

		if (utilizarAlternativas == true) 
		{
			altAtxt.text = alternativaA[listaPerguntas[idResponder]];
			altBtxt.text = alternativaB[listaPerguntas[idResponder]];
			altCtxt.text = alternativaC[listaPerguntas[idResponder]];
			altDtxt.text = alternativaD[listaPerguntas[idResponder]];
		}
	
	}

	//---------------------------------------------------------------------------------

	public void montarListaPerguntaImg() // monstar lista de perguntas com imagem
	{
		if (qtdPerguntas > perguntasImg.Length) // faz validação da quantidade de perguntas para o teste em relação a qtd de perguntas existentes
		{
			qtdPerguntas = perguntasImg.Length;
		}

		valorQuestao = 10 / (float)qtdPerguntas;

		if (perguntasAleatorias == true) // caso de modo de jogo com perguntas aleatorias
		{
			bool addPergunta = true;
		
			while (listaPerguntas.Count < qtdPerguntas) {
				addPergunta = true;
				int rand = Random.Range (0, perguntasImg.Length);

				foreach (int idP in listaPerguntas) 
				{
					if (idP == rand)
					{
						addPergunta = false;
					}
				}

				if (addPergunta == true) {
					listaPerguntas.Add (rand);
				}
			}
		}
		else // caso de modo de jogo com perguntas não aleatorias
		{
			for (int i = 0; i < qtdPerguntas; i++)
			{
				listaPerguntas.Add (i);
			}

		}

			perguntaImg.sprite = perguntasImg [listaPerguntas [idResponder]];
	
		if (utilizarAlternativas == true) 
		{
			altAtxt.text = alternativaA[listaPerguntas[idResponder]];
			altBtxt.text = alternativaB[listaPerguntas[idResponder]];
			altCtxt.text = alternativaC[listaPerguntas[idResponder]];
			altDtxt.text = alternativaD[listaPerguntas[idResponder]];
		}

	}

	//---------------------------------------------------------------------------------
	public void responder(string alternativa) // responsavel por processar a resposta do usuario
	{
		if (exibindoCorrreta == true) // verifica se no modo de jogo esta setado para exibir as alternativas corretas, caso esteja, ela retorna em caso de mais de um click
		{
			return;
		}

		qtdRespondida += 1;
		progressoBarra ();

		if (correta [listaPerguntas [idResponder]] == alternativa) { // verifica se esta errado ou certo na qtd de acerto
			qtdAcertos += 1;
			soundController.playAcerto ();
		} else {
			soundController.playErro ();
		}
			
		switch (correta [listaPerguntas[idResponder]]) //modo de jogo com exibição da correta, indica qual resposta esta correta
		{
		case "A":
			idBtnCorreto = 0;
			break;
		case "B":
			idBtnCorreto = 1;
			break;
		case "C":
			idBtnCorreto = 2;
			break;
		case "D":
			idBtnCorreto = 3;
			break;
		}

		if (mostrarCorreta == true) // caso esteja no modo de xibir a correta, altera a cor dos botoes e faz a chamada da animação
		{
			foreach (Button b in botoes) 
			{
				b.image.color = corErro;
			}
			exibindoCorrreta = true;

			botoes [idBtnCorreto].image.color = corAcerto;
			StartCoroutine ("mostrarAlternativaCorreta");
		} 
		else // caso não esteja para mostrar a correta, pula pra possima questao
		{
			exibindoCorrreta = true;
			StartCoroutine("aguardarProxima");
		}
	}

	//---------------------------------------------------------------------------------
	public void proximaPergunta() // responsavel para processar as perguntas, faz a chamada de uma nova pergunta ou encerra
	{
		idResponder += 1;
		TempTime = 0;

		if (idResponder < listaPerguntas.Count) {

			if (perguntaComImg == true) 
			{
				perguntaImg.sprite = perguntasImg [listaPerguntas [idResponder]];
			} 
			else {

				perguntaTxt.text = perguntas [listaPerguntas [idResponder]];
			}

			if (utilizarAlternativas == true) 
			{
				altAtxt.text = alternativaA[listaPerguntas[idResponder]];
				altBtxt.text = alternativaB[listaPerguntas[idResponder]];
				altCtxt.text = alternativaC[listaPerguntas[idResponder]];
				altDtxt.text = alternativaD[listaPerguntas[idResponder]];
			}

		} else { // caso o teste tenha sido finalizado ele ira calcular a nota final
			
			calcularNotaFinal ();
		}
	}

	//---------------------------------------------------------------------------------
	void progressoBarra() // controla a barra de progresso do jogo
	{
		infoRespostasTxt.text = "Respondeu " + qtdRespondida + " de " + listaPerguntas.Count + " perguntas";
		percProgresso = qtdRespondida / listaPerguntas.Count;
		barraProgresso.transform.localScale = new Vector3 (percProgresso, 1,1);
	}

	//---------------------------------------------------------------------------------
	void controleBarraTempo() // controla a barra de tempo caso esteja no modo contra o tempo
	{
		if (jogarComtempo == true) {barraTempo.SetActive (true);} 
		percTempo = ((TempTime - tempoResponder) / tempoResponder) * -1;
		if (percTempo < 0){ percTempo = 0; }
		barraTempo.transform.localScale = new Vector3 (percTempo, 1, 1);
	}

	//---------------------------------------------------------------------------------
	void calcularNotaFinal() // responsavel por calcular e gravar a nota 
	{

		testeFinalizado = true;

		notaFinal = Mathf.RoundToInt(valorQuestao * qtdAcertos);

		if(notaFinal > PlayerPrefs.GetInt("notaFinal_" + idTema.ToString()));
			{
		PlayerPrefs.SetInt ("notaFinal_" + idTema.ToString(), (int)notaFinal);
			}

		if (notaFinal == 10) 
		{
			nEstrelas = 3;
			soundController.playVinheta ();
		} 
		else if (notaFinal >= notaMin2Estralas) {
			nEstrelas = 2;
		}
		else if (notaFinal >= notaMin1Estrala){
			nEstrelas = 1;
		}

		notaFinalTxt.text = notaFinal.ToString ();
		notaFinalTxt.color = corMensagem [nEstrelas];

		msg1Txt.text = mensagem1 [nEstrelas];
		msg1Txt.color = corMensagem [nEstrelas];

		msg2Txt.text = mensagem2 [nEstrelas];

		foreach (GameObject e in estrela)
		{
			e.SetActive (false);
		}

		for (int i = 0; i < nEstrelas; i++ )
		{
			estrela[i].SetActive(true); 
		}

		paineis [0].SetActive (false);
		paineis [1].SetActive (true);
	}

	IEnumerator aguardarProxima ()
	{
		yield return new WaitForSeconds(1);
		exibindoCorrreta = false;
		proximaPergunta ();
	}

	IEnumerator mostrarAlternativaCorreta() //corroutine que faz a animação de iscar a alternativa correta
	{
		for (int i = 0; i < qtdPiscar; i++)
		{
			botoes [idBtnCorreto].image.color = corAcerto;
			yield return new WaitForSeconds (0.2f); //vai esperar 2 segundos
			botoes [idBtnCorreto].image.color = Color.white; // ai muda a cor original
			yield return new  WaitForSeconds (0.2f); // e volta esperar pra entrar no laço de novo
		}

		foreach (Button b in botoes) 
		{
			b.image.color = Color.white;
		}

		exibindoCorrreta = false;
		proximaPergunta ();
	}
}
