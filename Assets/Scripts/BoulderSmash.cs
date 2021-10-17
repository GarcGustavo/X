using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BoulderSmash : MonoBehaviour
{
    private GameObject boulderInstance;
    private Vector3 targetLocation;
    private Rigidbody body;

    public GameObject boulder;
    public float fallSpeed = 50f;
    public float speed = 10f;
    public bool kicked = false;

    void Start()
    {
        kicked = true;
        body = GetComponent<Rigidbody>();
        targetLocation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Debug.DrawRay(targetLocation, Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            targetLocation = rayHit.point;
        }

        //body.drag = targetLocation.magnitude;
    }

    void FixedUpdate()
    {
        StartCoroutine(kick(targetLocation));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
        }
    }

    private IEnumerator dropBoulder(Vector3 targetLocation)
    {
        //turn this into y-axis drop movement or use gravity
        yield return new WaitForSeconds(.3f);
    }

    private IEnumerator kick(Vector3 targetLocation)
    {
        if (kicked)
        {

            if(transform.position.y <= 1)
            {
                if (body.constraints != RigidbodyConstraints.FreezePositionY)
                {
                    body.constraints = RigidbodyConstraints.FreezePositionY;
                }

                //body.MovePosition(transform.position + (targetLocation.normalized * speed * Time.fixedDeltaTime));
                //body.AddForce((targetLocation - transform.position).normalized * speed * Time.fixedDeltaTime);
                body.position = Vector3.MoveTowards(transform.position, targetLocation - transform.position, speed * Time.fixedDeltaTime);


                Destroy(boulder, 2f);
            }
            else
            {
                //body.MovePosition(transform.position + (new Vector3(0, 1, 0) * -fallSpeed * Time.fixedDeltaTime));
                body.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetLocation.y, transform.position.z), fallSpeed * Time.fixedDeltaTime);
            }
        }

        yield return null;
    }
}
