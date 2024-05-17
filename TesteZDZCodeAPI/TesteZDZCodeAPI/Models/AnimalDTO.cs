namespace TesteZDZCodeAPI.Models
{
    public class AnimalDTO
    {
        public string Pet { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Vaccinated { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public AdopterDTO Adopter { get; set; }
    }

    public class AdopterDTO
    {
        public string AdopterName { get; set; }
        public string Cpf { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
