using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DESTRUCTION : MonoBehaviour {

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
