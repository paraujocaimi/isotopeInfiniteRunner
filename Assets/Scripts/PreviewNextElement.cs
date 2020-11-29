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

    private void updateAlfaHud(int a, int z, string name)
    {
        a_alfa.text = a.ToString();
        z_alfa.text = z.ToString();
        element_alfa.text = name;
    }

    private void updateNeutronHud(int a, int z, string name)
    {
        a_neutron.text = a.ToString();
        z_neutron.text = z.ToString();
        element_neutron.text = name;
    }

    private void updateCapturaEletronicaHud(int a, int z, string name)
    {
        a_captura.text = a.ToString();
        z_captura.text = z.ToString();
        element_captura.text = name;
    }

    private void updateBetaHud(int a, int z, string name)
    {
        a_beta.text = a.ToString();
        z_beta.text = z.ToString();
        element_beta.text = name;
    }

    public void update(string tipoemissao, int a, int z, string name)
    {
        string emissao = tipoemissao;

        switch (emissao)
        {
            case "alfa":
                a += -4;
                z += -2;
                updateAlfaHud(a, z, name);
                break;
            case "beta":
                z += +1;
                updateBetaHud(a, z, name);
                break;
            case "ec":
                z += -1;
                updateCapturaEletronicaHud(a, z, name);
                break;
            case "neutron":
                a += 1;
                updateNeutronHud(a, z, name);
                break;
        }

    }
}
