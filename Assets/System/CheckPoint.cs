using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public RaceSystem raceSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//player�ɏՓ˂��ꂽ�������
        {
            raceSystem.PassTheCheckpoint();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
