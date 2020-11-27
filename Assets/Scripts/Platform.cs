using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer == 11)
        {
            Invoke("FallDown", 2.0f);
        }
    }

	private void FallDown() {
		this.GetComponentInParent<Rigidbody>().isKinematic = false;
		Destroy (this.transform.parent.gameObject, 2f);
	}
}
