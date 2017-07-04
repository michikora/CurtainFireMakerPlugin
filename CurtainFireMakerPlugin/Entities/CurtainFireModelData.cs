﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPmx.Data;

namespace CurtainFireMakerPlugin.Entities
{
    internal class CurtainFireModelData
    {
        public List<int> indexList = new List<int>();
        public List<PmxVertexData> vertexList = new List<PmxVertexData>();
        public List<PmxMaterialData> materialList = new List<PmxMaterialData>();
        public List<PmxMorphData> morphList = new List<PmxMorphData>();
        public List<PmxBoneData> boneList = new List<PmxBoneData>();
        public List<string> textureList = new List<string>();

        public CurtainFireModelData()
        {
            PmxBoneData centerBone = new PmxBoneData();
            centerBone.boneName = "センター";
            centerBone.parentId = -1;
            centerBone.flag = 0x0002 | 0x0004 | 0x0008 | 0x0010;
            this.boneList.Add(centerBone);
        }

        public void SetupShotModelData(ShotModelData data, EntityShot entity)
        {
            if (data.Property.Type.RecordMotion())
            {
                if (data.Property.Type.HasMmdData())
                {
                    this.SetupShotModelData(data);
                }
                else
                {
                    this.SetupBone(data.bones[0]);
                }
            }
        }

        private void SetupShotModelData(ShotModelData data)
        {
            int[] indices = Array.ConvertAll(data.indices, i => i + this.vertexList.Capacity);
            this.indexList.AddRange(indices);

            PmxVertexData[] vertices = data.vertices;
            foreach (var vertex in vertices)
            {
                vertex.boneId = Array.ConvertAll(vertex.boneId, i => i + this.boneList.Capacity);
                this.vertexList.Add(vertex);
            }

            string[] textures = data.textures;
            foreach (var texture in textures)
            {
                if (!this.textureList.Contains(texture))
                {
                    this.textureList.Add(texture);
                }
            }

            PmxMaterialData[] materials = data.materials;
            PmxMorphData morph = data.morph;
            morph.morphName = this.morphList.Capacity.ToString();
            morph.type = 4;
            morph.morphArray = new PmxMorphMaterialData[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                morph.morphArray[i].Index = this.materialList.Capacity + i;
            }
            this.morphList.Add(morph);

            foreach (PmxMaterialData material in materials)
            {
                material.materialName = this.materialList.Capacity.ToString();
                material.textureId = textures.Length > 0 && material.textureId >= 0 ? this.textureList.IndexOf(textures[material.textureId]) : -1;
                this.materialList.Add(material);
            }
            this.SetupBone(data.bones);
        }

        private void SetupBone(params PmxBoneData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                PmxBoneData bone = data[i];

                bone.boneName = (this.boneList.Capacity + i - 1).ToString();
                bone.flag = 0x0001 | 0x0002 | 0x0004 | 0x0008 | 0x0010;
                bone.boneId = this.boneList.Capacity + i;

                if (bone.parentId <= 0)
                {
                    bone.parentId = 0;
                }
                else
                {
                    bone.parentId = this.boneList.IndexOf(data[bone.parentId]);
                }
            }
            this.boneList.AddRange(data);
        }
    }
}
