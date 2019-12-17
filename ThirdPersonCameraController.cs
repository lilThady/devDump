using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{


    public float RotationSpeed = 1f;
    public Transform Target, Player;
    //floats voor de muis
    float mouseX, mouseY;

    public Transform Obstruction;
    float zoomSpeed = 2f;
   
  
    
    // Start is called before the first frame update
    void Start()
    {
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }

    void CamControl()
    {
        
        // muis input
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

        // dit zorgt er voor dat de speler niet verder met zijn muis omhoog en naar benden kan.
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    //voor als de camera door de muur heen gaat, renderd die de muur weg.
    void ViewObstructed()
    {
    //schiet een straal uit de camera 
        RaycastHit hit;

        //als er iets tussen de straal en de speler zit, dan...  
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            //en als de camera de speler zijn collider niet raakt dan.. 
            if (hit.collider.gameObject.tag != "Player")
            {
                // zet obstruction var om naar een transform en zorgt er voor dat wat tussen de camera en speler zit niet wordt gerenderd.
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }

            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

                if (Vector3.Distance(transform.position, Target.position) < 4.5f) 
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
                

            }

        }

    }

}
