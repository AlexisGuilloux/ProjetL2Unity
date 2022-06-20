using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AppIcon : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject notificationParent;
    [SerializeField] private PanelController panelController;
    [SerializeField] public int appId = 0;
    [SerializeField] public int levelToAccessApp;

    private Transform transform;
    private Sequence sequence;

    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { panelController.Init(appId, iconImage.color, gameObject.transform.position);  NotificationOff();});
        transform = this.GetComponent<Transform>();
    }

    public void NotificationOn(bool withAnimation = true)
    {
        if (withAnimation)
        {
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, -8f), 0.2f)).SetEase(Ease.Linear)
                    .Join(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.25f)).SetEase(Ease.Linear)
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, -3f), 0.2f)).SetEase(Ease.Linear)
                    .Join(transform.DOScale(Vector3.one, 0.15f))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f)).SetEase(Ease.Linear)
                    .SetLoops(-1);
        }
        
        
        notificationParent.SetActive(true);
    }

    public void NotificationOff()
    {
        sequence.Kill();
        transform.localScale = Vector3.one;
        transform.DORotate(Vector3.zero, 0f);
        notificationParent.SetActive(false);
    }
}
