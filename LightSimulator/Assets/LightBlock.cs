namespace AdditionalClass{
    public class LightBlock
    {
        protected string pin_value;
        protected string pin_intensity;
        protected bool processed;

        public LightBlock(string pin_value, string pin_intensity)
        {
            this.pin_value = pin_value;
            this.pin_intensity = pin_intensity;
            this.processed = false;
        }

        public string getPinValue()
        {
            return this.pin_value;
        }

        public string getPinIntensity()
        {
            return this.pin_intensity;
        }

        public bool isProcessed()
        {
            return this.processed;
        }

        public void setProcessed(bool processed)
        {
            this.processed = processed;
        }
    }
}