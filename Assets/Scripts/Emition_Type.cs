using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emition_Type : MonoBehaviour
{
    //Define type of emition
    public enum TypesEmittion { Alfa, BetaPlus, BetaMinus, Eletronic, Neutron }

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

            // Create the particle effect.
            GameObject _particle = Instantiate(particle) as GameObject;
            _particle.transform.position = this.transform.position;
            Destroy(_particle, 1f);

            collision.gameObject.SendMessage("GetEmition", type_emition);

            // Remove the gem.
            Destroy(this.gameObject);

        }

    }
}