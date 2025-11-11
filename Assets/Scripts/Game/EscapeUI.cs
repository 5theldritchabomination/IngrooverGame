using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EscapeUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static EscapeUI Instance { get; private set; }

    private VisualElement root;

    private void Awake()
    {
        Instance = this;
        UIDocument uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
    }
    // Update is called once per frame
    public void Show()
    {
        VisualElement container = root.Q<VisualElement>("Buttons");
        int totalElement = container.childCount;
        for (int i = 0; i < totalElement; i++)
        {
            var button = container.ElementAt(i);
            button.style.display = true
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }
    }
    public void Hide()
    {
        VisualElement container = root.Q<VisualElement>("Buttons");
        int totalElement = container.childCount;
        for (int i = 0; i < totalElement; i++)
        {
            var button = container.ElementAt(i);
            button.style.display = false
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }

    }

    public void maiMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
