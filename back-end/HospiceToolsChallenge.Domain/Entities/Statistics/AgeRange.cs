namespace HospiceToolsChallenge.Domain.Entities.Statistics
{
    public class AgeRange
    {
        public int From { get; set; } = 0;

        public int? To { get; set; }

        public AgeRange() {}

        public AgeRange(int from, int? to) {
            From = from;
            To = to;
        }
    }
}
