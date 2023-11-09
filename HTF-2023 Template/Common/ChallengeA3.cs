using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ChallengeA3
    {
        HackTheFutureClient _client;

        private Animal[] animals;
        public ChallengeA3()
        {
            _client = new HackTheFutureClient();
            this.animals = animals;
        }



        public Animal[] Animals
        {
            get { return animals; }
            set { animals = value; }
        }





        public async Task<HttpResponseMessage> StartChallengeA3()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetAsync("/api/path/a/hard/start");

            return response;
        }

        public async Task<Animal[]> GetAnimals()
        {
            await _client.Login("Team A", "SZRz9Zqpiv");
            var response = await _client.GetFromJsonAsync<Animal[]>("/api/path/a/hard/puzzle");
            Animals = response;
            return response;
        }

        public async Task<HttpResponseMessage> BuildChain()
        {
            List<Animal> chain = new List<Animal> { animals[0] };
            animals = animals.Where(animal => animal != animals[0]).ToArray();
            ExtendChain(chain, true);
            ExtendChain(chain, false);

            List<string> namesList = new List<string>();


            foreach (Animal animal in chain)
            {
                namesList.Add(animal.Name);
            }

            string[] namesArray = namesList.ToArray();

            var response = await _client.PostAsJsonAsync("/api/path/a/hard/puzzle", namesArray);
            return response;
        }



        private void ExtendChain(List<Animal> chain, bool forward)
        {
            Animal current = forward ? chain.Last() : chain.First();

            while (animals.Any())
            {
                Animal next = FindNextAnimal(current);
                if (next != null)
                {
                    if (forward)
                    {
                        chain.Add(next);
                    }
                    else
                    {
                        chain.Insert(0, next);
                    }
                    animals = animals.Where(animal => animal != next).ToArray();
                    current = next;
                }
                else
                {
                    break;
                }
            }
        }

        private Animal FindNextAnimal(Animal current)
        {
            foreach (var animal in animals)
            {
                if (ShareCharacteristic(current, animal))
                {
                    return animal;
                }
            }
            return null;
        }

        private bool ShareCharacteristic(Animal a, Animal b)
        {

            return a.Species == b.Species ||
                   a.AgeInDays == b.AgeInDays ||
                   a.WeightInGrams == b.WeightInGrams ||
                   a.HeightInCm == b.HeightInCm;
        }




    }


}

