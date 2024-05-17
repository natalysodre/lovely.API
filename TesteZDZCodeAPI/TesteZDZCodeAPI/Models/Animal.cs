namespace TesteZDZCodeAPI.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Pet { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Vaccinated { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public Adopter Adopter { get; set; }
    }

}
