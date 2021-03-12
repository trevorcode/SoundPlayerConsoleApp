using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPlayerConsoleApp
{
    public class TestColorConverter
    {
        public void TestMidiComparison(MidiFile midifile1, MidiFile midiFile2)
        {
            List<Melanchall.DryWetMidi.Interaction.Note> noteList1 = new();
            List<Melanchall.DryWetMidi.Interaction.Note> noteList2 = new();

            foreach (var trackChunk in midifile1.GetTrackChunks())
            {
                using (var notesManager = trackChunk.ManageNotes())
                {
                    foreach (var note in notesManager.Notes)
                    {
                        noteList1.Add(note);
                    }
                }
            }

            foreach (var trackChunk in midiFile2.GetTrackChunks())
            {
                using (var notesManager = trackChunk.ManageNotes())
                {
                    foreach (var note in notesManager.Notes)
                    {
                        noteList2.Add(note);
                    }
                }
            }

            noteList1 = noteList1.OrderBy(x => x.Time).ToList();
            noteList2 = noteList2.OrderBy(x => x.Time).ToList();

            if (noteList1.Count != noteList2.Count)
            {
                Console.WriteLine("There is a different number of notes between these tracks");
            }

            for (int i = 0; i < noteList1.Count; i++)
            {
                if (noteList1[i].NoteNumber != noteList2[i].NoteNumber)
                {
                    Console.WriteLine($"Different note value for {i}: {noteList1[i].NoteNumber} - {noteList2[i].NoteNumber}");
                }
            }

        }
        public void TestConversion()
        {
            while (true)

            {
                Random random = new Random();

                int hue = random.Next(0, 360);
                byte saturation = (byte)random.Next(0, 100);
                byte lightness = (byte)random.Next(0, 100);

                var rgb = ColorHelper.ColorConverter.HslToRgb(new ColorHelper.HSL(hue, saturation, lightness));
                var color = Color.FromArgb(rgb.R, rgb.G, rgb.B);

                var HSLValues = ColorHelper.ColorConverter.RgbToHsl(new ColorHelper.RGB(rgb.R, rgb.G, rgb.B));
                if (HSLValues.H != hue && HSLValues.S != saturation && HSLValues.L != lightness)
                {
                    Console.WriteLine("Test Failed");
                }
                Console.WriteLine("Test passed");
            }
            


        }
    }
}
