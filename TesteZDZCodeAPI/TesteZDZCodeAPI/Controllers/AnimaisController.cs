using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TesteZDZCodeAPI.Models;

namespace TesteZDZCodeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimaisController : ControllerBase
    {
        private static List<Adopter> adopters = new List<Adopter>
        {
            new Adopter { Id = 1, AdopterName = "Júlio", Cpf = "12345678909", Address = "Rua Brasil, 200", Phone = "51998765004" },
            new Adopter { Id = 2, AdopterName = "Raissa", Cpf = "98765432100", Address = "Rua Milton Nascimento, 42", Phone = "51991234508" }
        };

        private static List<Animal> animals = new List<Animal>
        {
            new Animal { Id = 1, Pet = "Cachorro", Breed = "Golden Retriever", Name = "Brutus", Size = "Grande", Vaccinated = "Sim", Age = "Adulto", Sex = "Macho", Adopter = adopters[0] },
            new Animal { Id = 2, Pet = "Gato", Breed = "Siamês", Name = "Logan", Size = "Pequeno", Vaccinated = "Não", Age = "Filhote", Sex = "Macho", Adopter = adopters[1] }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get()
        {
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [HttpPost]
        public ActionResult<Animal> Post([FromBody] AnimalDTO animalDTO)
        {
            try
            {
                
                int newId = animals.Count + 1;

                Animal newAnimal = new Animal
                {
                    Id = newId,
                    Pet = animalDTO.Pet,
                    Breed = animalDTO.Breed,
                    Name = animalDTO.Name,
                    Size = animalDTO.Size,
                    Vaccinated = animalDTO.Vaccinated,
                    Age = animalDTO.Age,
                    Sex = animalDTO.Sex,
                    Adopter = new Adopter
                    {
                        AdopterName = animalDTO.Adopter.AdopterName,
                        Cpf = animalDTO.Adopter.Cpf,
                        Address = animalDTO.Adopter.Address,
                        Phone = animalDTO.Adopter.Phone
                    }
                };

                animals.Add(newAnimal);

                return CreatedAtAction(nameof(Get), new { id = newId }, newAnimal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateAnimal(int id, [FromBody] AnimalDTO updatedAnimalDto)
        {
            if (updatedAnimalDto == null)
            {
                Console.WriteLine("Dados inválidos recebidos");
                return BadRequest("Dados inválidos");
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var receivedData = JsonSerializer.Serialize(updatedAnimalDto, options);

            Console.WriteLine($"Recebido ID: {id}");
            Console.WriteLine($"Recebido Dados: {receivedData}");

            var animal = animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            animal.Pet = updatedAnimalDto.Pet;
            animal.Breed = updatedAnimalDto.Breed;
            animal.Name = updatedAnimalDto.Name;
            animal.Size = updatedAnimalDto.Size;
            animal.Vaccinated = updatedAnimalDto.Vaccinated;
            animal.Age = updatedAnimalDto.Age;
            animal.Sex = updatedAnimalDto.Sex;
            animal.Adopter = new Adopter
            {
                AdopterName = updatedAnimalDto.Adopter.AdopterName,
                Cpf = updatedAnimalDto.Adopter.Cpf,
                Address = updatedAnimalDto.Adopter.Address,
                Phone = updatedAnimalDto.Adopter.Phone
            };

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            animals.Remove(animal);

            return NoContent();
        }

    }
}
