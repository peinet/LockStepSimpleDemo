using System;
using UnityEngine;

namespace FixMath
{
    public struct FixVec3
    {
        public static readonly FixVec3 Zero = new FixVec3();
        public static readonly FixVec3 One = new FixVec3(1, 1, 1);
        public static readonly FixVec3 Left = new FixVec3(1, 0, 0);
        public static readonly FixVec3 Up = new FixVec3(0, 1, 0);
        public static readonly FixVec3 Front = new FixVec3(0, 0, 1);

        public static implicit operator FixVec3(FixVec2 value)
        {
            return new FixVec3(value.X, value.Y, Fix64.Zero);
        }
        public static explicit operator FixVec3(Vector2 value)
        {
            return new FixVec3(value.x, value.y, 0);
        }
        public static explicit operator FixVec3(Vector3 value)
        {
            return new FixVec3(value.x, value.y, value.z);
        }

        public static FixVec3 operator +(FixVec3 rhs)
        {
            return rhs;
        }
        public static FixVec3 operator -(FixVec3 rhs)
        {
            return new FixVec3(-rhs._x, -rhs._y, -rhs._z);
        }

        public static FixVec3 operator +(FixVec3 lhs, FixVec3 rhs)
        {
            return new FixVec3(lhs._x + rhs._x, lhs._y + rhs._y, lhs._z + rhs._z);
        }
        public static FixVec3 operator -(FixVec3 lhs, FixVec3 rhs)
        {
            return new FixVec3(lhs._x - rhs._x, lhs._y - rhs._y, lhs._z - rhs._z);
        }

        public static FixVec3 operator +(FixVec3 lhs, Fix64 rhs)
        {
            return lhs.ScalarAdd(rhs);
        }
        public static FixVec3 operator +(Fix64 lhs, FixVec3 rhs)
        {
            return rhs.ScalarAdd(lhs);
        }
        public static FixVec3 operator -(FixVec3 lhs, Fix64 rhs)
        {
            return new FixVec3(lhs._x - rhs, lhs._y - rhs, lhs._z - rhs);
        }
        public static FixVec3 operator *(FixVec3 lhs, Fix64 rhs)
        {
            return lhs.ScalarMultiply(rhs);
        }
        public static FixVec3 operator *(Fix64 lhs, FixVec3 rhs)
        {
            return rhs.ScalarMultiply(lhs);
        }
        public static FixVec3 operator /(FixVec3 lhs, Fix64 rhs)
        {
            return new FixVec3(lhs._x / rhs, lhs._y / rhs, lhs._z / rhs);
        }

        private Fix64 _x, _y, _z;

        public FixVec3(float x, float y, float z)
        {
            _x = (Fix64)x;
            _y = (Fix64)y;
            _z = (Fix64)z;
        }
        public FixVec3(Fix64 x, Fix64 y, Fix64 z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public FixVec3(Vector3 v)
        {
            _x = (Fix64)(v.x);
            _y = (Fix64)(v.y);
            _z = (Fix64)(v.z);
        }

        public Fix64 X { get { return _x; } }
        public Fix64 Y { get { return _y; } }
        public Fix64 Z { get { return _z; } }

        public Fix64 Dot(FixVec3 rhs)
        {
            return _x * rhs._x + _y * rhs._y + _z * rhs._z;
        }

        public FixVec3 Cross(FixVec3 rhs)
        {
            return new FixVec3(
                _y * rhs._z - _z * rhs._y,
                _z * rhs._x - _x * rhs._z,
                _x * rhs._y - _y * rhs._x
            );
        }

        FixVec3 ScalarAdd(Fix64 value)
        {
            return new FixVec3(_x + value, _y + value, _z + value);
        }
        FixVec3 ScalarMultiply(Fix64 value)
        {
            return new FixVec3(_x * value, _y * value, _z * value);
        }

        public Fix64 GetMagnitude()
        {
            return Fix64.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        public FixVec3 Normalize()
        {
            if (_x == Fix64.Zero && _y == Fix64.Zero && _z == Fix64.Zero)
                return FixVec3.Zero;

            var m = GetMagnitude();
            return new FixVec3(_x / m, _y / m, _z / m);
        }

        public Vector3 ToVector3()
        {
            return new Vector3((float)_x, (float)_y, (float)_z);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", _x, _y, _z);
        }
    }
}

