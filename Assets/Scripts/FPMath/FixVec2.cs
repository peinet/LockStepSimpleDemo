using System;
using UnityEngine;

namespace FixMath
{
    public struct FixVec2
    {
        public static readonly FixVec2 Zero = new FixVec2();
        public static readonly FixVec2 One = new FixVec2(1, 1);
        public static readonly FixVec2 Left = new FixVec2(1, 0);
        public static readonly FixVec2 Up = new FixVec2(0, 1);

        public static explicit operator FixVec2(Vector2 value)
        {
            return new FixVec2(value.x, value.y);
        }
        public static explicit operator FixVec2(Vector3 value)
        {
            return new FixVec2(value.x, value.y);
        }

        public static FixVec2 operator +(FixVec2 rhs)
        {
            return rhs;
        }
        public static FixVec2 operator -(FixVec2 rhs)
        {
            return new FixVec2(-rhs._x, -rhs._y);
        }

        public static FixVec2 operator +(FixVec2 lhs, FixVec2 rhs)
        {
            return new FixVec2(lhs._x + rhs._x, lhs._y + rhs._y);
        }
        public static FixVec2 operator -(FixVec2 lhs, FixVec2 rhs)
        {
            return new FixVec2(lhs._x - rhs._x, lhs._y - rhs._y);
        }

        public static FixVec2 operator +(FixVec2 lhs, Fix64 rhs)
        {
            return lhs.ScalarAdd(rhs);
        }
        public static FixVec2 operator +(Fix64 lhs, FixVec2 rhs)
        {
            return rhs.ScalarAdd(lhs);
        }
        public static FixVec2 operator -(FixVec2 lhs, Fix64 rhs)
        {
            return new FixVec2(lhs._x - rhs, lhs._y - rhs);
        }
        public static FixVec2 operator *(FixVec2 lhs, Fix64 rhs)
        {
            return lhs.ScalarMultiply(rhs);
        }
        public static FixVec2 operator *(Fix64 lhs, FixVec2 rhs)
        {
            new Vector2();
            return rhs.ScalarMultiply(lhs);
        }
        public static FixVec2 operator /(FixVec2 lhs, Fix64 rhs)
        {
            return new FixVec2(lhs._x / rhs, lhs._y / rhs);
        }

        Fix64 _x, _y;
        public FixVec2(float x, float y)
        {
            _x = (Fix64)x;
            _y = (Fix64)y;
        }
        public FixVec2(Fix64 x, Fix64 y)
        {
            _x = x;
            _y = y;
        }
        public FixVec2(Vector2 v)
        {
            _x = (Fix64)(v.x);
            _y = (Fix64)(v.y);
        }

        public Fix64 X { get { return _x; } }
        public Fix64 Y { get { return _y; } }

        public Fix64 Dot(FixVec2 rhs)
        {
            return _x * rhs._x + _y * rhs._y;
        }

        public Fix64 Cross(FixVec2 rhs)
        {
            return _x * rhs._y - _y * rhs._x;
        }

        FixVec2 ScalarAdd(Fix64 value)
        {
            return new FixVec2(_x + value, _y + value);
        }
        FixVec2 ScalarMultiply(Fix64 value)
        {
            return new FixVec2(_x * value, _y * value);
        }

        public Fix64 GetMagnitude()
        {
            return Fix64.Sqrt(_x * _x + _y * _y );
        }

        public FixVec2 Normalize()
        {
            if (_x == Fix64.Zero && _y == Fix64.Zero)
                return FixVec2.Zero;

            var m = GetMagnitude();
            return new FixVec2(_x / m, _y / m);
        }

        public Vector2 ToVector2()
        {
            return new Vector2((float)_x, (float)_y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", _x, _y);
        }
    }
}
