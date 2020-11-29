using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text a_value;
    public Text z_value;
    public Text element_name;

    //Method to update the values of the element status
    public void updateValuesHUD(int a, int z, string name)
    {
        a_value.text = a.ToString();
        z_value.text = z.ToString();
        element_name.text = name;
    }
}