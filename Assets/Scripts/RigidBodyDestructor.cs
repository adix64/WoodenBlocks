using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyDestructor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
            StartCoroutine(DestroyRigidBody());
    }
    private IEnumerator DestroyRigidBody()
    {
        yield return new WaitForSeconds(3f);
        Destroy(GetComponent<Rigidbody>());
        Destroy(this);
    }
}