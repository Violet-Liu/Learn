using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace QZ.Foundation.Model.BitFld
{
    /// <summary>
    /// SizeOf(<seealso cref="MyStruct"/>) is 12
    /// </summary>
    public struct MyStruct
    {
        public byte a;  // 1 byte
        public int b;   // 4 bytes
        public short c; // 2 bytes
        public byte d;  // 1 byte
    }

    /// <summary>
    /// Forces the compiler to assign the structure sequentially as listed in the definition,
    /// which is what it does by default.
    /// Ohter values of LayoutKind are Auto, which lets compiler determine the layout, and Explicit, which
    /// lets the programmer specify the size of each field.
    /// Explicit is often used to create sequential memory layouts with no packing, but in most cases it is 
    /// simpler to use the Pack field. For example, if you specify Pack = 1 then the struct
    /// will be organised so that each field is on a byte boundary and can be read a byte at
    /// a time - i.e. no packing is necessary.
    /// <para>SizeOf(<seealso cref="MyStruct1"/>) is 12</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MyStruct1
    {
        public byte a;  // 1 byte
        public int b;   // 4 bytes
        public short c; // 2 bytes
        public byte d;  // 1 byte
    }

    /// <summary>
    /// SizeOf(<seealso cref="MyStruct2"/>) is 8
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MyStruct2
    {
        public byte a;  // 1 byte
        public int b;   // 4 bytes
        public short c; // 2 bytes
        public byte d;  // 1 byte
    }

    // <summary>
    /// SizeOf(<seealso cref="MyStruct3"/>) is 10
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct MyStruct3
    {
        public byte a;  // 1 byte
        public int b;   // 4 bytes
        public short c; // 2 bytes
        public byte d;  // 1 byte
    }

    // It is also worth mentioning that you can modify the way a struct is packed by
    // simply reordering its fields. For example,
    /// <summary>
    /// According to that compiler likes data to be aligned on address boundaries that
    /// are the same size as the data.
    /// <para>SizeOf(<seealso cref="MyStruct4"/>) is 8</para>
    /// </summary>
    public struct MyStruct4
    {
        public byte a;  // 1 byte
        public byte d;  // 1 byte
        public short c; // 2 bytes
        public int b;   // 4 bytes 
    }

    /// <summary>
    /// In order to describe, SizeOf(<seealso cref="MyStruct5"/>) is also 8
    /// </summary>
    public struct MyStruct5
    {
        public byte a;  // 1 byte
        public short c; // 2 bytes
        public int b;   // 4 bytes 
    }

    // Being exact.
    /// <summary>
    /// SizeOf(<seealso cref="MyStruct6"/>) is 8 bytes
    /// <para>it is equivalent to Pack=1</para>
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MyStruct6
    {
        [FieldOffset(0)]
        public byte a;  // 1 byte
        [FieldOffset(1)]
        public int b;   // 4 bytes
        [FieldOffset(5)]
        public short c; // 2 bytes
        [FieldOffset(7)]
        public byte d;  // 1 byte
    }

    // Explicit really does give you complete control should you need it.
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct MyStruct7
    {
        [FieldOffset(0)]
        public byte a;  // 1 byte
        [FieldOffset(1)]
        public int b;   // 4 bytes
        [FieldOffset(10)]
        public short c; // 2 bytes
        [FieldOffset(14)]
        public byte d;  // 1 byte
    }
}
