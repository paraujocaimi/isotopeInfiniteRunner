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
	private bool isMovingRight = false;
	private bool hasPlayerStarted = false;

	[SerializeField]
	float speed = 4f;

	[HideInInspector]
	public bool canMove = true;

	[SerializeField]
	GameObject particle;

	[SerializeField]
    GameObject alfa, beta, ec, neutron;

    [SerializeField]
	private Text scoreText;
	private int score = 0;
    private static string BEST_SCORE = "BEST_SCORE";
    private static string LAST_SCORE = "LAST_SCORE";

    private void Awake()
    {
        hud.updateValuesHUD(element_a_value, element_z_value, element_name_value);
    }

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
        elementNoExist.SetActive(false);
        isDead.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && canMove == true) {

			// If the game hasen't started yet.
			if (hasPlayerStarted == false) {
				hasPlayerStarted = true;
				StartCoroutine (ShowGems (2.0f));
			}
				
			ChangeBoolean ();
			ChangeDirection ();
		}

		if (Physics.Raycast (this.transform.position, Vector3.down * 2) == false) {
			FallDown ();
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

    private void finishGame()
    {
        canMove = false;
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
