using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class OpenWWWLink : MonoBehaviour
{

    public void OpenTwitter()
    {
#if !UNITY_EDITOR
		openWindow("https://twitter.com/BambooHutGames");
#endif
    }
    public void OpenChrisZabriskie()
    {
#if !UNITY_EDITOR
		openWindow("http://chriszabriskie.com");
#endif
    }
    public void OpenBlog()
    {
#if !UNITY_EDITOR
		openWindow("https://bamboohutgames.tumblr.com");
#endif
    }
    public void OpenYoutubeChannel()
    {
       // openWindow("https://www.youtube.com/channel/UC_TH1gmEFZWIrRuDdfgRewA");


#if !UNITY_EDITOR
		openWindow("https://www.youtube.com/channel/UC_TH1gmEFZWIrRuDdfgRewA");
#endif
    }



    [DllImport("__Internal")]
    private static extern void openWindow(string url);

}