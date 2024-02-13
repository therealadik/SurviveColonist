using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // ���� �������� �������
    public Vector2 hotSpot = Vector2.zero; // �����, ������� ����� �������������� � �������� '�����������' �������

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
