using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emition_Type : MonoBehaviour
{
    //Define type of emition
    public enum TypesEmittion { Alfa,BetaMinus, Eletronic, Neutron }

    [SerializeField]
    GameObject particle;

    //Selected type to this emition
    public TypesEmittion type_emition;

    // When the player hits a emission.
    private void OnTriggerEnter(Collider collision)
    {

        //mudar para layer
        if (collision.gameObject.layer == 11)
        {

            collision.gameObject.SendMessage("GetEmission", type_emition);

        }

    }
}