using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] Texture2D mouseIcon1;
    [SerializeField] Texture2D mouseIcon2;

    public void SetCursor1()
    {
        Cursor.SetCursor(mouseIcon1, new Vector2(mouseIcon1.height / 2, mouseIcon1.width / 2), CursorMode.Auto);
        //Debug.Log();
    }

    public void SetCursor2()
    {
        Cursor.SetCursor(mouseIcon1, Vector2.zero, CursorMode.Auto);
    }
}
