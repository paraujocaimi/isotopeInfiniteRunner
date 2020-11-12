using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpScreen : MonoBehaviour
{
    // pontuação 
    public Text jump;
    public Text direction;

    private static string JUMP_MOBILE = "SWIPE";
    private static string JUMP_DESKTOP = "BARRA DE ESPAÇO";
    private static string DIRECTION_MOBILE = "TAP";
    private static string DIRECTION_DESKTOP = "CLICK DO MOUSE";

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            PlayerPrefs.SetString(JUMP_MOBILE, JUMP_MOBILE);
            PlayerPrefs.SetString(DIRECTION_MOBILE, DIRECTION_MOBILE);
        }
        else
        {
            PlayerPrefs.SetString(JUMP_DESKTOP, JUMP_DESKTOP);
            PlayerPrefs.SetString(DIRECTION_DESKTOP, DIRECTION_DESKTOP);
        }
    }
    public void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jump.text = PlayerPrefs.GetString(JUMP_MOBILE);
            direction.text = PlayerPrefs.GetString(DIRECTION_MOBILE);
        }
        else
        {
            jump.text = PlayerPrefs.GetString(JUMP_DESKTOP);
            direction.text = PlayerPrefs.GetString(DIRECTION_DESKTOP);
        }
    }
}
