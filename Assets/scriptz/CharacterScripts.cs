using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScripts : MonoBehaviour
{ 
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
      
    }
    

    // Update is called once per frame
    void Update()
    {
     // Se il giocatore preme e il personaggio non si trova già sulla corsia di sinistra del nostro tracciato, allora
     if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -9)
        {
         // Spostamento del personaggio di 9 unità a sinistra
         transform.Translate(-9, 0, 0);
        }
       // Se il giocatore preme e il personaggio non si trova già sulla corsia di destra del nostro tracciato, allora
      if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 9)
        {
          // Spostamento del personaggio di 9 unità a destra
          transform.Translate(9, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
         rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        
            if (rb.velocity.y > 0)
            {
                GetComponent<Animator>().SetBool("jump", true);
            }


            if (rb.velocity.y == 0)
            {
                GetComponent<Animator>().SetBool("jump", false);

            }


       
    }
    void FixedUpdate()
    {
        
        {
         rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

        }
    }

    
}