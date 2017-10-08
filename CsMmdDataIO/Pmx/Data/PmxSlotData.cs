﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsMmdDataIO.Pmx.Data
{
    [Serializable]
    public class PmxSlotData : IPmxData
    {
        public const byte SLOT_TYPE_BONE = 0;
        public const byte SLOT_TYPE_MORPH = 1;

        public String SlotName { get; set; } = "";
        public String SlotNameE { get; set; } = "";

        public bool NormalSlot { get; set; } = true;
        public byte Type { get; set; }
        public int[] Indices { get; set; }

        public void Export(PmxExporter exporter)
        {
            exporter.WriteText(SlotName);
            exporter.WriteText(SlotNameE);

            exporter.Write((byte)(NormalSlot ? 0 : 1));

            int elementCount = Indices.Length;
            exporter.Write(elementCount);

            byte size = Type == SLOT_TYPE_BONE ? PmxExporter.SIZE_BONE : PmxExporter.SIZE_MORPH;

            for (int i = 0; i < elementCount; i++)
            {
                exporter.Write(Type);

                int id = Indices[i];
                exporter.WritePmxId(size, id);
            }
        }

        public void Parse(PmxParser parser)
        {
            SlotName = parser.ReadText();
            SlotNameE = parser.ReadText();

            NormalSlot = parser.ReadByte() == 0;

            int elementCount = parser.ReadInt32();
            Indices = new int[elementCount];

            for (int i = 0; i < elementCount; i++)
            {
                byte type = parser.ReadByte();
                byte size = type == SLOT_TYPE_BONE ? parser.SizeBone : parser.SizeMorph;

                Indices[i] = parser.ReadPmxId(size);
            }
        }
    }
}