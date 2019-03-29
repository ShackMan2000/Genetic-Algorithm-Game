using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> songs;
    private int currentClipID = 0;



    private static Music instance;

    public static Music Instance
    {
        get
        {


            return instance;
        }
    }


    private void Awake()
    {

        if (instance == null)

        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        }

        else
        {
            Destroy(gameObject);

        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }




    //public IEnumerator PlaySong()
    //{
       
    //        AudioClip currentClip = ChooseNewSong(); 

    //        audioSource.clip = currentClip;
    //        audioSource.Play();


    //    yield return new WaitForSeconds(currentClip.length/200f);

    //    StartCoroutine(PlaySong());

    //}


    //private AudioClip ChooseNewSong()
    //{
    //    currentClipID ++;
    //    if(currentClipID == songs.Count)
    //        currentClipID = 0;

    //    return songs[currentClipID];
    //}

    //private void CheckIfMusicIsPlaying()
    //{
    //    if(!audioSource.isPlaying)
    //    {

    //    }
    //}

}
