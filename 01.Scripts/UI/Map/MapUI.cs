using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MapUI : MonoBehaviour
{
    private UIDocument _doc;


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

        _doc.rootVisualElement.Q<VisualElement>("map-btn").RegisterCallback<ClickEvent>(evt =>
        {
            //¿©±â¿¡ ÅÂÈñ ¸Ê ³Ö´Â°Å
        });
    }
}
