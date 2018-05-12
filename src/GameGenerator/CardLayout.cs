namespace GameGenerator
{
    using System;

    public class CardLayout
    {
        private readonly LayoutParameters _parameters;
        private string _landLayout;
        private string _animalLayout;
        private string _plantLayout;


        public CardLayout(LayoutParameters parameters)
        {
            this._parameters = parameters;
            LoadLayouts();
        }

        private void LoadLayouts()
        {
            this._landLayout = Helpers.LoadFile(@"Data\Layouts\Land.html");
            this._animalLayout = Helpers.LoadFile(@"Data\Layouts\Animal.html");
            this._plantLayout = Helpers.LoadFile(@"Data\Layouts\Plant.html");
        }

        public string RenderCardHtmlContent(Card card)
        {
            
        }
    }

    internal static class Helpers
    {
        public static string LoadFile(string path)
        {
            throw new NotImplementedException();
        }
    }

    internal class Card
    {
        public CardType CardType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public int CardCount { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.Title) &&
                   !string.IsNullOrWhiteSpace(this.Description) &&
                   !string.IsNullOrWhiteSpace(this.ImageName) && this.CardCount >= 0;
        }
    }

    internal enum CardType
    {
        Land,
        Animal,
        Plant
    }
}
