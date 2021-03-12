using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Drawing;

namespace SoundPlayerConsoleApp
{
    public class TonePixel : NotePixel
    {
        public SevenBitNumber NoteNumber { get; set; }
        public int Octave { get; set; }
        public long Length { get; set; }
        //public TonePixel(Note note, double AverageNoteLength)
        //{
        //    NoteNumber = note.NoteNumber;
        //    Octave = note.Octave;
        //    Length = note.Length;

        //    int hue = note.NoteNumber * 2;
        //    //int hue = (int)(((note.NoteNumber % 12.0) / 12.0) * 360);
        //    byte saturation = (byte)Math.Min(((1.0 / note.Length) * AverageNoteLength) * 50, 100);
        //    byte lightness = (byte)(note.Octave*9);

        //    var rgb = ColorHelper.ColorConverter.HslToRgb(new ColorHelper.HSL(hue, saturation, lightness));
        //    Color = Color.FromArgb(rgb.R, rgb.G, rgb.B);
        //}

        public TonePixel(Note note, double AverageNoteLength)
        {
            NoteNumber = note.NoteNumber;
            Octave = note.Octave;
            Length = note.Length;

            int red = note.NoteNumber * 2;
            //int hue = (int)(((note.NoteNumber % 12.0) / 12.0) * 360);
            int green = (int)Math.Min(((1.0 / note.Length) * AverageNoteLength) * 127, 255);
            int blue = note.Octave * 24;

            Color = Color.FromArgb(red, green, blue);
        }

        public TonePixel(int r, int g, int b, double AverageNoteLength = 150)
        {
            Length = Math.Max((long)(1.0 / ((g / 127.0) / AverageNoteLength)), 0);
            Octave = (int)(b / 24);
            NoteNumber = (SevenBitNumber)(r / 2);
        }

        //public TonePixel(float hue, float saturation, float brightness)
        //{
        //    //Color = Color.FromArgb(r, g, b);
        //    //var hsl = ColorHelper.ColorConverter.RgbToHsl(new ColorHelper.RGB((byte)r, (byte)g, (byte)b));

        //    Length = Math.Max((long)(1.0 / ((saturation / 50.0) / 150.735)), 0);
        //    Octave = (int)(brightness / 9);
        //    //var thing = ((hsl.H / 360.0) * 12 + (Octave * 12));

        //    NoteNumber = (SevenBitNumber)(Math.Round(hue) / 2);
        //}
    }
}
