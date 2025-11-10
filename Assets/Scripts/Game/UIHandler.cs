using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    private VisualElement root;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
    }

    public void SetHealthValue(int currentHealth)
    {
        VisualElement container = root.Q<VisualElement>("HealthContainer");
        int totalHearts = container.childCount;

        for (int i = 0; i < totalHearts; i++)
        {
            var heart = container.ElementAt(i);
            heart.style.display = (i < currentHealth)
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }
    }

    public void UpdateScore(int score)
    {
       
    }
}
