using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreen : Singleton<LoadingScreen>
{
    [SerializeField] private Image imgLoading;
    protected override void Awake()
    {
        base.KeepAlive(true);
        base.Awake();
    }

    public void ShowLoading(Action actionStartShow, Action actionFinish, float timer)
    {
        if (imgLoading == null) return;

        Array fillMethods = Enum.GetValues(typeof(Image.FillMethod));
        imgLoading.fillMethod = (Image.FillMethod)fillMethods.GetValue(UnityEngine.Random.Range(0, fillMethods.Length));

        imgLoading.fillAmount = 0f;
        imgLoading.DOKill();
        imgLoading.DOFillAmount(1f, timer)
                 .SetEase(Ease.Linear)
                 .OnComplete(delegate
                 {
                     actionStartShow?.Invoke();
                     DOVirtual.DelayedCall(0.5f, delegate
                     {
                         imgLoading.DOFillAmount(0f, timer).SetEase(Ease.Linear).OnComplete(() => actionFinish?.Invoke());
                     });
                 });

        
    }
}
