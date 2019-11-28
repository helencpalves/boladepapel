using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public float power = 150f;
    public float powerincrease = 0.4f;
    private bool moving = false;
    public Collider coll;
    Vector3 initialPos;
    Transform pai;
    Score score;
    public TextMeshProUGUI powertxt;

    // Start is called before the first frame update
    void Start()
    {
        print("Ball Start");
        score = (Score) FindObjectOfType(typeof(Score));

        pai = transform.parent;
        initialPos = transform.localPosition;

        coll = GetComponent<Collider>();
        coll.attachedRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (score.in_game){
            if (Input.GetButton("Fire1") && !moving)
            {
                power += powerincrease;
                powertxt.text = "Power: "+Mathf.RoundToInt(power/10).ToString();
                if (power > 300 || power < 0){
                    powerincrease = powerincrease*(-1);
                }
            }
            if (Input.GetButtonDown("Fire1") && !moving)
            {
                power = 150;
                powertxt.gameObject.SetActive(true);
            }
            if (Input.GetButtonUp("Fire1") && !moving)
            {
                moving = true;
                powertxt.gameObject.SetActive(false);
                coll.attachedRigidbody.useGravity = true;
                this.Launch(transform.forward, power);
            }
        } else {
            if (Input.GetButtonUp("Fire1"))
            {
                score.restart();
                Start();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Colisao");

        if(collision.gameObject.name == "target"){
            print("Ponto");
            score.hit();
            // Destroy(gameObject);
        } else {
            print("Perdida");
            score.missed();
            // power = 0;
        }

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        moving = false;
        coll.attachedRigidbody.useGravity = false;

        transform.parent = pai;
        transform.localPosition = initialPos;
    }

    void Launch(Vector3 direction, float power)
    {
        //transform.position += direction * power;
        transform.parent = null;

        GetComponent<Rigidbody>().AddForce(direction * power);
    }
}
