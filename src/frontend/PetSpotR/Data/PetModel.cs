using Dapr.Client;

namespace PetSpotR.Models 
{
    public class PetModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string OwnerEmail { get; set; }
        public string ID { get; set; }
        public string State { get; set; }
        public List<string> Images { get; set; }

        // Constructor
        public PetModel()
        {
            Name = "";
            Type = "";
            Breed = "";
            OwnerEmail = "";
            ID = Guid.NewGuid().ToString();
            State = "new";
            Images = new();
        }

        public async Task SavePetStateAsync(DaprClient daprClient)
        {
            // Save state to "pets" Dapr state store, using the supplied Dapr client
            try
            {
                await daprClient.SaveStateAsync("pets", ID, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            await daprClient.SaveStateAsync("pets", ID, this);
        }

        public async Task PublishLostPetAsync(DaprClient daprClient)
        {
            // Publish a message to the "lostPet" Dapr pub/sub topic on the "pubsub" broker
            try
            {
                await daprClient.PublishEventAsync("pubsub", "lostPet", this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
