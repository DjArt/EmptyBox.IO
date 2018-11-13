using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public class AudioGraph
    {
        public AudioGraph()
        {

        }

        public AudioLink Link(IAudioOutput output, IAudioInput input)
        {
            return new AudioLink(output, input);
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}