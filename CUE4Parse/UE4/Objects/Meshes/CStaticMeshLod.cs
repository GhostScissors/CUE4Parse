﻿using System;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.RenderCore;

namespace CUE4Parse.UE4.Objects.Meshes
{
    public class CStaticMeshLod : CBaseMeshLod
    {
        public CMeshVertex[] Verts;

        public void AllocateVerts(int count)
        {
            Verts = new CMeshVertex[count];
            for (var i = 0; i < Verts.Length; i++)
            {
                Verts[i] = new CMeshVertex(new FVector(), new FPackedNormal(0), new FPackedNormal(0), new FMeshUVFloat(0, 0));
            }

            NumVerts = count;
            AllocateUVBuffers();
        }

        public void BuildNormals()
        {
            if (HasNormals) return;
            // BuildNormalsCommon(Verts, Indices);
            HasNormals = true;
        }
    }

    public class CBaseMeshLod
    {
        public int NumVerts = 0;
        public int NumTexCoords = 0;
        public bool HasNormals = false;
        public bool HasTangents = false;
        public Lazy<CMeshSection[]> Sections;
        public Lazy<FMeshUVFloat[][]> ExtraUV;
        public FColor[] VertexColors;
        public Lazy<FRawStaticIndexBuffer> Indices;

        public void AllocateUVBuffers()
        {
            ExtraUV = new Lazy<FMeshUVFloat[][]>(() =>
            {
                var ret = new FMeshUVFloat[NumTexCoords - 1][];
                for (var i = 0; i < ret.Length; i++)
                {
                    ret[i] = new FMeshUVFloat[NumVerts];
                    for (var j = 0; j < ret[i].Length; j++)
                    {
                        ret[i][j] = new FMeshUVFloat(0, 0);
                    }
                }
                return ret;
            });
        }

        public void AllocateVertexColorBuffer()
        {
            VertexColors = new FColor[NumVerts];
            for (var i = 0; i < VertexColors.Length; i++)
            {
                VertexColors[i] = new FColor();
            }
        }
    }
}