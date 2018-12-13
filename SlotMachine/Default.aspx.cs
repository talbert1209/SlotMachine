using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;
using Image = System.Web.UI.WebControls.Image;

namespace SlotMachine
{
    public partial class Default : Page
    {
        private readonly Random _random = new Random();
        private readonly decimal playersMoney = 100m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateAll3Images();
                moneyLabel.Text = $"Player's money: {playersMoney:C}";
                ViewState.Add("PlayersMoney", playersMoney);
            }
        }

        protected void pullLeverButton_Click(object sender, EventArgs e)
        {
            if (!VerifyBet()) return;
            PullLever();
        }

        // Helper Methods
        private bool VerifyBet()
        {
            resultLabel.ForeColor = Color.Red;
            if (!VerifyBetIsMoney())
            {
                resultLabel.Text = $"{betTextBox.Text} is not a valid bet. Please enter a valid bet!";
                return false;
            }

            if (decimal.Parse(ViewState["PlayersMoney"].ToString()) == 0)
            {
                resultLabel.Text = "Get out of here, you don't have any money!";
                return false;
            }

            if (decimal.Parse(betTextBox.Text) > decimal.Parse(ViewState["PlayersMoney"].ToString()))
            {
                resultLabel.Text =
                    $"You don't have {decimal.Parse(betTextBox.Text):C} to bet. Please enter an amount less than {ViewState["PlayersMoney"]:C}";
                return false;
            }

            return true;
        }

        private void PullLever()
        {
            resultLabel.ForeColor = Color.Black;
            GenerateAll3Images();
            DisplayPlayResultLabel();
            CalculatePlayersCash();
            DisplayPlayersCash();
        }

        private void DisplayPlayersCash()
        {
            moneyLabel.Text = $"Player's Money: {ViewState["PlayersMoney"]:C}";
        }

        private void CalculatePlayersCash()
        {
            var playResult = GetPlayResults(GetImageResults(image1), GetImageResults(image2), GetImageResults(image3));
            var currentCash = decimal.Parse(ViewState["PlayersMoney"].ToString());

            if (DidPlayerWin())
            {
                currentCash += CalculateWinnings(playResult);
                ViewState.Add("PlayersMoney", currentCash);
            }
            else
            {
                currentCash -= decimal.Parse(betTextBox.Text);
                ViewState.Add("PlayersMoney", currentCash);
            }
        }

        private bool VerifyBetIsMoney()
        {
            return decimal.TryParse(betTextBox.Text, out _);
        }


        private void GenerateAll3Images()
        {
            GenerateImage(image1);
            GenerateImage(image2);
            GenerateImage(image3);
        }

        private void GenerateImage(Image targetImage)
        {
            var imageNumber = _random.Next(1, 13);
            targetImage.ImageUrl = GetImage(imageNumber);
        }

        private string GetImage(int imageNumber)
        {
            if (imageNumber == 1) return "~/Images/Bar.png";
            if (imageNumber == 2) return "~/Images/Bell.png";
            if (imageNumber == 3) return "~/Images/Cherry.png";
            if (imageNumber == 4) return "~/Images/Clover.png";
            if (imageNumber == 5) return "~/Images/Diamond.png";
            if (imageNumber == 6) return "~/Images/HorseShoe.png";
            if (imageNumber == 7) return "~/Images/Lemon.png";
            if (imageNumber == 8) return "~/Images/Orange.png";
            if (imageNumber == 9) return "~/Images/Plum.png";
            if (imageNumber == 10) return "~/Images/Seven.png";
            if (imageNumber == 11) return "~/Images/Strawberry.png";
            if (imageNumber == 12) return "~/Images/Watermelon.png";
            return "~/Images/Bar.png";
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToConditionalTernaryExpression")]
        private void DisplayPlayResultLabel()
        {
            var bet = decimal.Parse(betTextBox.Text);
            var playResult = GetPlayResults(GetImageResults(image1), GetImageResults(image2), GetImageResults(image3));

            if (!DidPlayerWin())
                resultLabel.Text = $"Sorry, you lost {CalculateLoss():C}. Better luck next time.";
            else
                resultLabel.Text = $"You bet {bet:C} and won {CalculateWinnings(playResult):C}!";
        }

        private decimal CalculateWinnings(string playResult)
        {
            var bet = decimal.Parse(betTextBox.Text);
            return bet * CalculateMultiplier(playResult);
        }

        private bool DidPlayerWin()
        {
            var playResult =
                GetPlayResults(GetImageResults(image1), GetImageResults(image2), GetImageResults(image3));
            if (playResult == "bust") return false;

            return true;
        }

        private decimal CalculateMultiplier(string playResult)
        {
            if (playResult == "Jackpot") return 100m;
            if (playResult == "2X") return 2m;
            if (playResult == "3X") return 3m;
            if (playResult == "4X") return 4m;
            return 1m;
        }

        private decimal CalculateLoss()
        {
            return decimal.Parse(betTextBox.Text);
        }

        private string GetImageResults(Image imageToCheck)
        {
            if (imageToCheck.ImageUrl == "~/Images/Bar.png")
                return "bar";
            if (imageToCheck.ImageUrl == "~/Images/Cherry.png")
                return "cherry";
            if (imageToCheck.ImageUrl == "~/Images/Seven.png")
                return "seven";
            return "other";
        }

        private string GetPlayResults(string image1Result, string image2Result, string image3Result)
        {
            var cherryCount = CherryCount(image1Result, image2Result, image3Result);
            var sevenCount = SevenCount(image1Result, image2Result, image3Result);
            var barCount = BarCount(image1Result, image2Result, image3Result);

            return AnalyzeCounts(cherryCount, sevenCount, barCount);
        }

        private string AnalyzeCounts(int cherryCount, int sevenCount, int barCount)
        {
            if (sevenCount == 3) return "Jackpot";
            if (barCount > 0) return "bust";
            return AnalyzeCherryCounts(cherryCount);
        }

        private string AnalyzeCherryCounts(int cherryCount)
        {
            if (cherryCount == 1) return "2X";
            if (cherryCount == 2) return "3X";
            if (cherryCount == 3) return "4X";

            return "bust";
        }

        private int CherryCount(string image1Result, string image2Result, string image3Result)
        {
            var cherryCount = 0;
            if (image1Result == "cherry") cherryCount++;
            if (image2Result == "cherry") cherryCount++;
            if (image3Result == "cherry") cherryCount++;

            return cherryCount;
        }

        private int BarCount(string image1Result, string image2Result, string image3Result)
        {
            var barCount = 0;
            if (image1Result == "bar") barCount++;
            if (image2Result == "bar") barCount++;
            if (image3Result == "bar") barCount++;

            return barCount;
        }

        private int SevenCount(string image1Result, string image2Result, string image3Result)
        {
            var sevenCount = 0;
            if (image1Result == "seven") sevenCount++;
            if (image2Result == "seven") sevenCount++;
            if (image3Result == "seven") sevenCount++;

            return sevenCount;
        }
    }
}