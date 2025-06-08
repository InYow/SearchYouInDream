using UnityEngine;
using DG.Tweening;

public class OutdoorAmbienceTrigger : MonoBehaviour
{
    public float fadeDuration = 1f;

    private Tween currentTween;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var outdoorAudio = SoundManager_New.Get("绿色草地背景音")[0];
            outdoorAudio.volume = 0;
            SoundManager_New.Play("绿色草地背景音");

            currentTween?.Kill();
            currentTween = DOTween.To(
                () => outdoorAudio.volume,
                x => outdoorAudio.volume = x,
                1f, fadeDuration);
            Debug.Log("播放");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var outdoorAudio = SoundManager_New.Get("绿色草地背景音")[0];

            currentTween?.Kill();
            currentTween = DOTween.To(
                () => outdoorAudio.volume,
                x => outdoorAudio.volume = x,
                0f, fadeDuration
            ).OnComplete(() => SoundManager_New.Stop("绿色草地背景音"));
            Debug.Log("停止");
        }
    }
}
