using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day04 : AdventProblem<GuardEvent[], int>
    {
        protected override string InputFilePath => "Inputs/Day04.txt";

        protected override GuardEvent[] ParseInputFile()
        {
            var lines = File.ReadAllLines(InputFilePath);
            var parsed = ParseLines(lines);
            return parsed;
        }

        public GuardEvent[] ParseLines(string[] lines)
        {
            // sorts chronologically because AoC is nice enough to go big -> small in their input choice
            Array.Sort(lines);

            var events = lines.Select(GuardEvent.Parse).ToArray();
            return events;
        }

        public override int Part1(GuardEvent[] guardEvents)
        {
            // approach:
            // for each guard, make a map from minute => number of times they were asleep
            // use that to find out who sleeps the most
            // use that to find out what minute they are most likely to be asleep

            var metadata = GetMetadata(guardEvents);

            var guardSleepTimes = metadata.ToDictionary(p => p.Key,
                                                        p => p.Value.Sum(pair => pair.Value));

            var sleepiestGuardId = guardSleepTimes.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            var sleepiestMinute = metadata[sleepiestGuardId].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return sleepiestGuardId * sleepiestMinute;
        }

        public Dictionary<int, Dictionary<int, int>> GetMetadata(GuardEvent[] guardEvents)
        {
            var metadata = new Dictionary<int, Dictionary<int, int>>();

            for (var i = 0; i < guardEvents.Length; i++)
            {
                var guardEvent = guardEvents[i];

                if (guardEvent.GuardAction == GuardAction.FallAsleep)
                {
                    if (!metadata.ContainsKey(guardEvent.GuardId))
                        metadata[guardEvent.GuardId] = new Dictionary<int, int>();

                    var wakeUpEvent = guardEvents[i + 1];
                    if (wakeUpEvent.GuardAction != GuardAction.WakeUp)
                        throw new Exception("We have made an erroneous assumption that falling asleep is always immediately followed by waking up");
                    for (var minute = guardEvent.DateTime.Minute; minute < wakeUpEvent.DateTime.Minute; minute++)
                    {
                        if (!metadata[guardEvent.GuardId].ContainsKey(minute))
                            metadata[guardEvent.GuardId][minute] = 0;

                        metadata[guardEvent.GuardId][minute]++;
                    }
                }

            }

            return metadata;
        }

        public override int Part2(GuardEvent[] guardEvents)
        {
            var metadata = GetMetadata(guardEvents);

            int? maxId = null;
            int? maxMinute = null;
            var maxFrequency = 0;

            foreach (var metadatum in metadata)
            {
                if (!maxId.HasValue)
                    maxId = metadatum.Key;

                foreach (var times in metadatum.Value)
                {
                    if (!maxMinute.HasValue)
                        maxMinute = times.Key;

                    if (times.Value > maxFrequency)
                    {
                        maxFrequency = times.Value;
                        maxId = metadatum.Key;
                        maxMinute = times.Key;
                    }
                }
            }

            if (!maxId.HasValue || !maxMinute.HasValue)
                throw new Exception("No maximum found... are you operating on an empty list?");

            return maxId.GetValueOrDefault() * maxMinute.GetValueOrDefault();
        }
    }



    public struct GuardEvent
    {
        public readonly DateTime DateTime;
        public readonly int GuardId;
        public readonly GuardAction GuardAction;

        public GuardEvent(DateTime dateTime, int guardId, GuardAction guardAction)
        {
            DateTime = dateTime;
            GuardId = guardId;
            GuardAction = guardAction;
        }

        public static int? ActiveGuardId { get; set; }
        public static GuardEvent Parse(string str)
        {
            var digitStrs = Regex.Split(str, @"\D+").Where(split => split != string.Empty);
            var digits = digitStrs.Select(int.Parse).ToArray();
            if (digits.Length != 5 && digits.Length != 6)
                throw new ArgumentException($"Error parsing this line: {str}");

            var dateTime = new DateTime(digits[0], digits[1], digits[2], digits[3], digits[4], 00);

            Days.GuardAction action;
            if (digits.Length == 6)
            {
                ActiveGuardId = digits[5];
                action = GuardAction.BeginShift;
            }
            else
            {
                if (str.Contains("falls asleep"))
                    action = GuardAction.FallAsleep;
                else if (str.Contains("wakes up"))
                    action = GuardAction.WakeUp;
                else
                    throw new ArgumentException($"Error parsing this line: {str}");
            }

            if (action != GuardAction.BeginShift && !ActiveGuardId.HasValue)
                throw new ArgumentException("No active guard ID");

            var guardEvent = new GuardEvent(dateTime, ActiveGuardId.GetValueOrDefault(), action);

            return guardEvent;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GuardEvent))
            {
                return false;
            }

            var @event = (GuardEvent)obj;
            return DateTime == @event.DateTime &&
                   GuardId == @event.GuardId &&
                   GuardAction == @event.GuardAction;
        }

        public override int GetHashCode()
        {
            var hashCode = 958497183;
            hashCode = hashCode * -1521134295 + DateTime.GetHashCode();
            hashCode = hashCode * -1521134295 + GuardId.GetHashCode();
            hashCode = hashCode * -1521134295 + GuardAction.GetHashCode();
            return hashCode;
        }
    }

    public enum GuardAction
    {
        BeginShift = 0,
        FallAsleep = 1,
        WakeUp = 2,
    }
}
