﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsPmx.Data;
using CsPmx;
using CurtainFireMakerPlugin.Mathematics;

namespace CurtainFireMakerPlugin.Entities
{
    internal class CurtainFireModel
    {
        public List<int> IndexList { get; } = new List<int>();
        public List<PmxVertexData> VertexList { get; } = new List<PmxVertexData>();
        public List<PmxMaterialData> MaterialList { get; } = new List<PmxMaterialData>();
        public List<PmxMorphData> MorphList { get; } = new List<PmxMorphData>();
        public List<PmxBoneData> BoneList { get; } = new List<PmxBoneData>();
        public List<string> TextureList { get; } = new List<string>();

        public CurtainFireModel()
        {
            PmxBoneData centerBone = new PmxBoneData();
            centerBone.BoneName = "センター";
            centerBone.ParentId = -1;
            centerBone.Flag = 0x0002 | 0x0004 | 0x0008 | 0x0010;
            this.BoneList.Add(centerBone);
        }

        public void InitShotModelData(ShotModelData data)
        {
            if (data.Property.Type.RecordMotion())
            {
                if (data.Property.Type.HasMmdData())
                {
                    this.SetupShotModelData(data);
                }
                else
                {
                    this.SetupBone(data, data.Bones[0]);
                }
            }
        }

        private void SetupShotModelData(ShotModelData data)
        {
            int[] indices = Array.ConvertAll(data.Indices, i => i + this.VertexList.Count);
            this.IndexList.AddRange(indices);

            PmxVertexData[] vertices = data.Vertices;
            foreach (var vertex in vertices)
            {
                vertex.BoneId = Array.ConvertAll(vertex.BoneId, i => i + this.BoneList.Count);
                this.VertexList.Add(vertex);
            }

            string[] textures = data.Textures;
            foreach (var texture in textures)
            {
                if (!this.TextureList.Contains(texture))
                {
                    this.TextureList.Add(texture);
                }
            }

            PmxMaterialData[] materials = data.Materials;
            PmxMorphData morph = data.MaterialMorph;
            morph.MorphName = data.Property.Type.Name + this.MorphList.Count.ToString();
            morph.Type = 4;
            morph.MorphArray = ArrayUtil.Set(new PmxMorphMaterialData[materials.Length], i => new PmxMorphMaterialData());

            for (int i = 0; i < materials.Length; i++)
            {
                morph.MorphArray[i].Index = this.MaterialList.Count + i;
            }
            this.MorphList.Add(morph);

            foreach (PmxMaterialData material in materials)
            {
                material.MaterialName = data.Property.Type.Name + this.MaterialList.Count.ToString();
                material.Diffuse = new DxMath.Vector4(data.Property.Red, data.Property.Green, data.Property.Blue, 1.0F);
                material.Ambient = new DxMath.Vector3(data.Property.Red, data.Property.Green, data.Property.Blue);
                material.TextureId = textures.Length > 0 && material.TextureId >= 0 ? this.TextureList.IndexOf(textures[material.TextureId]) : -1;
                this.MaterialList.Add(material);
            }
            this.SetupBone(data, data.Bones);
        }

        private void SetupBone(ShotModelData data, params PmxBoneData[] bones)
        {
            for (int i = 0; i < bones.Length; i++)
            {
                PmxBoneData bone = bones[i];

                bone.BoneName = data.Property.Type.Name + (this.BoneList.Count - 1).ToString();
                bone.Flag = 0x0001 | 0x0002 | 0x0004 | 0x0010;
                bone.BoneId = this.BoneList.Count + i;

                if (-1 < bone.ParentId && bone.ParentId < bones.Length)
                {
                    bone.ParentId = this.BoneList.IndexOf(bones[bone.ParentId]);
                }
                else
                {
                    bone.ParentId = 0;
                }
                this.BoneList.Add(bone);
            }
        }

        public void GetData(PmxModelData data)
        {
            var header = new PmxHeaderData()
            {
                Version = 2.0F
            };

            var boneSlot = new PmxSlotData()
            {
                SlotName = "弾ボーン",
                Type = PmxSlotData.SLOT_TYPE_BONE,
                Indices = Enumerable.Range(0, this.BoneList.Count).ToArray()
            };

            var morphSlot = new PmxSlotData()
            {
                SlotName = "弾モーフ",
                Type = PmxSlotData.SLOT_TYPE_MORPH,
                Indices = Enumerable.Range(0, this.MorphList.Count).ToArray()
            };

            data.Header = header;
            data.VertexIndices = this.IndexList.ToArray();
            data.TextureFiles = this.TextureList.ToArray();
            data.VertexArray = this.VertexList.ToArray();
            data.MaterialArray = this.MaterialList.ToArray();
            data.BoneArray = this.BoneList.ToArray();
            data.MorphArray = this.MorphList.ToArray();
            data.SlotArray = new PmxSlotData[] { boneSlot, morphSlot };
        }
    }
}
