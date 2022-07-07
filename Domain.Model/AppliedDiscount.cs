namespace Domain.Model
{
    public class AppliedDiscount
    {
        public Product Product { get; set; }

        public string Description { get; set; }

        public double Savings { get; set; }
    }
}