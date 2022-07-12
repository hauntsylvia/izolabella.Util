using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace izolabella.Util.Music
{
    public class GetWavInfo
    {
        public GetWavInfo(string File)
        {
            WaveFileReader R = new (File);
            this.File = File;
            this.Duration = R.TotalTime;
            R.Close();
        }

        public string File { get; }

        public TimeSpan Duration { get; }
    }
}
