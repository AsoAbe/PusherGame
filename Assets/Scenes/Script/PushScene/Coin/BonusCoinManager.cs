using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BonusCoinManager : MonoBehaviour
{
    public CoinController coinController;
    public CoinGenerator coinGenerator;
    public Vector2 BonusRange;
    public AudioClip BonusSE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            coinGenerator.audioSource.PlayOneShot(BonusSE);
            
            var AddCount = Random.Range(BonusRange.x, BonusRange.y);
            for (int i = 0; i < AddCount; i++)
            {
                var pos = coinGenerator.AddCoinPosition.position;
                pos.x = Random.Range(-coinGenerator.RandomPos_x, coinGenerator.RandomPos_x);
                coinGenerator.AddCoinPosition.position = pos;
                
                // ƒ‰ƒ“ƒ_ƒ€‚É‘I‘ð
                int randomIndex =
                    Random.Range(0, coinController.Coin.Length);


                Instantiate(coinController.Coin[randomIndex],
                    coinGenerator.AddCoinPosition.position, Quaternion.identity);
               
                coinGenerator.audioSource.PlayOneShot(coinGenerator.SE);
            }
        }
    }
}
