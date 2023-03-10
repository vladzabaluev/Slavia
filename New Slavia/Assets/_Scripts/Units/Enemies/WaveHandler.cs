using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    private int waveCounter;
    private int waveRecord;
    private int temp;
    private GameObject enem;
    private GameObject[] lvl1,lvl2,lvl3;
    private int[] tempWaves;
    private bool temp1;
    void Start()
    {
        waveRecord = PlayerPrefs.GetInt("WaveRecord");
        waveCounter = 0;
        lvl1 = Resources.LoadAll<GameObject>("Waves/LVL1");
        lvl2 = Resources.LoadAll<GameObject>("Waves/LVL2");
        lvl3 = Resources.LoadAll<GameObject>("Waves/LVL3");
        tempWaves = new int[] { -1, -1, -1, -1, -1 };
    }

    void FixedUpdate()
    {
        enem = GameObject.FindGameObjectWithTag("Enemy");
        temp = Random.Range(0, 10);
        if (enem == null)
        {
            waveCounter++;
            temp1 = true;
            if (waveCounter % 6 == 0)
            {
                tempWaves = new int[] { -1, -1, -1, -1, -1 };
            }
            while (temp1){
                temp1 = false;
                foreach (int n in tempWaves)
                {
                    if (n == temp)
                    {
                        temp = Random.Range(0, 10);
                        temp1 = true;
                        break;
                    }
                }
            }
            tempWaves[waveCounter % 5] = temp;
            if (waveCounter <= 5 && waveCounter > 0)
            {
                Instantiate(lvl1[temp], new Vector3(11,0,0), Quaternion.Euler(0, 0, 0));
            }
            if (waveCounter <= 10 && waveCounter > 5)
            {
                Instantiate(lvl2[temp], new Vector3(11, 0, 0), Quaternion.Euler(0, 0, 0));
            }
            if (waveCounter <= 15 && waveCounter > 10)
            {
                Instantiate(lvl3[temp], new Vector3(11, 0, 0), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
