using UnityEngine;
using UnityEngine.UI;

public class PreviewNextElement : MonoBehaviour
{
    public Text a_alfa;
    public Text z_alfa;
    public Text element_alfa;

    public Text a_neutron;
    public Text z_neutron;
    public Text element_neutron;

    public Text a_captura;
    public Text z_captura;
    public Text element_captura;

    public Text a_beta;
    public Text z_beta;
    public Text element_beta;


    public void updateAlfaHud(int a, int z, string name, bool exists)
    {
        a_alfa.text = a.ToString();
        z_alfa.text = z.ToString();
        element_alfa.text = name;

        if (exists.Equals(false))
        {
            a_alfa.text = "<color=#A93226>" + a_alfa.text + "</color>";
            z_alfa.text = "<color=#A93226>" + z_alfa.text + "</color>";
            element_alfa.text = "<color=#A93226>" + element_alfa.text + "</color>";
        }

    }

    public void updateNeutronHud(int a, int z, string name, bool exists)
    {
        a_neutron.text = a.ToString();
        z_neutron.text = z.ToString();
        element_neutron.text = name;

        if (exists.Equals(false))
        {
            a_neutron.text = "<color=#A93226>" + a_neutron.text + "</color>";
            z_neutron.text = "<color=#A93226>" + z_neutron.text + "</color>";
            element_neutron.text = "<color=#A93226>" + element_neutron.text + "</color>";
        }
    }

    public void updateCapturaEletronicaHud(int a, int z, string name, bool exists)
    {
        a_captura.text = a.ToString();
        z_captura.text = z.ToString();
        element_captura.text = name;

        if (exists.Equals(false))
        {
            a_captura.text = "<color=#A93226>" + a_captura.text + "</color>";
            z_captura.text = "<color=#A93226>" + z_captura.text + "</color>";
            element_captura.text = "<color=#A93226>" + element_captura.text + "</color>";
        }
    }

    public void updateBetaHud(int a, int z, string name, bool exists)
    {
        a_beta.text = a.ToString();
        z_beta.text = z.ToString();
        element_beta.text = name;

        if (exists.Equals(false))
        {
            a_beta.text = "<color=#A93226>" + a_beta.text + "</color>";
            z_beta.text = "<color=#A93226>" + z_beta.text + "</color>";
            element_beta.text = "<color=#A93226>" + element_beta.text + "</color>";
        }
    }

}
