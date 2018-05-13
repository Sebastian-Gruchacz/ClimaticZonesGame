namespace GameGenerator
{
    public class Card
    {
        public CardType CardType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public ZoneName ZoneSetName { get; set; }

        public int IconLocation { get; set; }


        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.Title) &&
                   !string.IsNullOrWhiteSpace(this.Description) &&
                   !string.IsNullOrWhiteSpace(this.ImageName);
        }
    }
}