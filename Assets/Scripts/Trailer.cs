using UnityEngine;

public class Trailer : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            float animationTime = clipInfo[0].clip.length;
            Invoke("OnAnimationComplete", animationTime);
        }
    }

    void OnAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
