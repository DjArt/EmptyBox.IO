using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public sealed class SoftwareAudioChunk : IAudioChunk
    {
        [StructLayout(LayoutKind.Explicit)]
        private unsafe struct BF : IDisposable
        {
            [FieldOffset(0)]
            public byte* ByteBuffer;
            [FieldOffset(0)]
            public float* FloatBuffer;
            
            public void Allocate(uint size)
            {
                if (ByteBuffer != null)
                {
                    Deallocate();
                }
                IntPtr pos = Marshal.AllocHGlobal((int)size);
                ByteBuffer = (byte*)pos;
            }

            public void Deallocate()
            {
                Marshal.FreeHGlobal(new IntPtr(ByteBuffer));
            }

            public void Dispose()
            {
                Deallocate();
            }
        }

        private const uint BYTE_FLOAT_RATIO = sizeof(float) / sizeof(byte);

        private BF _Buffer;

        public float this[uint index]
        {
            get
            {
                if (index < Size)
                {
                    unsafe
                    {
                        return _Buffer.FloatBuffer[index];
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (index < Size)
                {
                    unsafe
                    {
                        _Buffer.FloatBuffer[index] = value;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public uint Size { get; private set; }

        public SoftwareAudioChunk(float[] buffer)
        {
            Size = (uint)buffer.Length;
            unsafe
            {
                _Buffer.Allocate((uint)buffer.Length * BYTE_FLOAT_RATIO);
                for (int i0 = 0; i0 < buffer.Length; i0++)
                {
                    _Buffer.FloatBuffer[i0] = buffer[i0];
                }
            }
        }

        public SoftwareAudioChunk(byte[] buffer)
        {
            Size = (uint)buffer.Length / BYTE_FLOAT_RATIO;
            unsafe
            {
                _Buffer.Allocate((uint)buffer.Length);
                for (int i0 = 0; i0 < buffer.Length; i0++)
                {
                    _Buffer.ByteBuffer[i0] = buffer[i0];
                }
            }
        }

        public unsafe SoftwareAudioChunk(byte* buffer, uint size, bool allocNew = false)
        {
            Size = size / BYTE_FLOAT_RATIO;
            if (!allocNew)
            {
                _Buffer.ByteBuffer = buffer;
            }
            else
            {
                _Buffer.Allocate(size);
                for (int i0 = 0; i0 < size; i0++)
                {
                    _Buffer.ByteBuffer[i0] = buffer[i0];
                }
            }
        }

        public unsafe SoftwareAudioChunk(float* buffer, uint size, bool allocNew = false)
        {
            Size = size;
            if (!allocNew)
            {
                _Buffer.FloatBuffer = buffer;
            }
            else
            {
                _Buffer.Allocate(size * BYTE_FLOAT_RATIO);
                for (int i0 = 0; i0 < size; i0++)
                {
                    _Buffer.FloatBuffer[i0] = buffer[i0];
                }
            }
        }

        ~SoftwareAudioChunk()
        {
            Dispose();
        }

        public unsafe byte* GetRawBuffer()
        {
            return _Buffer.ByteBuffer;
        }

        public byte[] GetBuffer()
        {
            byte[] buffer = new byte[Size * BYTE_FLOAT_RATIO];
            unsafe
            {
                for (int i0 = 0; i0 < Size * BYTE_FLOAT_RATIO; i0++)
                {
                   buffer[i0] = _Buffer.ByteBuffer[i0];
                }
            }
            return buffer;
        }

        public void Dispose()
        {
            _Buffer.Dispose();
        }
    }
}
