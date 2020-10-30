using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBench : MonoBehaviour
{

    public LineRenderer frontSling;
    public LineRenderer backSling;
    private bool dragging;
    private SpringJoint2D springJoint;
    private Rigidbody2D componentRigidBody;
    private float releaseDelay;
    private GameObject tempObject;
    private Camera mainCamera;

    public List<GameObject> listOfObjects;

    // Start is called before the first frame update
    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        releaseDelay = 1 / (springJoint.frequency * 4);
        mainCamera = Camera.main;
        componentRigidBody = GetComponent<Rigidbody2D>();
    }

    public void OnMouseUp()
    {
        dragging = false;
        componentRigidBody.isKinematic = false;
        // Fazer rotina para soltar o passaro
        StartCoroutine(Release());
    }

    public void OnMouseDown()
    {
        dragging = true;
        componentRigidBody.isKinematic = true;
        // Criar clone do passarinho
        
        tempObject = Instantiate<GameObject>(listOfObjects[Random.Range(0, listOfObjects.Count)]);
        // Colocar a transform do objeto criado como a desse GameObject
        tempObject.transform.position = transform.position;
        tempObject.GetComponent<Rigidbody2D>().isKinematic = true;
        tempObject.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            componentRigidBody.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        frontSling.SetPosition(1, transform.position);
        backSling.SetPosition(1, transform.position);
    }

    // Criar rotina de soltar o passarinho
    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseDelay);
        Rigidbody2D springBody = springJoint.GetComponent<Rigidbody2D>();
        if (tempObject != null)
        {
            Rigidbody2D objectBody = tempObject.GetComponent<Rigidbody2D>();
            objectBody.isKinematic = false;
            objectBody.velocity = springBody.velocity;
            tempObject.transform.SetParent(null);
            
            tempObject = null;
        }

        springBody.velocity = Vector3.zero;
    }
}
