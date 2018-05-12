namespace GameGenerator
{
    public class LayoutRecord
    {
        public int CardsPrinted { get; private set; }
        public int PossibleCardsInLine { get; private set; }
        public int PossibleLinesOfCards { get; private set; }

        public LayoutRecord(int cardsPrinted, int possibleCardsInLine, int possibleLinesOfCards)
        {
            this.CardsPrinted = cardsPrinted;
            this.PossibleCardsInLine = possibleCardsInLine;
            this.PossibleLinesOfCards = possibleLinesOfCards;
        }
    }
}