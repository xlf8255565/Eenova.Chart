// Type: System.Math
// Assembly: mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
// Assembly location: C:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
    public static class Math
    {
        public const double PI = 3.14159265358979;
        public const double E = 2.71828182845905;

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Acos(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Asin(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Atan(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Atan2(double y, double x);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Ceiling(double a);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Cos(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Cosh(double value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Floor(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Sin(double a);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Tan(double a);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Sinh(double value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Tanh(double value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Round(double a);

        public static double Round(double value, int digits);
        public static decimal Round(decimal d);
        public static decimal Round(decimal d, int decimals);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Sqrt(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Log(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Log10(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Exp(double d);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Pow(double x, double y);

        [SecuritySafeCritical]
        public static double IEEERemainder(double x, double y);

        [CLSCompliant(false)]
        public static sbyte Abs(sbyte value);

        public static short Abs(short value);
        public static int Abs(int value);
        public static long Abs(long value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static float Abs(float value);

        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static double Abs(double value);

        public static decimal Abs(decimal value);

        [CLSCompliant(false)]
        public static sbyte Max(sbyte val1, sbyte val2);

        public static byte Max(byte val1, byte val2);
        public static short Max(short val1, short val2);

        [CLSCompliant(false)]
        public static ushort Max(ushort val1, ushort val2);

        public static int Max(int val1, int val2);

        [CLSCompliant(false)]
        public static uint Max(uint val1, uint val2);

        public static long Max(long val1, long val2);

        [CLSCompliant(false)]
        public static ulong Max(ulong val1, ulong val2);

        public static float Max(float val1, float val2);
        public static double Max(double val1, double val2);
        public static decimal Max(decimal val1, decimal val2);

        [CLSCompliant(false)]
        public static sbyte Min(sbyte val1, sbyte val2);

        public static byte Min(byte val1, byte val2);
        public static short Min(short val1, short val2);

        [CLSCompliant(false)]
        public static ushort Min(ushort val1, ushort val2);

        public static int Min(int val1, int val2);

        [CLSCompliant(false)]
        public static uint Min(uint val1, uint val2);

        public static long Min(long val1, long val2);

        [CLSCompliant(false)]
        public static ulong Min(ulong val1, ulong val2);

        public static float Min(float val1, float val2);
        public static double Min(double val1, double val2);
        public static decimal Min(decimal val1, decimal val2);
        public static double Log(double a, double newBase);

        [CLSCompliant(false)]
        public static int Sign(sbyte value);

        public static int Sign(short value);
        public static int Sign(int value);
        public static int Sign(long value);
        public static int Sign(float value);
        public static int Sign(double value);
        public static int Sign(decimal value);
    }
}
