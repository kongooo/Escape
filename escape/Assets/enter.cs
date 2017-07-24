using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter : MonoBehaviour {

    public AudioSource Wood;

    private void OnCollisionEnter(Collision collision)
    {
        Wood.Play();
    }
}
