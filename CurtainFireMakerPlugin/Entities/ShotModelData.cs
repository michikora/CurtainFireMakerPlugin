﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsMmdDataIO.Pmx.Data;

namespace CurtainFireMakerPlugin.Entities
{
    public class ShotModelData
    {
        public HashSet<EntityShot> OwnerEntities { get; } = new HashSet<EntityShot>();

        public PmxMorphData MaterialMorph { get; } = new PmxMorphData();

        public Dictionary<string, PmxMorphData> MorphDict { get; } = new Dictionary<string, PmxMorphData>();

        public PmxBoneData[] Bones { get; }
        public PmxVertexData[] Vertices { get; }
        public int[] Indices { get; }
        public PmxMaterialData[] Materials { get; }
        public String[] Textures { get; }

        public ShotProperty Property { get; }
        public World World { get; }

        private static System.Diagnostics.Stopwatch Stopwatch { get; } = new System.Diagnostics.Stopwatch();

        public ShotModelData(World world, ShotProperty property)
        {
            Property = property;
            World = world;

            Bones = Property.Type.CreateBones(world, property);

            if (Property.Type.HasMesh)
            {
                Vertices = Property.Type.CreateVertices(world, property);
                Indices = Property.Type.CreateVertexIndices(world, property);
                Materials = Property.Type.CreateMaterials(world, property);
                Textures = Property.Type.CreateTextures(world, property);
            }
            else
            {
                Vertices = new PmxVertexData[0];
                Indices = new int[0];
                Materials = new PmxMaterialData[0];
                Textures = new string[0];
            }
        }

        public void AddMorph(PmxMorphData morph)
        {
            MorphDict.Add(morph.MorphName, morph);
            World.PmxModel.Morphs.MorphList.Add(morph);
        }
    }
}
