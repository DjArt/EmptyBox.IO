using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Audio
{
    public interface IAudioDevice : IDevice
    {
        byte BitDepth { get; }
        uint SampleRate { get; }
        float Volume { get; }
        IEnumerable<byte> SupportedBitDepths { get; }
        IEnumerable<uint> SupportedSampleRates { get; }

        Task<VoidResult<AccessStatus>> TrySetSampleRate(uint rate);
        Task<VoidResult<AccessStatus>> TrySetBitDepth(byte depth);
        Task<VoidResult<AccessStatus>> TrySetVolume(float volume);
    }
}
