using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header ("VARIABLES MOVIMIENTO")]
    public float velocity = 0f; //velocidad inicial
    float walkVelocity = 2f;
    float runVelocity = 5f;
    float crouchVelocity = 0.3f;
    float z;
    float x;
    public bool isGrounded = false;
    bool interacting = false;
    bool canJump = false;
    Vector3 jump;
    public float jumpVelocity=20;

    Rigidbody rb;

    Animator anim;

    [Header("UI VARIABLES")]
    public Text UIText;

    [Header("OTRAS VARIABLES")]
       //LEVEL1 
    public Transform playerCamera;
    public float turnSmoothing = 0.06f;
    public bool inLevel1 = false;
   

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 10.0f, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("6_Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region CAMBIO DE ESCENA
        if (other.gameObject.tag == "ToLevel0")
        {
            SceneManager.LoadScene("0_Start");
        }
        if (other.gameObject.tag == "ToLevel1")
        {
            SceneManager.LoadScene("1_House");
        }
        if (other.gameObject.tag == "ToLevel2")
        {
            SceneManager.LoadScene("2_HouseFirstFloor");
        }
        if (other.gameObject.tag == "ToLevel3")
        {
            SceneManager.LoadScene("3_Forest");
        }
        if (other.gameObject.tag == "ToLevel4")
        {
            SceneManager.LoadScene("4_Cave");
        }
        if (other.gameObject.tag == "ToLevel5")
        {
            SceneManager.LoadScene("5_Lab");
        }
        #endregion

        #region TRIGGERS DE EVENTOS
        //Triggers de EVENTOS
        if (other.gameObject.tag == "Event0") // IA Walkros level0
        {
            GameManager.Instance.EventWalkros0 = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Event0Cin") // Cinemática Level0
        {
            eventLevel(0);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Event3Cin") // Cinemática primera Level3
        {
            eventLevel(3f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Event3.2Cin") // Cinemática segunda Level3
        {
            eventLevel(6f);//evento level 3 pero para que no pete
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Event4") // Cinemática Level4
        {
            eventLevel(4f);
            Destroy(other.gameObject);
        }
        #endregion

        #region SAVE COLLIDERS
        if (other.gameObject.tag == "SaveTrigger") // Save the game
        {
            GameManager.Instance.SaveSystem();
        }
        #endregion

        #region TREE COLLIDERS
        if (other.gameObject.tag == "TreeTrigger") 
        {
            canJump = true;
        }
        #endregion
    }
    private void OnTriggerExit(Collider other)
    {
        #region TREE COLLIDERS
        if (other.gameObject.tag == "TreeTrigger")
        {
            canJump = false;
        }
        #endregion
    }
    void FixedUpdate()
    {
        #region MOVIMIENTO PLAYER
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        //set movement
        rb.MovePosition(transform.position + (z * transform.forward) * Time.deltaTime * velocity);

        if (Input.GetAxis("Jump") !=0 && isGrounded && GameManager.Instance.Jump == true && canJump)
        {
            rb.AddForce(jump * Input.GetAxis("Jump")*jumpVelocity, ForceMode.Impulse);
            isGrounded = false;
        }
        //Idle
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Stealth") == 0 && Input.GetAxis("Run") == 0 && interacting == false)
        {
            anim.SetFloat("Movement", 0f);
        }
        //Idle agachado
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Stealth") != 0 && Input.GetAxis("Run") == 0 && interacting == false)
        {
            anim.SetFloat("Movement", 0f);
            anim.SetBool("Crouch", true);
            anim.SetBool("CrouchWalk", false);
        }
        //Sigilo
        if (isGrounded == true && Input.GetAxis("Stealth") != 0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Run") == 0 && interacting == false)
        {
            anim.SetBool("Crouch", true); //Animación agacharse
            velocity = crouchVelocity; //velocidad de sigilo
            GameManager.Instance.stealthOn = true;
            Debug.Log("sigilo " + GameManager.Instance.stealthOn);
        }
        //Andar
        if (isGrounded == true && Input.GetAxis("Vertical") != 0 && Input.GetAxis("Stealth") == 0 && Input.GetAxis("Run") == 0 && interacting == false)
        {
            anim.SetFloat("Movement", 1);//Animación andar
            velocity = walkVelocity;
        }
        //Correr
        if (isGrounded == true && Input.GetAxis("Run") != 0 && Input.GetAxis("Vertical") != 0 && Input.GetAxis("Stealth") == 0 && interacting == false)
        {
            anim.SetFloat("Movement", 2); //Animación correr
            velocity = runVelocity; //velocidad de correr
        }
        //Andar agachado
        if (isGrounded == true && Input.GetAxis("Vertical") != 0 && Input.GetAxis("Stealth") != 0 && Input.GetAxis("Run") == 0 && GameManager.Instance.stealthOn == true && interacting == false)
        {
            anim.SetBool("CrouchWalk", true);
        }
        //Dejar de estar en sigilo
        if (Input.GetAxis("Stealth") == 0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Stealth") == 0 && interacting == false)
        {
            velocity = 0;
            GameManager.Instance.stealthOn = false;
            //Debug.Log("sigilo " + GameManager.Instance.stealthOn);
            anim.SetBool("Crouch", false); //Animación levantarse
            anim.SetFloat("Movement", 0);//Animación idle
        }
        //Dejar de correr
        if (Input.GetAxis("Run") == 0 && Input.GetAxis("Vertical") != 0 && Input.GetAxis("Stealth") == 0 && interacting == false)
        {
            velocity = walkVelocity;
            anim.SetFloat("Movement", 1);//Animación andar
            GameManager.Instance.stealthOn = false;
        }
        #endregion

        #region LEVEL0
        //LEVEL0
        if (SceneManager.GetActiveScene().name == "0_Start")
        {
            GameManager.Instance.currentLevel = "0_Start";

            GameManager.Instance.spawn1point = true; //SpawnPoint in Level1
            GameManager.Instance.spawn2point = false;
            GameManager.Instance.spawn3point = false;
            GameManager.Instance.spawnDone = false;

            if (GameManager.Instance.Jump == false)
            {
                GameObject.Find("Main Camera").GetComponent<GM_Level0>().text.text = "Me duele todo el cuerpo de la caída. No puedo moverme bien";
            }
            float dist1 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level0>().FirstAidKit.transform.position, transform.position); //FirstAidKit
            if (dist1 < 3f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                GameManager.Instance.enableJump();
                setTextUI("Me encuentro mucho mejor. Debería investigar este lugar.");
                GameObject.Find("Main Camera").GetComponent<GM_Level0>().FirstAidKit.SetActive(false);
                GameManager.Instance.SaveSystem();
            }

            //if (isGrounded == true && Input.GetButtonDown("Jump") && GameManager.Instance.Jump == true && canJump == true)
            //{
            //    velocity = 0;
            //    anim.SetTrigger("Jump");
            //    isGrounded = false;
            //    GameObject.Find("Main Camera").GetComponent<GM_Level0>().FallenTree1Coll.isTrigger = true;
            //    GameObject.Find("Main Camera").GetComponent<GM_Level0>().FallenTree2Coll.isTrigger = true;
            //}

        }
        #endregion

        #region level1
        //LEVEL1
        if (SceneManager.GetActiveScene().name == "1_House")
        {

            inLevel1 = true;
            GameManager.Instance.currentLevel = "1_House";

            GameManager.Instance.EventWalkros0 = false; //IA del walkros level0

            float dist4 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level1>().woodenPlanks.transform.position, transform.position); //Woodenplanks
            if (dist4 < 5f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                Debug.Log("Woodenplanks");
                GameManager.Instance.takeWoodenplanks();
                setTextUI("Perfecto, ahora puedo investigar con calma este lugar. Aunque no veo bien. Debería encender alguna luz");
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().woodenPlanks.SetActive(false);
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().event1Done();
                GameManager.Instance.SaveSystem();
            }
            if (GameManager.Instance.woodenPlanks == false && GameManager.Instance.EventWalkros1Done==false)
            {
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().text.text = "Tengo que bloquear la puerta con algo o ese Walkros entrará";
                GameManager.Instance.roomDoorOpen = false;
            }
            GameObject.Find("Main Camera").GetComponent<GM_Level1>().timer  += Time.deltaTime;
            Debug.Log(GameObject.Find("Main Camera").GetComponent<GM_Level1>().timer);
            if (GameObject.Find("Main Camera").GetComponent<GM_Level1>().timer>=20 && GameManager.Instance.woodenPlanks == false && GameManager.Instance.EventWalkros1Done == false) //WoodenPlanks Timer
            {
                Debug.Log("No has cerrado la puerta y los walkros entran");
                eventLevel(1);
            }


            float dist3 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level1>().LightBox.transform.position, transform.position); //LightBox
            if (dist3 < 3f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                Debug.Log("LightBox");
                GameManager.Instance.enableLightboxLight();
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().text.text = "Ahora veo mucho mejor. Puedo investigar.";
                GameManager.Instance.SaveSystem();
            }

            float dist2 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level1>().fireplaceThings.transform.position, transform.position); //Fireplace
            if (dist2 < 3f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                Debug.Log("Fireplace");
                GameManager.Instance.enableFireLight();
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().text.text = "Ahora veo mucho mejor. Puedo investigar.";
                GameManager.Instance.SaveSystem();
            }

            float dist5 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level1>().Cage.transform.position, transform.position); //Cage
            if (dist5 < 2f && GameManager.Instance.LightBoxOn == true && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                Debug.Log("Cage");
                GameManager.Instance.roomDoorOpen = true; //Se desbloquea la entrada a la habitación de arriba
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().birdCageOpen = true; //Animación pájaro 
                eventLevel(10f);
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().birdSoundOn = true;
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().text.text = "¡Menudo susto!\n ¿Qué es esto que hay bajo la jaula?\n Una llave! Ahora podré abrir la puerta del piso de arriba";
                GameManager.Instance.SaveSystem(); 
            }
            if (dist5 < 3f && GameManager.Instance.LightBoxOn == false && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                GameObject.Find("Main Camera").GetComponent<GM_Level1>().text.text = "Está muy oscuro. No veo lo suficiente";
            }
        }
        #endregion

        #region LEVEL2
        //LEVEL2
        if (SceneManager.GetActiveScene().name == "2_HouseFirstFloor")
        {
            GameManager.Instance.currentLevel = "2_HouseFirstFloor";

            GameManager.Instance.spawn2point = true; //SpawnPoint in Level1
            GameManager.Instance.spawn1point = false;
            GameManager.Instance.spawn3point = false;
            GameManager.Instance.spawnDone = false;

            float dist = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level2>().Computer.transform.position, transform.position); //Computer
            if (dist < 4f && Input.GetButtonDown("Interaction") && GameObject.Find("Main Camera").GetComponent<GM_Level2>().computerPassword == false && GameObject.Find("Main Camera").GetComponent<GM_Level2>().usb == false)
            {
                InteractAnimationEvent(0);
                Debug.Log("Computer Off");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().text.text = "Parece que necesito una contraseña para desbloquearlo...";
            }
            if (dist < 3f && Input.GetButtonDown("Interaction") && GameObject.Find("Main Camera").GetComponent<GM_Level2>().computerPassword == true && GameObject.Find("Main Camera").GetComponent<GM_Level2>().usb == false)
            {
                InteractAnimationEvent(0);
                Debug.Log("Computer On");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().computerLight.gameObject.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().text.text = "LA CONTRASEÑA LO HA DESBLOQUEADO!\n En la pantalla solo hay otra clave... ¿Para qué servirá?";
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().passwordInPcImage.gameObject.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().StartCoroutine("disableUIElement");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().safeboxPassword = true;
                GameManager.Instance.SaveSystem();
            }
            if (dist < 3f && Input.GetButtonDown("Interaction") && GameObject.Find("Main Camera").GetComponent<GM_Level2>().computerPassword == true && GameObject.Find("Main Camera").GetComponent<GM_Level2>().usb == true)
            {
                InteractAnimationEvent(0);
                Debug.Log("DOCUMENTS");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().text.text = "Está llena de documentos. Indican que en algún lugar del bosque hay una cueva en la que podría encontrar respuestas.";
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().caveMapImage.gameObject.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().StartCoroutine("disableMapUI");
                GameManager.Instance.forestDoorOpen = true; //Se desbloquea la entrada al camino hacia la cueva
                GameManager.Instance.Level2Completed = true; //Se desbloquea la puerta de la habitación
                GameManager.Instance.SaveSystem();
            }

            float dist2 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level2>().SafeBox.transform.position, transform.position); //SafeBox
            if (dist2 < 3f && Input.GetButtonDown("Interaction") && GameObject.Find("Main Camera").GetComponent<GM_Level2>().safeboxPassword == false)
            {
                InteractAnimationEvent(0);
                Debug.Log("SafeBox");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().text.text = "Necesito la combinación";
            }
            if (dist2 < 3f && Input.GetButtonDown("Interaction") && GameObject.Find("Main Camera").GetComponent<GM_Level2>().safeboxPassword == true)
            {
                InteractAnimationEvent(0);
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().Usb.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().SafeBoxAnim.SetTrigger("Open");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().BlindWalkros.GetComponent<Animator>().SetBool("Detect", true);
                //eventLevel(2f);
                Debug.Log("SafeBox");
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().text.text = "Oh no! Un Walkros! \n Debería esconderme";
                GameObject.Find("Main Camera").GetComponent<GM_Level2>().usb = true;
                GameManager.Instance.SaveSystem();
            }
        }
        #endregion

        #region LEVEL3
        //LEVEL3
        if (SceneManager.GetActiveScene().name == "3_Forest")
        {
            GameManager.Instance.currentLevel = "3_Forest";

            GameManager.Instance.EventWalkros0 = false;
            GameManager.Instance.EventWalkros1 = false;

            GameManager.Instance.spawn3point = true; //SpawnPoint in Level1
            GameManager.Instance.spawn1point = false;
            GameManager.Instance.spawn2point = false;
            GameManager.Instance.spawnDone = false;

            float dist = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level3>().Stones.transform.position, transform.position); //Stones Event
            if (dist < 9)
            {
                GameObject.Find("Main Camera").GetComponent<GM_Level3>().text.text = "La cueva... pero ¡Oh no! Un walkros bloquea la entrada. Tengo que hacer algo para alejarlo de allí";
            }
            if (dist < 3 && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(1);
                GameObject.Find("Main Camera").GetComponent<GM_Level3>().text.text = "¡Perfecto! Ya puedo pasar.";
                GameManager.Instance.throwStones();
            }

            //float dist3 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level3>().FallenTree3.transform.position, transform.position); //FallenTree1
            //if (dist3 < 0.6f && isGrounded == true && Input.GetButtonDown("Jump") && GameManager.Instance.Jump == true)
            //{
            //    velocity = 0;
            //    anim.SetTrigger("Jump");
            //    isGrounded = false;
            //    GameObject.Find("Main Camera").GetComponent<GM_Level3>().FallenTree3Coll.isTrigger = true;
            //}
        }
        #endregion

        #region LEVEL4
        //LEVEL4
        if (SceneManager.GetActiveScene().name == "4_Cave")
        {
            GameManager.Instance.currentLevel = "4_Cave";

            float dist1 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level4>().LabKey.transform.position, transform.position); //Lab.Key
            if (dist1 < 2f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(0);
                Debug.Log("Lab.key");
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().text.text = "Una LLAVE! Me pregunto qué puerta abrirá...";
                GameManager.Instance.unlockingLabDoor();
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().LabKey.SetActive(false);
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().TpLevel4To5.SetActive(true);
                GameManager.Instance.SaveSystem();
            }

            float dist3 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level4>().Bone.transform.position, transform.position); //Bone EVENT
            if (dist3 < 5f && dist3 > 2f && GameManager.Instance.unlockLabDoor == false)
            {
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().text.text = "Oh no, un Walkros\n Parece que hay algo cerca de él en el suelo...\n Tengo que encontrar la forma de distraerlo para poder ir a cogerlo";
            }
            if (dist3 < 2f && Input.GetButtonDown("Interaction"))
            {
                InteractAnimationEvent(1);
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().text.text = "Se ha distraído con el ruido, ¡es mi oportunidad!";
                GameObject.Find("Walkros_Araña").GetComponent<WalkrosAraña>().boneEventDone();
            }

            float dist2 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level4>().LabDoor.transform.position, transform.position); //Lab.Door
            if (dist2 < 3.5f && Input.GetButtonDown("Interaction") && GameManager.Instance.unlockLabDoor == false)
            {
                InteractAnimationEvent(0);
                Debug.Log("Lab.Door Closed");
                GameObject.Find("Main Camera").GetComponent<GM_Level4>().text.text = "Esta puerta está cerrada. Parece que necesito una llave";
            }
        }
        #endregion

        #region LEVEL5
        //LEVEL5
        if (SceneManager.GetActiveScene().name == "5_Lab")
        {
            GameManager.Instance.currentLevel = "5_Lab";

            float dist1 = Vector3.Distance(GameObject.Find("Main Camera").GetComponent<GM_Level5>().WalkrosCapsule.transform.position, transform.position); //Walkros Capsule
            if (dist1 < 4f && Input.GetButtonDown("Interaction"))
            {
                GameObject.Find("Main Camera").GetComponent<GM_Level5>().capsuleInteracted = true;
                setTextUI("Es un walkros pero... Me resulta demasiado familiar... \n ... <Sujeto de prueba Janette Wood> ... \n ¿?");
            }
        }
        #endregion
    }

    #region """Events"""

    public void InteractAnimationEvent(int type)
    {
        interacting = true;
        velocity = 0;
        if (type == 0) //Normal Interaction
        {
            StartCoroutine("stopInteractionEvent");
            anim.SetTrigger("Interact");
        }
        if (type == 1) //Throw Interaction
        {
            StartCoroutine("stopInteractionEvent");
            anim.SetTrigger("Throw");
        }
    }

    public void eventLevel(float level)
    {
        interacting = true;
        velocity = 0;
        if (level == 0)
        {
            StartCoroutine("stopEventLevel", 0);
            GameManager.Instance.EventWalkros0Cin = true;
        }
        if (level == 1)
        {
            StartCoroutine("stopEventLevel", 1);
            GameManager.Instance.EventWalkros1 = true;
        }
        if (level == 2)
        {
            StartCoroutine("stopEventLevel", 2);
            GameManager.Instance.EventWalkros2 = true;
        }
        if (level == 3)
        {
            StartCoroutine("stopEventLevel", 3);
            GameManager.Instance.EventWalkros3 = true;
        }
        if (level == 6)
        {
            StartCoroutine("stopEventLevel", 6);
            GameManager.Instance.EventWalkros3Sec = true;
        }
        if (level == 4)
        {
            StartCoroutine("stopEventLevel", 4f);
            GameManager.Instance.EventWalkros4 = true;
        }
        if (level == 10)
        {
            StartCoroutine("stopEventLevel", 10);
            GameManager.Instance.EventBird = true;
        }
    }

    IEnumerator stopEventLevel(float level)
    {
        yield return new WaitForSeconds(3);
        velocity = 1;
        interacting = false;

        if (level == 0)
        {
            GameManager.Instance.EventWalkros0Cin = false;
        }
        if (level == 1)
        {
            GameManager.Instance.EventWalkros1 = false;
        }
        if (level == 2)
        {
            GameManager.Instance.EventWalkros2 = false;
        }
        if (level == 3)
        {
            GameManager.Instance.EventWalkros3 = false;
        }
        if (level == 6)
        {
            GameManager.Instance.EventWalkros3Sec = false;
        }
        if (level == 4f)
        {
            GameManager.Instance.EventWalkros4 = false;
        }
        if (level == 10)
        {
            GameManager.Instance.EventBird = false;
        }
    }

    IEnumerator stopInteractionEvent()
    {
        yield return new WaitForSeconds(2);
        interacting = false;
        velocity = 1f;
    }
    #endregion

    public void setTextUI(string textUI)
    {
        UIText.text = textUI;
    }

    #region Movement Camera
    private void LateUpdate()
    {
        // set rotation, se puede cambiar
        Vector3 rotationVelocity = new Vector3(0, x, 0);
        Quaternion deltaRotation = Quaternion.Euler(rotationVelocity * Time.deltaTime * 100);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Call function that deals with player orientation.
        Rotating(x, z);
    }

    void Rotating(float horizontal, float vertical)
    {
        // Get camera forward direction, without vertical component.
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward);

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

        // Lerp current direction to calculated target direction.
        if ((IsMoving() && targetDirection != Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSmoothing);
            rb.MoveRotation(newRotation);
        }
       
    }
   
  
    public bool IsMoving()
    {
        return (z != 0);
    }
    #endregion
}
