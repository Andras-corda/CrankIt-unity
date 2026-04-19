using UnityEngine;
using System.Collections;

public enum ResourceType { Raw, Processed }

public class Resource : MonoBehaviour
{
    public int gridX;
    public int gridY;
    public ResourceType type = ResourceType.Raw;

    public SpriteRenderer spriteRenderer;
    public Sprite rawSprite;
    public Sprite processedSprite;

    public void Init(int x, int y, ResourceType t)
    {
        gridX = x;
        gridY = y;
        type = t;
        UpdateSprite();
        transform.position = GridManager.Instance.GridToWorld(x, y);
    }

    public void MoveTo(int x, int y)
    {
        gridX = x;
        gridY = y;
        StartCoroutine(AnimateMove(GridManager.Instance.GridToWorld(x, y)));
    }

    public void SetProcessed()
    {
        type = ResourceType.Processed;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = type == ResourceType.Raw ? rawSprite : processedSprite;
    }

    IEnumerator AnimateMove(Vector3 target)
    {
        Vector3 start = transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
        transform.position = target;
    }
}
