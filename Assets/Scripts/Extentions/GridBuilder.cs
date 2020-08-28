using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridBuilder
{
    public static Vector2[,] GetGrid(this Vector2Int size, float screenWidth, float screenHeight, Paddings paddings)
    {
        float realWidth = screenWidth * (1f - paddings.LeftPadding - paddings.RightPadding);
        float realHeight = screenHeight * (1f - paddings.TopPadding - paddings.DownPadding);
        float horizontalOffset = realWidth / (size.x - 1);
        float verticalOffset = realHeight / (size.y - 1);
        float offset = Mathf.Min(horizontalOffset, verticalOffset);

        float additionalXoffset = (horizontalOffset > verticalOffset) ? realWidth - offset * (size.x - 1) : 0f;
        float additionalYoffset = (verticalOffset > horizontalOffset) ? realHeight - offset * (size.y - 1) : 0f;

        Vector2[,] grid = new Vector2[size.y, size.x];

        float startX = screenWidth * paddings.LeftPadding + additionalXoffset / 2;
        float startY = screenHeight * paddings.DownPadding + additionalYoffset / 2;

        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
                grid[y, x] = new Vector2(startX + offset * x, startY + offset * y);

        return grid;
    }
}
