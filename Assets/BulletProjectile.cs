using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float speed = 20f;

    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
