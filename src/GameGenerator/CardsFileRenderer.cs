namespace GameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CardsFileRenderer
    {
        private readonly LayoutParameters _parameters;
        private readonly CardRenderer _cardRenderer;
        private string _cardsFileTemplate;

        public CardsFileRenderer(LayoutParameters parameters)
        {
            this._parameters = parameters;
            this._cardRenderer = new CardRenderer(parameters);

            this.LoadTemplates();
        }

        private void LoadTemplates()
        {
            this._cardsFileTemplate = Helpers.LoadFile(@"Data\Layouts\AllCards.html");
        }

        public void RenderFile(IEnumerable<Card> cards, StreamWriter output)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards));
            }
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            string template = this._cardsFileTemplate;
            string content = this.BuildContent(cards);

            string result = template.Replace("{#CARDS_TABLE_CONTENT#}", content);

            output.Write(result);
        }

        private string BuildContent(IEnumerable<Card> cards)
        {
            StringBuilder sb = new StringBuilder();
            int startIndex = 0;

            var enumerable = cards as Card[] ?? cards.ToArray();
            while (startIndex < enumerable.Count())
            {
                var cardsOnPage = enumerable.Skip(startIndex).Take(this._parameters.Layout.CardsPrinted).ToArray();
                sb.Append(this.RenderOnePage(cardsOnPage));

                startIndex += cardsOnPage.Count();
            }

            return sb.ToString();
        }

        private string RenderOnePage(Card[] cardsOnPage)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<table class=""CardsTable"" cellspacing=""0"" cellpadding=""0""><tbody>");

            int cardIndex = 0;
            for (int y = 0; y < this._parameters.Layout.PossibleLinesOfCards; y++)
            {
                sb.AppendLine("<tr>");
                for (int x = 0; x < this._parameters.Layout.PossibleCardsInLine; x++)
                {
                    sb.AppendLine("<td>");

                    sb.AppendLine(cardIndex < cardsOnPage.Length
                        ? this._cardRenderer.RenderCardHtmlContent(cardsOnPage[cardIndex])
                        : "&nbsp;");

                    cardIndex++;
                    sb.AppendLine("</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine(@"</tbody></table><br><br>");

            return sb.ToString();
        }
    }
}
