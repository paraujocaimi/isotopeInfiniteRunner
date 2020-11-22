using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Initialize variables.
    public HUD hud;
    private int element_a_value = 1;
    private int element_z_value = 1;
    private string element_name_value = "H";

    public GameObject elementNoExist, isDead;

	private Rigidbody rb;



	[HideInInspector]
	public bool canMove = true;

	[SerializeField]
	GameObject particle;

	[SerializeField]
    //emissions
    GameObject alfa, beta, ec, neutron;

    [SerializeField]
    //score
	private Text scoreText;
	private int score = 0;
    private static string BEST_SCORE = "BEST_SCORE";
    private static string LAST_SCORE = "LAST_SCORE";

    //moviments
    [SerializeField]
    float speed = 4f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    private enum UserInput
    {
        NONE, TAP, SWIPE
    }
    private Vector3 Direction;
    private UserInput userInput;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    private bool isGrounded = true;
    private bool isMovingRight = false;
    private bool hasPlayerStarted = false;



    private void Awake()
    {
        hud.updateValuesHUD(element_a_value, element_z_value, element_name_value);
    }

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
        elementNoExist.SetActive(false);
        isDead.SetActive(false);

        // inicializando variaveis para mobile
        Direction = Vector3.zero;
        userInput = UserInput.NONE;
        dragDistance = Screen.height * 10 / 100; //dragDistance is 10% height of the screen

        if (canMove == true)
        {
            Direction = Vector3.zero;
        }
    }

	// Update is called once per frame
	void Update () {

        UpdateUserInput();

        if(canMove == true)
        {
            speed = 3;

            //mobile moviment
            if (Application.platform == RuntimePlatform.Android)
            {
                if (userInput == UserInput.TAP)
                {
                    MovimentPlayer();
                }

                if (userInput == UserInput.SWIPE && isGrounded)
                {
                    JumpPlayer();
                }
                //voltando ao status de rolando
                userInput = UserInput.NONE;
            }
            // desktop moviment
            else
            {
                if (Input.GetMouseButtonDown(0))
                {

                    // If the game hasen't started yet.
                    if (hasPlayerStarted == false)
                    {
                        hasPlayerStarted = true;
                        StartCoroutine(ShowGems(2.0f));
                    }

                    ChangeBoolean();
                    ChangeDirection();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    JumpPlayer();
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pressESC();
                }
            }
        }

		if (Physics.Raycast (this.transform.position, Vector3.down * 2) == false) {
			FallDown ();
		}
	}

    private void FixedUpdate()
    {
        if (hasPlayerStarted)
        {
            moviment();
        }

    }

    private void UpdateUserInput()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position; //last touch position

                //Check if drag distance is greater than 10% of the screen height
                if (lp.y > fp.y + dragDistance)
                {
                    userInput = UserInput.SWIPE;
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position; //last touch position

                //Check if drag distance is greater than 10% of the screen height
                if (lp.y > fp.y + dragDistance)
                {
                    userInput = UserInput.SWIPE;
                }
                else
                {   //It's a tap as the drag distance is less than 10% of the screen height
                    userInput = UserInput.TAP;
                }
            }
        }
        else
        {
            userInput = UserInput.NONE;
        }
    }

    public void moviment()
    {
        if (isMovingRight == true)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, speed);
        }
    }

    public void Jump()
    {
        //Check if player is grounded
        //rb.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * 3, ForceMode.Impulse);
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    }

    public void JumpPlayer()
    {
        if (isGrounded == true)
        {
            Jump();
            isGrounded = true;
        }

    }

    public void MovimentPlayer()
    {
        if (hasPlayerStarted == false)
        {
            hasPlayerStarted = true;
        }

        ChangeBoolean();
        moviment();

        if (Physics.Raycast(new Vector3(this.transform.position.x, 0.7f, this.transform.position.z), Vector3.down * 2) == false)
        {
            FallDown();
        }
    }

    // Hide the gems for the first 2.5 seconds (avoiding bugs).
    IEnumerator ShowGems (float count) {
        yield return new WaitForSeconds(count);
        // Checks if player hasn't fallen off before showing grms.
        if (canMove == true)
        {
            alfa.SetActive(true);
            beta.SetActive(true);
            ec.SetActive(true);
            neutron.SetActive(true);

        }
    }
		

	// Control player direction.
	private void ChangeBoolean() {
		isMovingRight = !isMovingRight;
	}

    private void ChangeDirection() {
		if (isMovingRight == true) {
			rb.velocity = new Vector3 (speed, 0f, 0f);
		} 
		else {
			rb.velocity = new Vector3 (0f,0f,speed);
		}
	}


	// When the player falls off the platform.
	private void FallDown() {
        isDead.SetActive(true);
        finishGame();
    }

    private void pressESC()
    {
        canMove = false;
        rb.velocity = new Vector3(0f, 0f, 0f);
        finishGame();
    }


    private void finishGame()
    {
        canMove = false;
        speed = 0;
        rb.velocity = new Vector3(0f, -4f, 0f);
        StartCoroutine(ReturnToMainMenu(1.5f, 0));
        salvarScore(score);
    }

    // Return to main menu.
    IEnumerator ReturnToMainMenu (float count, int sceane) {
		yield return new WaitForSeconds (count);
		Application.LoadLevel(sceane);
	}


	// When the player hits a gem.
	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Gem") {

			// Remove the gem.
			Destroy (other.gameObject);
		
			// Create the particle effect.
			GameObject _particle = Instantiate (particle) as GameObject;
			_particle.transform.position = this.transform.position;
			Destroy (_particle, 1f);

		}
	}

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void salvarScore(int score)
    {
        int currentBestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);

        currentBestScore = Mathf.Max(currentBestScore, score);

        PlayerPrefs.SetInt(LAST_SCORE, score);
        PlayerPrefs.SetInt(BEST_SCORE, currentBestScore);


    }

    private void GetEmission(Emition_Type.TypesEmittion emittion_type){

        bool exists = false;

        switch (emittion_type)
        {
            case Emition_Type.TypesEmittion.Alfa:
                element_a_value += -4;
                element_z_value += -2;
                break;
            case Emition_Type.TypesEmittion.BetaMinus:
                element_z_value += +1;
                break;
            case Emition_Type.TypesEmittion.Eletronic:
                element_z_value += -1;
                break;
            case Emition_Type.TypesEmittion.Neutron:
                element_a_value += 1;
                break;
        }


        foreach(Isotope isotope in JSONReader.emissionJson.isotopes) {

            if(element_z_value == isotope.Z && element_a_value == isotope.A) {
                element_name_value = isotope.Atomic;
                exists = true;
                UpdateScore();
            }

        }

        if(exists.Equals(false)){
            elementNoExist.SetActive(true);
            finishGame();
        }

        hud.updateValuesHUD(element_a_value, element_z_value, element_name_value);
    }
}
