using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSnake : MonoBehaviour
{
    public int MoveSpeed = 2;
    public float SteerSpeed = 180;
    public GameObject MyPrefab;
    public int Insert = 10;
    private List<GameObject> namesOfDestroyedObjects = new List<GameObject>();
    private List<Vector3> positionSnake = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        MySnakePrefab();
        MySnakePrefab();
        MySnakePrefab();
        MySnakePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
        positionSnake.Insert(0, transform.position);
        int i = 0;
        foreach (var prefab in namesOfDestroyedObjects)
        {

            Vector3 position = positionSnake[Mathf.Min(i * Insert, positionSnake.Count - 1)];
            Vector3 positionforward = position - prefab.transform.position;
            prefab.transform.position += positionforward * MoveSpeed * Time.deltaTime;
            prefab.transform.LookAt(position);
            i++;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            MySnakePrefab();
            Instantiate(MyPrefab, new Vector3(Random.Range(-10.0f, 10.0f)));
        }
    }
    private void MySnakePrefab()
    {
        GameObject prefab = Instantiate(MyPrefab);
        namesOfDestroyedObjects.Add(prefab);
    }

}
