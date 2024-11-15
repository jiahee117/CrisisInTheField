using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual<T>
{
    private Grid<T> grid;
    Mesh mesh;
    public bool gridValueChanged;
    public HeatMapVisual(Grid<T> grid, MeshFilter meshFilter)
    {
        this.grid = grid;
        gridValueChanged = false;
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        UpdateHeatMap();
        grid.OnGridValueChanged += Grid_OnGridValueChanged;

    }

    public void UpdateHeatMap()
    {
        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out vertices, out uvs, out triangles);


        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {

                int index = x * grid.GetHeight() + y;
                Debug.Log(index);
                Vector3 baseSize = new Vector3(1, 1) * grid.GetCellSize();
                MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, y) + baseSize * 0.5f, 0f, baseSize, Vector2.zero, Vector2.zero);


            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }


    private void Grid_OnGridValueChanged(object sender, EventArgs e)
    {
        gridValueChanged = true;
    }
}
