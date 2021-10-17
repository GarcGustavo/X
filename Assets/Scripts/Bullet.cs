using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public float speed;
    public bool pattern;
    private Rigidbody body;
    private Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        //targetLocation = new Vector3();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //body.AddForce((targetLocation - transform.position).normalized * speed * Time.fixedDeltaTime);
        body.MovePosition(transform.position + (targetLocation.normalized * speed * Time.fixedDeltaTime));
        //body.transform.LookAt(targetLocation);
    }

    public void SetDirection(Vector3 direction)
    {
        targetLocation = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Enemy enemyInstance = collision.collider.GetComponent<Enemy>();
            //enemyInstance.Damage(10f);
            //Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Player"))
        {
            //Destroy(gameObject);
        }
        
        //Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyInstance = other.GetComponent<Enemy>();
            enemyInstance.Damage(10f);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, 3f);
    }

}
