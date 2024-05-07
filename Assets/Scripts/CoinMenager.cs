using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinMenager : MonoBehaviour
{
    public int coinCounter = 0;
    public int deathCounter = 0;
    public Text coinText;
    public Text deathText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = " " + coinCounter.ToString();
        deathText.text = " " + deathCounter.ToString();
    }
}
