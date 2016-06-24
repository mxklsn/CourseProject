namespace OpenGlTemplateApp
{

    internal class AreaSettings
    {
        public int Interval { get; private set; }
        public double Coefficient { get; private set; }

        public AreaSettings(int interval, double coefficient)
        {
            Interval = interval;
            Coefficient = coefficient;
        }
    }
}
