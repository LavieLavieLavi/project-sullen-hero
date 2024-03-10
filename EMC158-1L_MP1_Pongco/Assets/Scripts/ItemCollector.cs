using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    int coinCounter = 0;
    [SerializeField] Text coinText;
    [SerializeField] AudioSource itemSound;





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crown"))
        {
            Destroy(other.gameObject);
            coinCounter++;
            coinText.text = "Crowns: " + coinCounter;
            itemSound.Play();
        }
    }

    private void Update()
    {
        if (coinCounter >= 5)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }



}
