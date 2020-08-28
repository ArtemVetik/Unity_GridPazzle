using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static T[,] Resize<T>(this T[,] matrix, int newWidth, int newHeight)
    {
        T[,] newMatrix = new T[newHeight, newWidth];

        int minWidth = Mathf.Min(matrix.GetLength(1), newWidth);
        int minHeight = Mathf.Min(matrix.GetLength(0), newHeight);

        for (int y = 0; y < minHeight; y++)
        {
            for (int x = 0; x < minWidth; x++)
            {
                newMatrix[y, x] = matrix[y, x];
            }
        }

        return newMatrix;
    }

    public static void ConfigureSprite(this SpriteRenderer spriteRenderer, Sprite newSprite, Vector2 newLocalScale, Vector2 newPosition = default, Vector2 newSize = default)
    {
        spriteRenderer.sprite = newSprite;
        spriteRenderer.transform.localScale = newLocalScale;
        spriteRenderer.transform.position = newPosition;
        spriteRenderer.size = newSize;
    }
}
