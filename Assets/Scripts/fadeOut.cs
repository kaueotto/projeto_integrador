using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour {

	public fade fade;

	void Start () {

		fade = FindObjectOfType (typeof(fade)) as fade;
		fade.fadeOut ();
	}

}
