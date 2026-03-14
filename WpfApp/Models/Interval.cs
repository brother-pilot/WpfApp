namespace WpfApp.Models
{
    public class Interval
    {
        public int DepthFrom { get; set; }
        public int DepthTo { get; set; }
        public Rock? Rock { get; set; }
        public double Porosity { get; set; }
    }
}