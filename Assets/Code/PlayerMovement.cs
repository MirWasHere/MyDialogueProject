using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // Setting rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Creating a vector with the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // If not moving, stop rotating; keep rotation the same
        if(movement == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        // Setting the rotation
        rb.MoveRotation(targetRotation);


    }


    // Player movement. 
    /*
     DEVELOPMENT NOTES:
        [5/28]: Created the movement method, which moves the player, without rotating them to face the direction noted.
        [5/29]: Movement faces the player in the direction of movement
    */
    void Movement()
    {
         // Right
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.forward *  speed * Time.deltaTime);

           
        }

        // Left
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }

        // Forward
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
         }

        
        // Back
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }


    }


    // Programming the scene changes
    /* [8/8]: You have to tag the transporter with the name of the scene
        you want to go to
    */
    public void sceneChange(string sceneName)
    {
        Debug.Log("Changing Scene!");
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag != "Untagged")
            sceneChange(collider.gameObject.tag);
        
        
    }

}
