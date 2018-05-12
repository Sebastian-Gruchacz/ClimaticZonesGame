namespace GameGenerator
{
    public class Card
    {
        public CardType CardType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public string ZoneSetName { get; set; }

        public int CardCount { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.Title) &&
                   !string.IsNullOrWhiteSpace(this.Description) &&
                   !string.IsNullOrWhiteSpace(this.ImageName) &&
                   !string.IsNullOrWhiteSpace(this.ZoneSetName) && 
                   this.CardCount >= 0;
        }
    }
}