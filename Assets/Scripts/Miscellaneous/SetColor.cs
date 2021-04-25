using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour 
{
	public Color col;	
	
	// Update is called once per frame
	void Awake () 
	{
		gameObject.GetComponent<Renderer>().material.color = col;
       // gameObject.GetComponent<Renderer>().material.shader = this.GetComponent<Shader>();
        gameObject.GetComponent<Renderer>().material.SetColor("_Tint", col);
    }
}
