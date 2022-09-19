using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{
    LeftToRight,
    RightToLeft,
    DownToUp,
    UpToDown
}

public class Fade : MonoBehaviour {

    static Animation fadeAnim;

    private void Start()
    {
        fadeAnim = GetComponent<Animation>();
    }

    public static void PlayFade(Dir dir)
    {
        switch (dir)
        {
            case Dir.LeftToRight:
                fadeAnim.Play("LeftToRight");
                break;

            case Dir.RightToLeft:
                fadeAnim.Play("RightToLeft");
                break;

            case Dir.DownToUp:
                fadeAnim.Play("DownToUp");
                break;

            case Dir.UpToDown:
                fadeAnim.Play("UpToDown");
                break;
        }
    }
}
