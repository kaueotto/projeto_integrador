using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffset : MonoBehaviour {

	private Material material;
	public float velocidadeX, velocidadeY;
	public float incremento;
	private float offset;

	//---------------------------------------------------------------------------------
	void Start () {

		material = GetComponent<Renderer> ().material;
	}
	
	//---------------------------------------------------------------------------------
	void Update () {

		offset += incremento;
		material.SetTextureOffset ("_MainTex", new Vector2 (offset * velocidadeX, offset * velocidadeY));
	}
}
