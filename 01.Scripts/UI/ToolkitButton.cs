using System;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public struct ToolkitButton
{
    private Button _btn;

    public UIDocument UIDoc;
    public TopBarType TopBarType;
    public string ButtonName;
    public UnityEvent<ClickEvent> ClickEvent;
   
    public void Init()
    {
        _btn = UIDoc.rootVisualElement.Q<Button>(ButtonName);

        _btn.RegisterCallback<ClickEvent>(HandleClickEvent);
    }

    private void HandleClickEvent(ClickEvent evt)
    {
        ClickEvent?.Invoke(evt);
    }
}
