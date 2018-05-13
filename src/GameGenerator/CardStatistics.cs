namespace GameGenerator
{
    using System.Linq;

    public class CardStatistics
    {
        public CardStatistics(CardInfo[] infos, Card[] cards)
        {
            this.AllCardDefinitionsCount = infos.Length;
            this.CardsTotal = cards.Length;

            this.LandDefinitionsCount = infos.Count(i => i.CardType == CardType.Land);
            this.AnimalDefinitionsCount = infos.Count(i => i.CardType == CardType.Animal);
            this.PlantDefinitionsCount = infos.Count(i => i.CardType == CardType.Plant);

            this.LandCardsCount = cards.Count(card => card.CardType == CardType.Land);
            this.AnimalCardsCount = cards.Count(card => card.CardType == CardType.Animal);
            this.PlantCardsCount = cards.Count(card => card.CardType == CardType.Plant);

            this.LandCardsMultiplierMin = infos.Where(i => i.CardType == CardType.Land).Select(i => i.CardCount).Min();
            this.LandCardsMultiplierMax = infos.Where(i => i.CardType == CardType.Land).Select(i => i.CardCount).Max();

            this.AnimalCardsMultiplierMin = infos.Where(i => i.CardType == CardType.Animal).Select(i => i.CardCount).Min();
            this.AnimalCardsMultiplierMax = infos.Where(i => i.CardType == CardType.Animal).Select(i => i.CardCount).Max();

            this.PlantCardsMultiplierMin = infos.Where(i => i.CardType == CardType.Plant).Select(i => i.CardCount).Min();
            this.PlantCardsMultiplierMax = infos.Where(i => i.CardType == CardType.Plant).Select(i => i.CardCount).Max();
        }

        public int AllCardDefinitionsCount { get; }
        public int CardsTotal { get; }
        public int LandDefinitionsCount { get; }
        public int AnimalDefinitionsCount { get; }
        public int PlantDefinitionsCount { get; }
        public int LandCardsCount { get; }
        public int AnimalCardsCount { get; }
        public int PlantCardsCount { get; }
        public int LandCardsMultiplierMin { get; }
        public int LandCardsMultiplierMax { get; }
        public int AnimalCardsMultiplierMin { get; }
        public int AnimalCardsMultiplierMax { get; }
        public int PlantCardsMultiplierMin { get; }
        public int PlantCardsMultiplierMax { get; }
    }
}
