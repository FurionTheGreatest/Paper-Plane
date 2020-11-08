using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
public float movementSpeed = 10f;
public float lookatspeed = 5f;


    void Update () 
    {
 
        if (Input.GetKey ("a") && !Input.GetKey ("d")) 
        {
                transform.position += transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
        } 
        else if (Input.GetKey ("d") && !Input.GetKey ("a")) 
        {
                transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
        }
        
    }
}