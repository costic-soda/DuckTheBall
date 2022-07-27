using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    [SerializeField] public GameObject targetBullet;
    [SerializeField] private float force;
    
     
    private Rigidbody _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        targetBullet = GameObject.FindGameObjectWithTag("Target");
    }
    private void OnEnable()
    {
        Invoke("hideBullet", 10f);

    }

    void hideBullet()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void FixedUpdate()
    {

        target();
    }
    public void target()
    {
        if (targetBullet != null)
        {


            transform.position = Vector3.MoveTowards(transform.position, targetBullet.transform.position,
                force *  Time.deltaTime );
           //Vector3 direction = targetBullet.transform.position - _rb.position;
           // direction.Normalize();
           // Vector3 rotationAmount = Vector3.Cross(-transform.up, direction);
            //_rb.angularVelocity = rotationAmount * rotaion;
            //_rb.velocity = -transform.up * force;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SCORE")
        {
            GameManager.instance.score++;
         gameObject.SetActive(false);
        }

    }
}
