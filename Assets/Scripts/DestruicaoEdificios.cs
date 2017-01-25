using UnityEngine;
using System.Collections;

public class DestruicaoEdificios : MonoBehaviour {

    private GameObject particulaEscombro;
    private bool controlo = true;

    public GameObject[] listaParticulas;
    public GameObject objectoEscombro;
    public int numeroParticulas = 20;
    public float dispercaoParticulas = 0.3f;
    public float tamanhoMinimo = 0.5f;
    public float tamanhoMaximo = 2;
    public float multiplicadorForca = 5;

	void Update () {
        /*if (Time.time > 2 && controlo==true) {
            Destruir();
            controlo = false;
        }*/
	}

    public void Destruir () {
        Invoke("EsperaExplosao", 0.1f);

        //Criação de Escombros
        for (int i = 0; i < numeroParticulas; i++) {
            particulaEscombro = listaParticulas[Random.Range(0, listaParticulas.Length)];

            Vector3 ajustePosicional = new Vector3(Random.Range(-dispercaoParticulas, dispercaoParticulas), Random.Range(0.1f, 0.1f+dispercaoParticulas), Random.Range(-dispercaoParticulas, dispercaoParticulas));
            GameObject instancia = Instantiate(particulaEscombro, this.transform.position + ajustePosicional, Quaternion.identity) as GameObject;

            instancia.transform.localRotation = Quaternion.Euler(Random.Range(1, 90), Random.Range(1, 90), Random.Range(1, 90));

            float tamanho = Random.Range(tamanhoMinimo, tamanhoMaximo);
            instancia.transform.localScale = new Vector3(tamanho, tamanho, tamanho);

            instancia.GetComponent<Rigidbody>().AddForce(ajustePosicional * multiplicadorForca, ForceMode.Impulse);
        }
        Instantiate(objectoEscombro, this.transform.position, Quaternion.Euler(this.transform.eulerAngles + new Vector3(90,0,0)));
        ScoreImpact.scoreImpact.Score();
    }

    private void EsperaExplosao() {
        Destroy(this.gameObject);
    }
}
