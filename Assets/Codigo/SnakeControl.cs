using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeControl : MonoBehaviour
{
    public float moveSpeed = 5;
    public float steerSpeed = 180;
    public int Gap;
    public GameObject GOText;
    public GameObject botonRepetir;
    public int Score;
    public Text Puntuacion;
    public GameObject CuerpoPrefab;
    public AudioSource Monedita;
    public AudioSource Muerte;
    List<GameObject> Cuerpos = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();
    public Text Tiempo;
    private void Start()
    {
        GOText.SetActive(false);
        botonRepetir.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Tiempo.text = ""+ Mathf.Floor(Time.time);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        PositionHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in Cuerpos)
        {
            Vector3 point = PositionHistory[Mathf.Clamp(index * Gap, 0, PositionHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;

            body.transform.LookAt(point);
            index++;
        }
        Puntuacion.text = Score.ToString();
    }

    void GrowSnake()
    {
        GameObject Cuerpo = Instantiate(CuerpoPrefab);
        Cuerpos.Add(Cuerpo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Comida")
        {
            Monedita.Play();
            GrowSnake();
            Destroy(other.gameObject);
            Score++;
        }
        if(other.gameObject.tag == "Pared")
        {
            Muerte.Play();
            GOText.SetActive(true);
            Time.timeScale = 0;
            botonRepetir.SetActive(true);
        }
    }
    public void Repetir()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
