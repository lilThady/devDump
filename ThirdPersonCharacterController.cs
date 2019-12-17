using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    //hier onder zitten alle variablen van de character controller.
    public float Speed;



    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();

    }

    // Functie dat de speler kan bewegen over de grond heen.  
    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //een Vector punt zodat we weten hoe de character orienteerd. dus kort wat de x , y en z is.
        // delta.time zorgt er voor dat de beweging smooth afspeelt
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

}      
