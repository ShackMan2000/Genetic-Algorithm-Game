using UnityEngine;
using UnityEngine.UI;

public class MusicBTN : MonoBehaviour
{


    private Text text;


    private void Start()
    {
        text = GetComponentInChildren<Text>();

        if (Music.Instance.audioSource.volume == 1f)
            text.text = "Turn Music Off";
        else
            text.text = "Turn Music On";
    }


    public void SoundBTNClicked()
    {
        if (Music.Instance.audioSource.volume == 1f)
        {
            Music.Instance.audioSource.volume = 0;
            text.text = "Turn Music On";

        }
        else
        {
            Music.Instance.audioSource.volume = 1f;
            text.text = "Turn Music Off";
        }
    }


}
