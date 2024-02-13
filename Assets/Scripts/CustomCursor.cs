using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // ¬аша текстура курсора
    public Vector2 hotSpot = Vector2.zero; // “очка, котора€ будет использоватьс€ в качестве 'наконечника' курсора

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
