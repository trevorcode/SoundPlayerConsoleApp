using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    public class NotePixel
    {
        public Color Color { get; set; }
    }

    public class TimingPixel : NotePixel
    {
        public long Time { get; set; }

        public TimingPixel(Note note, double AverageNoteLength, double MaxTime)
        {
            Time = note.Time;


            var red = (note.Time / MaxTime) * 255;
            var green = (red % 1) * 255;
            var blue = (green % 1) * 255;

            //count += (redByte / 255) * MaxTime;
            //long count = (long)(((blueByte / (double)(255^3)) + (greenByte / (double)(255^2)) + ((redByte / 255.0))) * MaxTime);
          
            //var hue = ((note.Time / MaxTime) * 360);
            //byte lightness = (byte)Math.Min(((hue % 360) ) * 100, 100);
            //byte saturation = (byte)Math.Min(((hue % 360) % 100), 100);
            ////byte saturation = (byte)Math.Min(((1.0 / note.Length) * AverageNoteLength) * 50, 100);
            //var rgb = ColorHelper.ColorConverter.HslToRgb(new ColorHelper.HSL((int)hue, (byte)saturation, (byte)lightness));
            Color = Color.FromArgb((byte)red, (byte)green, (byte)blue);
           

            //var hsl = ColorHelper.ColorConverter.RgbToHsl(new ColorHelper.RGB(Color.R, Color.G, Color.B));

            //var tempTime = (long)(((hsl.H / 360.0) * MaxTime) + (double)hsl.L + (double)hsl.S);
        }

        public TimingPixel(int r, int g, int b, long MaxTime)
        {
            Color = Color.FromArgb(r, g, b);

            var count = b / 255.0;
            count += g;
            count /= 255.0;
            count += r;
            count /= 255.0;
            count *= MaxTime;

            Time = (long)count;
        }
    }

    public class TonePixel : NotePixel
    {
        public SevenBitNumber NoteNumber { get; set; }
        public int Octave { get; set; }
        public long Length { get; set; }
        public TonePixel(Note note, double AverageNoteLength)
        {
            NoteNumber = note.NoteNumber;
            Octave = note.Octave;
            Length = note.Length;

            int hue = (int)(((note.NoteNumber % 12.0) / 12.0) * 360);
            byte saturation = (byte)Math.Min(((1.0 / note.Length) * AverageNoteLength) * 50, 100);
            byte lightness = (byte)Math.Min((note.Octave / 8.0) * 100, 100);

            var rgb = ColorHelper.ColorConverter.HslToRgb(new ColorHelper.HSL(hue, saturation, lightness));
            Color = Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }

        public TonePixel(int r, int g, int b)
        {
            Color = Color.FromArgb(r, g, b);
            var hsl = ColorHelper.ColorConverter.RgbToHsl(new ColorHelper.RGB((byte)r, (byte)g, (byte)b));

            Length = Math.Max((long)(1.0 / ((hsl.S / 50.0) / 150.735)), 0);
            Octave = (int)((hsl.L / 100.0) * 8) + 1;
            var thing = ((hsl.H / 360.0) * 12 + (Octave * 12));
            NoteNumber = (SevenBitNumber)thing;
        }
    }
}
