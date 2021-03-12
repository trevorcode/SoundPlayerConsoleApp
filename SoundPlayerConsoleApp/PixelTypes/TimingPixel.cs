using Melanchall.DryWetMidi.Interaction;
using System.Drawing;

namespace SoundPlayerConsoleApp
{
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
}
