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
    public static class MidiFileToBitmap
    {
        public static Bitmap ToBitmap(this MidiFile midiFile)
        {
            List<Melanchall.DryWetMidi.Interaction.Note> noteList = new();

            foreach (var trackChunk in midiFile.GetTrackChunks())
            {
                using (var notesManager = trackChunk.ManageNotes())
                {
                    foreach (var note in notesManager.Notes)
                    {
                        noteList.Add(note);
                        
                    }
                }
            }

            var tempoMap = midiFile.GetTempoMap();
            Console.WriteLine(noteList.Average(x => x.Length));
            Console.WriteLine(noteList.Average(x => x.Time));
            Console.WriteLine(noteList.Max(x => x.Time));
            MetricTimeSpan timespan = TimeConverter.ConvertTo<MetricTimeSpan>(noteList.Max(x => x.Time), tempoMap);
            Console.WriteLine(timespan.TotalMicroseconds);
            long things = TimeConverter.ConvertFrom(timespan, tempoMap);
            Console.WriteLine(things);
            MidiTimeSpan duration = midiFile.GetDuration<MidiTimeSpan>();
            Console.WriteLine($"Duration: {duration.TimeSpan}");

            noteList = noteList.OrderBy(x => x.Time).ToList();
            List<MidiPixelSet> notePixels = noteList.Select(n => new MidiPixelSet(n, noteList.Average(x => x.Length), duration.TimeSpan)).ToList();
            
            for (int i = 0; i< 10; i++)
            {
                Console.WriteLine(notePixels[i].Tone.NoteNumber);
            }

            var dimensions = (int)Math.Ceiling(Math.Sqrt(notePixels.Count*2));
            if (dimensions % 2 != 0)
            {
                dimensions += 1;
            }

            var bitmap = new Bitmap(dimensions, dimensions);

            int column = 0;
            int row = 0;
            int count = 0;
            notePixels.ForEach(x =>
            {
                count++;
                if (column >= (dimensions - 1))
                {
                    column = 0;
                    row++;
                }

                bitmap.SetPixel(row, column, x.Tone.Color);
                column++;
                bitmap.SetPixel(row, column, x.Timing.Color);
                column++;
                
                
            });

            return bitmap;
        }
        public static MidiFile ToMidiFile(this Bitmap bitmap, TimeSpan songLength)
        {
            bool newPixelSet = true;
            var metricTimeSpan = new MetricTimeSpan(songLength);

            var midiFile = new MidiFile();
            TempoMap tempoMap = midiFile.GetTempoMap();

            long maxLength = TimeConverter.ConvertFrom(metricTimeSpan, tempoMap);

            List<MidiPixelSet> pixelSet = new List<MidiPixelSet>();

            MidiPixelSet midiPixelSet = new();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);

                    if (pixel.IsEmpty)
                    {
                        break;
                    }

                    
                    if (newPixelSet)
                    {
                        midiPixelSet = new();
                        midiPixelSet.Tone = new(pixel.R, pixel.G, pixel.B);
                    }
                    else
                    {
                        midiPixelSet.Timing = new(pixel.R, pixel.G, pixel.B, maxLength);
                        pixelSet.Add(midiPixelSet);
                    }

                    newPixelSet = !newPixelSet;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(pixelSet[i].Tone.NoteNumber);
            }

            

            var trackChunk = new TrackChunk();
            int count = 0;
            using (var notesManager = trackChunk.ManageNotes())
            {
                NotesCollection notes = notesManager.Notes;
                pixelSet.ForEach(x =>
                {
                    count++;
                    notes.Add(new Note(x.Tone.NoteNumber, x.Tone.Length, x.Timing.Time));
                });
            }

            midiFile.Chunks.Add(trackChunk);
            return midiFile;

        }
    }
}
