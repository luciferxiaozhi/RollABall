using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private int count;
    private float startTime;
    private bool finished = false;

    public float speed;
    public Text countText;
    public Text winText;
    public Text timerText;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        startTime = Time.time;
    }

    void Update()
    {
        if (finished)
            return;
        else
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f1");
            timerText.text = "Time used: " + minutes + ":" + seconds;
        }
    }
    // The function where physics code goes.
    void FixedUpdate()
    {
        if (finished)
            return;
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You WIN!!!";
            timerText.color = Color.green;
            finished = true;
        }
    }
}