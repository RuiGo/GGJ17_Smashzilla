using UnityEngine;
using System.Collections;

public class ComportamentoParticula : MonoBehaviour {

    private float inicioDesaparecer = 5;
    private float inicioTemporizador;
    private float opacidade = 0;

    private Material material;
    private Color cor;

    void Start () {
	    material = GetComponent<Renderer>().material;
        cor = material.color;

        inicioTemporizador = Time.time;
    }	
	
	void Update () {
        if (Time.time - inicioTemporizador >= inicioDesaparecer) {
            material.color = new Color(cor.r, cor.g, cor.b, cor.a - opacidade);
            opacidade += 0.02f;

            if (opacidade >= 1) {
                Destroy(this.gameObject);
            }
        }    
    }
}
