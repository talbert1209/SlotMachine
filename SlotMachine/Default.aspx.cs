using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SlotMachine
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly Random _random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateImage(image1);
            GenerateImage(image2);
            GenerateImage(image3);
        }

        private void PullLever()
        {
            // randomly generate 3 images

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
    }
}