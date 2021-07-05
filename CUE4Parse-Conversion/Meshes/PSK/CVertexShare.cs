﻿using System;
using System.Collections.Generic;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.Meshes;
using CUE4Parse.UE4.Objects.RenderCore;

namespace CUE4Parse_Conversion.Meshes.PSK
{
    public class CVertexShare
    {
        private const int _HASH_SIZE = 16384;
        
        public List<FVector> Points;
        public List<FPackedNormal> Normals;
        public List<uint> ExtraInfos;
        public List<int> WedgeToVert;
        public Lazy<int[]> VertToWedge;
        public int WedgeIndex;
        public FVector Mins;
        public FVector Maxs;
        public Lazy<FVector> Extents;
        public int[] Hash;
        public Lazy<int[]> HashNext;

        public void Prepare(CMeshVertex[] verts)
        {
            WedgeIndex = 0;
            Points = new List<FVector>();
            Normals = new List<FPackedNormal>();
            ExtraInfos = new List<uint>();
            WedgeToVert = new List<int>();
            VertToWedge = new Lazy<int[]>(new int[verts.Length]);

            ComputeBounds(verts);

            Extents = new Lazy<FVector>(() =>
            {
                var ret = Maxs - Mins;
                ret[0] += 1f;
                ret[1] += 1f;
                ret[2] += 1f;
                return ret;
            });

            Hash = new int[_HASH_SIZE];
            for (var i = 0; i < Hash.Length; i++)
            {
                Hash[i] = -1;
            }
            
            HashNext = new Lazy<int[]>(() =>
            {
                var ret = new int[verts.Length];
                for (var i = 0; i < ret.Length; i++)
                {
                    ret[i] = -1;
                }
                return ret;
            });
        }

        public void ComputeBounds(CMeshVertex[] verts, bool updateBounds = false)
        {
            var numVerts = verts.Length;
            if (numVerts <= 0)
            {
                if (updateBounds) return;
                Mins.Set(0f, 0f, 0f);
                Maxs.Set(0f, 0f, 0f);
                return;
            }

            var i = 0;
            if (!updateBounds)
            {
                Mins.Set(Maxs.Set(verts[i++].Position));
                numVerts--;
            }
            
            while (numVerts-- != 0)
            {
                var v = verts[i++].Position;
                if (v[0] < Mins[0]) Mins[0] = v[0];
                if (v[0] > Maxs[0]) Maxs[0] = v[0];
                if (v[1] < Mins[1]) Mins[1] = v[1];
                if (v[1] > Maxs[1]) Maxs[1] = v[1];
                if (v[2] < Mins[2]) Mins[2] = v[2];
                if (v[2] > Maxs[2]) Maxs[2] = v[2];
            }
        }

        public int AddVertex(FVector position, FPackedNormal normal, uint extraInfo = 0)
        {
            var pointIndex = -1;
            normal.Data &= 0xFFFFFFu;

            var h = (int)Math.Floor(((position[0] - Mins[0]) / Extents.Value[0] + (position[1] - Mins[1]) / Extents.Value[1] + (position[2] - Mins[2]) / Extents.Value[2]) * (_HASH_SIZE / 3.0f * 16)) % _HASH_SIZE;
            pointIndex = Hash[h];
            while (pointIndex >= 0)
            {
                if (Points[pointIndex] == position && Normals[pointIndex] == normal && ExtraInfos[pointIndex] == extraInfo)
                    break;
                pointIndex = HashNext.Value[pointIndex];
            }
            
            if (pointIndex == -1)
            {
                Points.Add(position);
                pointIndex = Points.Count - 1;
                Normals.Add(normal);
                ExtraInfos.Add(extraInfo);
                HashNext.Value[pointIndex] = Hash[h];
                Hash[h] = pointIndex;
            }

            WedgeToVert.Add(pointIndex);
            VertToWedge.Value[pointIndex] = WedgeIndex++;
            return pointIndex;
        }
    }
}