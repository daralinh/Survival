using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tilePalette;
    public int tilemapSize;

    private Vector3Int center;
    private Vector3Int currentPlayerCellPos;
    private int halfSize;
    
    private void Start()
    {
        if (tilemapSize % 2 == 1)
        {
            tilemapSize--;
        }

        center = tilemap.WorldToCell(PlayerController.Instance.transform.position);
        halfSize = tilemapSize / 2;

        GenerateInitialTilemap();
    }

    private void FixedUpdate()
    {
        currentPlayerCellPos = tilemap.WorldToCell(PlayerController.Instance.transform.position);

        if (currentPlayerCellPos != center)
        {
            RenderFollowPlayer();
        }
    }

    private void GenerateInitialTilemap()
    {
        Vector3Int centerCell = tilemap.WorldToCell(PlayerController.Instance.transform.position);

        for (int x = -halfSize; x <= halfSize; x++)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                Vector3Int cellPosition = new Vector3Int(centerCell.x + x, centerCell.y + y, 0);

                tilemap.SetTile(cellPosition, GetRandomTileBase());
            }
        }
    }

    private TileBase GetRandomTileBase()
    {
        return tilePalette[Random.Range(0, tilePalette.Length)];
    }

    private void RenderFollowPlayer()
    {
        while (currentPlayerCellPos.x < center.x)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                tilemap.SetTile( new Vector3Int(center.x + halfSize, center.y + y) , null);
                tilemap.SetTile( new Vector3Int(center.x - halfSize - 1, center.y + y) , GetRandomTileBase());
            }
            center = new Vector3Int(center.x - 1, center.y, 0);
        }

        while (currentPlayerCellPos.x > center.x)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                tilemap.SetTile(new Vector3Int(center.x - halfSize, center.y + y), null);
                tilemap.SetTile(new Vector3Int(center.x + halfSize + 1, center.y + y), GetRandomTileBase());
            }
            center = new Vector3Int(center.x + 1, center.y, 0);
        }

        while (currentPlayerCellPos.y > center.y)
        {
            for (int x = -halfSize; x <= halfSize; x++)
            {
                tilemap.SetTile(new Vector3Int(center.x + x, center.y - halfSize), null);
                tilemap.SetTile(new Vector3Int(center.x + x, center.y + halfSize + 1), GetRandomTileBase());
            }
            center = new Vector3Int(center.x, center.y + 1);
        }

        while (currentPlayerCellPos.y < center.y)
        {
            for (int x = -halfSize; x <= halfSize; x++)
            {
                tilemap.SetTile(new Vector3Int(center.x + x, center.y + halfSize), null);
                tilemap.SetTile(new Vector3Int(center.x + x, center.y - halfSize - 1), GetRandomTileBase());
            }
            center = new Vector3Int(center.x, center.y - 1);
        }
    }

}
