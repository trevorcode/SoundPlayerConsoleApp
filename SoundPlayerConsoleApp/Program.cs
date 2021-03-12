using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SoundPlayerConsoleApp
{
    partial class Program
    {
        public static void Main()
        {


            //// ------- READ FROM MIDI FILE --------- 
            //string midiPath = Path.Combine("midi/", "queen.mid");
            //var midiFile = MidiFile.Read(midiPath);
            //Console.WriteLine(midiFile.GetDuration<MetricTimeSpan>().Minutes + " " + midiFile.GetDuration<MetricTimeSpan>().Seconds);

            //string bmpPath = Path.Combine("images/", "queen.bmp");
            //var bitmap = midiFile.ToBitmap();
            //bitmap.Save(bmpPath);


            // ------- READ FROM BMP FILE ----------
            string bmpPathRead = Path.Combine("images/", "donut.png");
            var newBitmap = new Bitmap(bmpPathRead);
            var length = new TimeSpan(0, 1, 0);
            var newMidi = newBitmap.ToMidiFile(length);

            TestColorConverter testColorConverter = new();
            //testColorConverter.TestMidiComparison(midiFile, newMidi);

            Console.WriteLine($"{newMidi.GetDuration<MetricTimeSpan>().Minutes} minute {newMidi.GetDuration<MetricTimeSpan>().Seconds} seconds");
            using (var outputDevice = OutputDevice.GetAll().FirstOrDefault())
            using (var playback = newMidi.GetPlayback(outputDevice))
            {
                playback.Speed = 1.0;
                playback.Play();
            }

            Console.WriteLine("Song Finished");
            Console.ReadLine();
        }       

    }

}
