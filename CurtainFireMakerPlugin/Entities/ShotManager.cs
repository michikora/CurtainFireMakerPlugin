﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CurtainFireMakerPlugin.ShotTypes;
using CurtainFireMakerPlugin.Collections;
using CsPmx.Data;
using CsPmx;
using CsVmd.Data;

namespace CurtainFireMakerPlugin.Entities
{
    internal class ShotManager
    {
        private Dictionary<ShotType, ShotTypeGroup> TypeGroupDict { get; } = new Dictionary<ShotType, ShotTypeGroup>();
        private World World { get; }

        public ShotManager(World world)
        {
            this.World = world;
        }

        public ShotModelData AddEntity(EntityShot entity)
        {
            if (!this.TypeGroupDict.ContainsKey(entity.Property.Type))
            {
                this.TypeGroupDict[entity.Property.Type] = new ShotTypeGroup(entity.Property.Type, this.World);
            }

            ShotTypeGroup typeGroup = this.TypeGroupDict[entity.Property.Type];

            ShotModelData data = typeGroup.AddEntityToGroup(entity);
            if (data == null)
            {
                data = typeGroup.CreateGroup(entity);
                this.World.PmxModel.InitShotModelData(data);
            }

            return data;
        }
    }

    internal class ShotTypeGroup
    {
        private ShotType Type { get; }
        private List<ShotGroup> GroupList { get; } = new List<ShotGroup>();

        private World World { get; }

        public ShotTypeGroup(ShotType type, World world)
        {
            this.Type = type;
            this.World = world;
        }

        public ShotModelData AddEntityToGroup(EntityShot entity)
        {
            foreach (ShotGroup group in this.GroupList)
            {
                if (group.IsAddable(entity))
                {
                    group.AddEntity(entity);
                    return group.Data;
                }
            }
            return null;
        }

        public ShotModelData CreateGroup(EntityShot entity)
        {
            ShotGroup group = new ShotGroup(entity.Property, World);
            group.AddEntity(entity);
            this.GroupList.Add(group);

            return group.Data;
        }
    }

    internal class ShotGroup
    {
        public List<EntityShot> ShotList { get; } = new List<EntityShot>();
        public ShotModelData Data { get; }

        private ShotProperty Property { get; }
        private World World { get; }

        public ShotGroup(ShotProperty property, World world)
        {
            this.Property = property;
            World = world;

            this.Data = new ShotModelData(World, this.Property);
        }

        public bool IsAddable(EntityShot entity)
        {
            return this.Property.Equals(entity.Property) && !this.ShotList.Exists(e => !e.IsDeath);
        }

        public void AddEntity(EntityShot entity)
        {
            this.ShotList.Add(entity);
        }
    }
}
