using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPlayerConsoleApp
{
    public class MidiPixelSet
    {
        public TimingPixel Timing { get; set; }
        public TonePixel Tone { get; set; }

        public MidiPixelSet(Note note, double AverageNoteLength, double MaxTime)
        {
            Timing = new(note, AverageNoteLength, MaxTime);
            Tone = new(note, AverageNoteLength);
        }

        public MidiPixelSet()
        {

        }

    }
}
