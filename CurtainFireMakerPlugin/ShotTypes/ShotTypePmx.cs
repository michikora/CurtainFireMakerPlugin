﻿using System;
using CsPmx;
using CsPmx.Data;
using CurtainFireMakerPlugin.Entities;
using VecMath;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CurtainFireMakerPlugin.ShotTypes
{
    public class ShotTypePmx : ShotType
    {
        private PmxModelData Data { get; } = new PmxModelData();
        private Vector3 VertexScale { get; }

        public Action<ShotProperty, PmxMaterialData[]> InitMaterials { get; set; } = (prop, materials) =>
        {
            foreach (var material in materials)
            {
                material.Diffuse = new DxMath.Vector4(prop.Red, prop.Green, prop.Blue, 1.0F);
                material.Ambient = new DxMath.Vector3(prop.Red, prop.Green, prop.Blue);
            }
        };

        public ShotTypePmx(string name, string path, float size) : this(name, path, new Vector3(size, size, size)) { }

        public ShotTypePmx(string name, string path, Vector3 size) : base(name)
        {
            var inStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (inStream)
            {
                PmxParser parser = new PmxParser(inStream);
                parser.Parse(this.Data);
            }

            VertexScale = size;
        }

        public override PmxVertexData[] CreateVertices(ShotProperty property)
        {
            PmxVertexData[] result = new PmxVertexData[Data.VertexArray.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = DeepCopy(Data.VertexArray[i]);
                result[i].Pos.x *= VertexScale.x;
                result[i].Pos.y *= VertexScale.y;
                result[i].Pos.z *= VertexScale.z;
            }
            return result;
        }

        public override int[] CreateVertexIndices(ShotProperty property)
        {
            var result = new int[this.Data.VertexIndices.Length];
            Array.Copy(this.Data.VertexIndices, result, this.Data.VertexIndices.Length);

            return result;
        }

        public override PmxMaterialData[] CreateMaterials(ShotProperty property)
        {
            PmxMaterialData[] result = new PmxMaterialData[this.Data.MaterialArray.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = DeepCopy(this.Data.MaterialArray[i]);
            }
            InitMaterials(property, result);
            return result;
        }

        public override String[] CreateTextures(ShotProperty property)
        {
            String[] result = new String[this.Data.TextureFiles.Length];
            Array.Copy(this.Data.TextureFiles, result, this.Data.TextureFiles.Length);

            return result;
        }

        public override PmxBoneData[] CreateBones(ShotProperty property)
        {
            PmxBoneData[] result = new PmxBoneData[this.Data.BoneArray.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = DeepCopy(this.Data.BoneArray[i]);
            }
            return result;
        }

        public static T DeepCopy<T>(T target)
        {
            T result;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();

            try
            {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T)b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }

            return result;
        }
    }
}
