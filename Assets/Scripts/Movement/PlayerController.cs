using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    
    public float playerSpeed;
    public float playerTurnSpeed;
    public float shootCD;

    public GameObject boulder;
    public Bullet bullet;
    //this needs to be turned on and off according to character or targeting mode
    private Rigidbody playerBody;
    private Vector3 playerDirection;
    private Vector3 targetLocation;
    private Vector3 lastInput;
    private Quaternion lastRotation;

    private bool aiming;
    private bool onCD;

    private Vector3 moveDirection = Vector3.zero;
    Animator anim;
    //Rigidbody body;
    Camera mainCamera;

    //need to replace movement rigid boy method for character controller method

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        onCD = false;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        MovePlayer();
        playerAim();
        Shoot();
    }

    private void castBoulder()
    {
        if (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetLocation = transform.position;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;

                if (Physics.Raycast(ray, out rayHit))
                {
                    targetLocation = rayHit.point;
                }

                GameObject boulderInstance = Instantiate(boulder, new Vector3(transform.position.x + (targetLocation.normalized.x), transform.position.y + 20f, transform.position.z + (targetLocation.normalized.z)), transform.rotation);
            }
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(1) && !onCD)
        {
            onCD = true;
            StartCoroutine(createBullets());
        }
    }

    private IEnumerator createBullets()
    {
        Bullet bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstance.SetDirection(playerDirection);
        yield return new WaitForSeconds(shootCD);
        onCD = false;
    }

    private void MovePlayer()
    {
        // Directional movement
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerBody.MovePosition(transform.position + (playerInput * playerSpeed * Time.fixedDeltaTime));
        }
    }

    private void playerAim()
    {
        // Mouse input targeting
        // Generate a ray from the cursor position
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var playerPlane = new Plane(Vector3.up, transform.position);
        var hitdist = 0f;

        // If the ray is parallel to the plane, Raycast will return false.

        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            targetLocation = ray.GetPoint(hitdist);
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            var targetRotation = Quaternion.LookRotation(targetLocation - transform.position);

            playerBody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, playerTurnSpeed * Time.deltaTime));

            Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)), ray.direction * 15);
        }

        playerDirection = targetLocation - transform.position;
    }

}
