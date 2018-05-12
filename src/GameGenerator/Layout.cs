namespace GameGenerator
{
    using System.Diagnostics;

    public class Layout
    {
        private readonly LayoutParameters _settings;

        public Layout(LayoutParameters settings)
        {
            this._settings = settings;
        }

        public LayoutRecord Calculations()
        {
            var pageAvailableWidth = this._settings.PageWidth - (this._settings.PageMarginLeft +
                                                                 this._settings.PageMarginRight);
            var pageAvailableHeight = this._settings.PageHeight - (this._settings.PageMarginTop +
                                                                 this._settings.PageMarginBottom);

            var cardsPrintedPortrait = this.GetCardsOnPortrait(pageAvailableWidth, pageAvailableHeight);
            var cardsPrintedLandscape = this.GetCardsOnLandscape(pageAvailableHeight, pageAvailableWidth);

            // prefer portrait printing if possible
            this._settings.IsPortraitLayout = cardsPrintedPortrait.CardsPrinted >= cardsPrintedLandscape.CardsPrinted;
            this._settings.Layout = (this._settings.IsPortraitLayout.Value) ? cardsPrintedPortrait : cardsPrintedLandscape;

            return this._settings.Layout;
        }

        private LayoutRecord GetCardsOnLandscape(int pageAvailableHeight, int pageAvailableWidth)
        {
            var possibleCardsInLine = pageAvailableHeight / this._settings.CardWidth;
            var possibleLinesOfCards = pageAvailableWidth / this._settings.CardHeight;

            var widthWaste = pageAvailableWidth - (possibleLinesOfCards * this._settings.CardHeight);
            var heightWaste = pageAvailableHeight - (possibleCardsInLine * this._settings.CardWidth);

            var cardsPrinted = possibleCardsInLine * possibleLinesOfCards;

            Debug.WriteLine($"Landscape waste area: {widthWaste*heightWaste:D2} mm^2");
            Debug.WriteLine($"Landscape {this._settings.CardWidth}x{this._settings.CardHeight} cards printed: {cardsPrinted}");

            return new LayoutRecord(cardsPrinted, possibleCardsInLine, possibleLinesOfCards);
        }

        private LayoutRecord GetCardsOnPortrait(int pageAvailableWidth, int pageAvailableHeight)
        {
            var possibleCardsInLine = pageAvailableWidth / this._settings.CardWidth;
            var possibleLinesOfCards = pageAvailableHeight / this._settings.CardHeight;

            var widthWaste = pageAvailableWidth - (possibleCardsInLine * this._settings.CardWidth);
            var heightWaste = pageAvailableHeight - (possibleLinesOfCards * this._settings.CardHeight);

            var cardsPrinted = possibleCardsInLine * possibleLinesOfCards;

            Debug.WriteLine($"Portrait waste area: {widthWaste * heightWaste:D2} mm^2");
            Debug.WriteLine($"Portrait {this._settings.CardWidth}x{this._settings.CardHeight} cards printed: {cardsPrinted}");

            return new LayoutRecord(cardsPrinted, possibleCardsInLine, possibleLinesOfCards);
        }
    }
}
