using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace izolabella.Util.Music
{
    public class WAVEInformation
    {
        [JsonConstructor]
        public WAVEInformation(short ChannelCount,
                               int SampleRate,
                               int LengthInBytes,
                               int BytesPerSecond,
                               short BitsPerSample,
                               TimeSpan FileDuration)
        {
            this.ChannelCount = ChannelCount;
            this.SampleRate = SampleRate;
            this.LengthInBytes = LengthInBytes;
            this.BytesPerSecond = BytesPerSecond;
            this.BitsPerSample = BitsPerSample;
            this.FileDuration = FileDuration;
        }

        public WAVEInformation(string FileLocation)
        {
            if (File.Exists(FileLocation))
            {
                byte[] FullData = new byte[44];
                using StreamReader Reader = new(FileLocation);
                Reader.BaseStream.Read(FullData, 0, FullData.Length);
                string RIFF = Encoding.UTF8.GetString(FullData[..4]);
                string WAVE = Encoding.UTF8.GetString(FullData.Skip(8).Take(4).ToArray());
                if (RIFF.ToUpper() != "RIFF" || WAVE.ToUpper() != "WAVE")
                {
                    throw new ArgumentException("The provided file is not in .wav format.", paramName: nameof(FileLocation));
                }
                this.LengthInBytes = BitConverter.ToInt32(FullData.Skip(4).Take(4).ToArray());
                this.ChannelCount = BitConverter.ToInt16(FullData.Skip(22).Take(2).ToArray());
                this.SampleRate = BitConverter.ToInt32(FullData.Skip(24).Take(4).ToArray());
                this.BitsPerSample = BitConverter.ToInt16(FullData.Skip(34).Take(2).ToArray());
                // 32,218,820
                // 32,218,828
                this.BytesPerSecond = BitConverter.ToInt32(FullData.Skip(28).Take(4).ToArray());
                this.FileDuration = TimeSpan.FromSeconds(this.LengthInBytes / this.BytesPerSecond);
            }
        }

        [JsonProperty]
        public short ChannelCount { get; }

        [JsonProperty]
        public int SampleRate { get; }

        [JsonProperty]
        public int LengthInBytes { get; }

        [JsonProperty]
        public long BytesPerSecond { get; }

        [JsonProperty]
        public short BitsPerSample { get; }

        [JsonProperty]
        public TimeSpan FileDuration { get; }
    }
}
