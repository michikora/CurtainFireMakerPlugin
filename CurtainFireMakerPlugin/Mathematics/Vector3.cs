﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurtainFireMakerPlugin.Mathematics
{
    public struct Vector3
    {
        public double x;
        public double y;
        public double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector3 v1) : this(v1.x, v1.y, v1.z)
        {

        }

        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            var v3 = new Vector3();

            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            v3.z = v1.z + v2.z;

            return v3;
        }

        public static Vector3 Sub(Vector3 v1, Vector3 v2)
        {
            var v3 = new Vector3();

            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            v3.z = v1.z - v2.z;

            return v3;
        }

        public static Vector3 Scale(Vector3 v1, double d1)
        {
            var v3 = new Vector3();

            v3.x = v1.x * d1;
            v3.y = v1.y * d1;
            v3.z = v1.z * d1;

            return v3;
        }

        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return v2.x * v1.x + v2.y * v1.y + v2.z * v1.z;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            var v3 = new Vector3();

            v3.x = v1.y * v2.z - v1.z * v2.y;
            v3.y = v1.z * v2.x - v1.x * v2.z;
            v3.z = v1.x * v2.y - v1.y * v2.x;

            return v3;
        }

        public static Vector3 Transform(Matrix m1, Vector3 v1)
        {
            var v2 = new Vector3();

            v2.x = m1.m00 * v1.x + m1.m01 * v1.y + m1.m02 * v1.z + m1.m03;
            v2.y = m1.m10 * v1.x + m1.m11 * v1.y + m1.m12 * v1.z + m1.m13;
            v2.z = m1.m20 * v1.x + m1.m21 * v1.y + m1.m22 * v1.z + m1.m23;

            return v2;
        }

        public static Vector3 TransformNormal(Matrix m1, Vector3 v1)
        {
            var v2 = new Vector3();

            v2.x = m1.m00 * v1.x + m1.m01 * v1.y + m1.m02 * v1.z;
            v2.y = m1.m10 * v1.x + m1.m11 * v1.y + m1.m12 * v1.z;
            v2.z = m1.m20 * v1.x + m1.m21 * v1.y + m1.m22 * v1.z;

            return v2;
        }

        public static Vector3 Normalize(Vector3 v1)
        {
            var v2 = new Vector3();

            double len = Length(v1);

            if (len != 1.0 && len != 0.0)
            {
                v2.x = v1.x / len;
                v2.y = v1.y / len;
                v2.z = v1.z / len;
            }

            return v2;
        }

        public static double Length(Vector3 v1)
        {
            return Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        }

        public double Length()
        {
            return Length(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Vector3?;
            if ((System.Object)p == null)
            {
                return false;
            }

            return this.Equals((Vector3)obj);
        }

        public bool Equals(Vector3 v1) => v1.x == this.x && v1.y == this.y && v1.z == this.z;

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
        }

        public static Vector3 operator -(Vector3 v1) => v1 * -1;

        public static Vector3 operator +(Vector3 v1) => Normalize(v1);

        public static bool operator ==(Vector3 v1, Vector3 v2) => v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;

        public static bool operator !=(Vector3 v1, Vector3 v2) => !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);

        public static Vector3 operator +(Vector3 v1, Vector3 v2) => Add(v1, v2);

        public static Vector3 operator -(Vector3 v1, Vector3 v2) => Sub(v1, v2);

        public static Vector3 operator *(Vector3 v1, double d1) => Scale(v1, d1);

        public static double operator *(Vector3 v1, Vector3 v2) => Dot(v1, v2);

        public static Vector3 operator *(Matrix m1, Vector3 v1) => TransformNormal(m1, v1);

        public static Vector3 operator /(Vector3 v1, double d1) => Scale(v1, 1.0 / d1);

        public static Vector3 operator ^(Vector3 v1, Vector3 v2) => Cross(v1, v2);

        public static explicit operator DxMath.Vector3(Vector3 v1) => new DxMath.Vector3((float)v1.x, (float)v1.y, (float)v1.z);

        public static implicit operator Vector3(DxMath.Vector3 v1) => new Vector3(v1.X, v1.Y, v1.Z);
    }
}