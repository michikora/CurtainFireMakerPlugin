﻿using System;
using CurtainFireMakerPlugin.Mathematics;
using CurtainFireMakerPlugin.BezierCurve;
using CsPmx.Data;
using CsVmd.Data;

namespace CurtainFireMakerPlugin.Entities
{
    public class EntityShot : Entity
    {
        public ShotProperty Property { get; }

        private Entity parentEntity;
        public override Entity ParentEntity
        {
            get => parentEntity;
            set
            {
                if ((this.parentEntity = value) is EntityShot entity && entity.Property.Type.RecordMotion)
                {
                    this.RootBone.ParentId = entity.RootBone.BoneId;
                }
            }
        }

        internal ShotModelData ModelData { get; }
        public PmxBoneData RootBone { get; }
        public PmxBoneData[] Bones { get; }
        public PmxMorphData MaterialMorph { get; }

        private bool ShouldRecord
        {
            get => this.RecordWhenVelocityChanges ? IsUpdatedVelocity : IsUpdatedPos;
            set => this.IsUpdatedVelocity = this.IsUpdatedPos = value;
        }
        private bool IsUpdatedVelocity { get; set; }
        private bool IsUpdatedPos { get; set; }

        private static float Epsilon { get; set; } = 0.00001F;

        public override Vector3 Velocity
        {
            get => base.Velocity;
            set => this.IsUpdatedVelocity |= !Vector3.EpsilonEquals(base.Velocity, (base.Velocity = value), Epsilon);
        }

        public override Vector3 Upward
        {
            get => base.Upward;
            set => this.IsUpdatedVelocity |= !Vector3.EpsilonEquals(base.Upward, (base.Upward = value), Epsilon);
        }

        public override Vector3 Pos
        {
            get => base.Pos;
            set => this.IsUpdatedPos |= !Vector3.EpsilonEquals(base.Pos, (base.Pos = value), Epsilon);
        }

        public override Quaternion Rot
        {
            get => base.Rot;
            set => this.IsUpdatedPos |= !Quaternion.EpsilonEquals(base.Rot, (base.Rot = value), Epsilon);
        }

        private bool recordWhenVelocityChanges = true;
        public bool RecordWhenVelocityChanges
        {
            get => recordWhenVelocityChanges;
            set
            {
                recordWhenVelocityChanges = value;
                if (recordWhenVelocityChanges)
                {
                    this.IsUpdatedVelocity |= this.IsUpdatedPos;
                }
                else
                {
                    this.IsUpdatedPos |= this.IsUpdatedVelocity;
                }
            }
        }

        public EntityShot(World world, string typeName, int color) : this(world, new ShotProperty(typeName, color)) { }
        public EntityShot(World world, string typeName, int color, Vector3 size) : this(world, new ShotProperty(typeName, color, size)) { }

        public EntityShot(World world, ShotProperty property) : base(world)
        {
            this.Property = property;

            this.Property.Type.Init(this);

            ModelData = this.World.AddShot(this);
            Bones = ModelData.Bones;
            RootBone = Bones[0];
            MaterialMorph = ModelData.MaterialMorph;
        }

        internal override void Frame()
        {
            if (this.FrameCount == 1 || this.ShouldRecord)
            {
                this.AddVmdMotion();
            }
            this.ShouldRecord = false;

            base.Frame();
        }

        protected override void UpdateRot()
        {
            if (this.RecordWhenVelocityChanges)
            {
                this.Rot = Matrix.LookAt(+this.Velocity, +this.Upward);
            }
            base.UpdateRot();
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            this.AddVmdMorph(-this.World.FrameCount, 1.0F, this.MaterialMorph);
            this.AddVmdMorph(0, 1.0F, this.MaterialMorph);
            this.AddVmdMorph(1, 0.0F, this.MaterialMorph);

            this.AddVmdMotion();
        }

        public override void OnDeath()
        {
            base.OnDeath();

            this.AddVmdMorph(-1, 0.0F, this.MaterialMorph);
            this.AddVmdMorph(0, 1.0F, this.MaterialMorph);

            this.AddVmdMotion();
        }

        public override void SetMotionBezier(Vector2 pos1, Vector2 pos2, int length)
        {
            this.AddVmdMotion();

            base.SetMotionBezier(pos1, pos2, length);
        }

        internal override void RemoveMotionBezier()
        {
            this.AddVmdMotion(true);

            base.RemoveMotionBezier();
        }

        public void AddVmdMotion(bool replace = true)
        {
            this.UpdateRot();

            var bezier = VmdBezierCurve.Line;

            if (this.motionInterpolation != null && this.motionInterpolation.startFrame < this.World.FrameCount)
            {
                bezier = this.motionInterpolation.curve;
            }

            this.AddVmdMotion(RootBone, Pos, Rot, bezier, replace);
        }

        public void AddVmdMotion(PmxBoneData bone, Vector3 pos, Quaternion rot, VmdBezierCurve bezier, bool replace = true)
        {
            var interpolation = new byte[4];
            interpolation[0] = (byte)(127 * bezier.P1.x);
            interpolation[1] = (byte)(127 * bezier.P1.y);
            interpolation[2] = (byte)(127 * bezier.P2.x);
            interpolation[3] = (byte)(127 * bezier.P2.y);

            var motion = new VmdMotionFrameData()
            {
                BoneName = bone.BoneName,
                KeyFrameNo = this.World.FrameCount,
                Pos = (DxMath.Vector3)pos,
                Rot = (DxMath.Quaternion)rot,
                InterpolatePointX = interpolation,
                InterpolatePointY = interpolation,
                InterpolatePointZ = interpolation,
                InterpolatePointR = interpolation
            };

            this.World.VmdMotion.AddVmdMotion(motion, replace);
        }

        public void AddVmdMorph(int frameOffset, float rate, PmxMorphData morph)
        {
            if (this.Property.Type.HasMmdData)
            {
                var frameData = new VmdMorphFrameData()
                {
                    MorphName = morph.MorphName,
                    KeyFrameNo = this.World.FrameCount + frameOffset,
                    Rate = (float)rate
                };
                this.World.VmdMotion.AddVmdMorph(frameData, morph);
            }
        }

        public PmxMorphData CreateVertexMorph(Func<Vector3, Vector3> func)
        {
            string vertexMorphName = "v" + this.MaterialMorph.MorphName;
            PmxMorphData morph = this.World.PmxModel.MorphList.Find(v => v.MorphName == vertexMorphName);

            if (morph == null)
            {
                var vertices = ModelData.Vertices;

                morph = new PmxMorphData()
                {
                    MorphName = vertexMorphName,
                    Type = PmxMorphData.MORPHTYPE_VERTEX,
                    MorphArray = new PmxMorphVertexData[vertices.Length]
                };

                for (int i = 0; i < morph.MorphArray.Length; i++)
                {
                    var vertex = vertices[i];
                    var vertexMorph = new PmxMorphVertexData()
                    {
                        Index = this.World.PmxModel.VertexList.IndexOf(vertex),
                        Position = (DxMath.Vector3)func(vertex.Pos)
                    };
                    morph.MorphArray[i] = vertexMorph;
                }

                this.World.PmxModel.MorphList.Add(morph);
            }

            return morph;
        }
    }
}
