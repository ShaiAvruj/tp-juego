using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSnake : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public GameObject MyPrefab;
    public float Insert;
    private List<GameObject> namesOfDestroyedObjects = new List<GameObject>();
    private List<Vector3> positionSnake = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        MySnakePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
        positionSnake.Insert(0, transform.position);
        foreach (GameObject prefab in namesOfDestroyedObjects)
        {
            int i = 0;
            Vector3 position = positionSnake[Mathf.Min(i)]
            i++;
        }

    }
    private void MySnakePrefab()
    {
        GameObject prefab = Instantiate(MyPrefab);
        namesOfDestroyedObjects.Add(prefab);
    }
   
}
