using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider capsuleColliderl;
    public float colliderminus = 0.5f;
    Vector3 OldcolliderPos;
    public int health = 3;
    public bool isAlive = true;
    private float crouchThreshold;
    public Text score;
    public bool thresholdSet = false;
    float leftShoulder = 0;
    float leftHip = 0;
    public float crouchThresholdOffset = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleColliderl = GetComponent<CapsuleCollider>();
        OldcolliderPos = capsuleColliderl.center;
        leftShoulder = float.Parse(Globals.Variables.LEFT_SHOULDER[1]);
        leftHip = float.Parse(Globals.Variables.LEFT_HIP[1]);
        //score.text = "\nLEFT_SHOULDER: " + Globals.Variables.LEFT_SHOULDER[1] + "\nLEFT_HIP: " + Globals.Variables.LEFT_HIP[1] + "\nCROUCHTHRESHOLD: " + crouchThreshold;
        //score.text = "\nksjbdf: " + leftShoulder.ToString() + "\nkdjfs: " + leftHip.ToString();
    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    anim?.SetBool("isCrounch", true);
        //    capsuleColliderl.center = new Vector3(capsuleColliderl.center.x,
        //         colliderminus, capsuleColliderl.center.z);
        //}
        
        if (Globals.Variables.LEFT_SHOULDER[1] != "" && !thresholdSet)
        {
            crouchThreshold = (float.Parse(Globals.Variables.LEFT_HIP[1]) + float.Parse(Globals.Variables.LEFT_SHOULDER[1])) / 2f;
            crouchThreshold += crouchThresholdOffset;
            thresholdSet = true;
        }
        if (float.Parse(Globals.Variables.LEFT_SHOULDER[1]) < crouchThreshold)
        {
            anim?.SetBool("isCrounch", true);
            capsuleColliderl.center = new Vector3(capsuleColliderl.center.x,
                 colliderminus, capsuleColliderl.center.z);
        }
        //else if (Input.GetKeyUp(KeyCode.C))
        //{
        //    anim?.SetBool("isCrounch", false);
        //    capsuleColliderl.center = OldcolliderPos;
        //}
        else
        {
            anim?.SetBool("isCrounch", false);
            capsuleColliderl.center = OldcolliderPos;
        }
        score.text = "\nLEFT_SHOULDER: " + Globals.Variables.LEFT_SHOULDER[1] + "\nLEFT_HIP: " + Globals.Variables.LEFT_HIP[1] + "\nCROUCHTHRESHOLD: " + crouchThreshold;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && isAlive)
        {
            DamageDetect();
            other.gameObject.SetActive(false);
            if (health <= 0)
            {
                anim?.SetTrigger("Death");
                Destroy(gameObject, 3f);
                isAlive = false;
            }
        }
        void DamageDetect()
        {
            if (health > 0)
            {
                health -= 1;
            }
        }
    }
}

