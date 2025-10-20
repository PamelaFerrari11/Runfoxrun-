using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScripts : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float shift;  
    [SerializeField] float jumpForce;  
    [SerializeField] Animator anim;  
    bool isGameOver;
    [SerializeField] GameObject menu;   
    [SerializeField] Animator death;
    [SerializeField] TMP_Text score;
    float roundScore; 
    [SerializeField] GameObject itemVFX;
    [SerializeField] GameObject shield; 
    Vector3 objectPosition;
    bool isShield;
    [SerializeField] AudioClip itemSFX, shieldSFX, obstacleSFX, destroySFX;
    [SerializeField] AudioSource sound, music;



   
      

    void Start()
    {       
       objectPosition = transform.position;
       print(objectPosition);      
    }     
    void Update() 
    {                   
        if (!isGameOver)
        {
            roundScore += Time.deltaTime;
          score.text = "score: " + roundScore.ToString("f1");

           // Se il giocatore preme e il personaggio non si trova già sulla corsia di sinistra del nostro tracciato, allora
            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -9)
            {
                // Spostamento del carattere di 9 unità a sinistra
                transform.Translate(-9, 0, 0);
            }
            // Se il giocatore preme e il personaggio non si trova già sulla corsia di destra del nostro tracciato, allora
            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 9)
            {
                // Spostamento del carattere di 9 unità a destra
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
        


    }
    void FixedUpdate()
    {
       if (!isGameOver)
       {
         rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);       
 
        }   
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if(isShield)
           {
             isShield=true;
             Destroy(other.gameObject);
             sound.clip = destroySFX;
             sound.Play();
            }
            else
           {
             isGameOver = true;
             menu.SetActive(true);
             death.SetBool("death",true);
             sound.clip = obstacleSFX;
             sound.Play();
             music.Stop();
           }
            
        }

    }

    void DectivateShield()
    {
        isShield=false;
    }


    private void OnTriggerEnter(Collider other) 
   {
       if(other.CompareTag("Money"))
       {  
         roundScore +=5;
          score.text = "scoure:"+ roundScore.ToString("f1");
          //GameObject vfx = Instantiate(itemVFX, other.transform.position, other.transform.rotation);
          //Destroy (vfx, 3f);
          
          
          Destroy(other.gameObject);
       }
    }

   void GenerateObject()
    {
     float distance = Random.Range(100,200);
      Instantiate (shield,transform.position + new Vector3(0,2,distance),transform.rotation);
      Invoke("GenerateObject", Random.Range (20,30));
   }
  
} 

