using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour, IGetSeed
{
    //»ý¼ºµÈ ¾¾¾Ñ È¸Àü
    float rotSpeed = 150;//¾¾¾Ñ È¸Àü ¼Óµµ
    public GameObject destroyEff;//Ãæµ¹½Ã ÅÍÁö´Â ÀÌÆåÆ®
    public GameObject EffMade;

    private void OnEnable()
    {
        Instantiate(EffMade, this.transform.position, Quaternion.identity);
    }
    void Update()
    {
        this.transform.Rotate(new Vector3(rotSpeed * Time.deltaTime, 0, 0));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IGetSeed getSeed = other.GetComponent<IGetSeed>();
            
            if (other.GetComponent<P_Hatch_Plant>().isgetSeed == false)
            {
                getSeed.GetSeed();
                destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
                Destroy(destroyEff, 1);
            }
        }
        //else if (other.GetComponent<P_Casper_Plant>().isgetSeed == false)
        //{
        //    IGetSeed getSeed = other.GetComponent<IGetSeed>();
        //    if (other.tag == "Player")
        //    {
        //        getSeed.GetSeed();
        //        destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
        //        this.gameObject.SetActive(false);
        //        Destroy(destroyEff, 1);
        //    }
        //}


    }


    public void GetSeed()
    {
        Debug.Log("¾¾¾ÑÀ» ¾ò¾ú´Ù");
        P_Hatch_Plant hatchPlant = FindObjectOfType<P_Hatch_Plant>();
        hatchPlant.isgetSeed = true;
        destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
        Destroy(destroyEff, 1);
        gameObject.SetActive(false);
    }
    public void GetSeed2()
    {
        Debug.Log("¾¾¾ÑÀ» ¾ò¾ú´Ù");
        P_Casper_Plant casperPlant = FindObjectOfType<P_Casper_Plant>();
        casperPlant.isgetSeed = true;
        destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
        Destroy(destroyEff, 1);
        gameObject.SetActive(false);
    }
}
