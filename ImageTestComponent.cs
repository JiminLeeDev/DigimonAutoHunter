using System;
using System.Drawing;
using System.Windows.Forms;

namespace DigimonAutoHunter
{
    class ImageTestComponent : Control
    {
        readonly Random Random = new Random();

        readonly Bitmap[] Images;

        Bitmap CurrentImage;

        public ImageTestComponent(Bitmap[] images, int x, int y, int width, int height)
        {
            Images = images;

            Location = new Point(x, y);
            Width = width;
            Height = height;

            Click += (o,e)=> DrawRandomImage();
            InvokePaint(this, new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        void DrawRandomImage()
        {
            ChangeCurrentImage();

            var imgWidth = CurrentImage.Width;
            var imgHeight = CurrentImage.Height;
            var imgX = Width >= imgWidth ? Random.Next(Width - imgWidth) : 0;
            var imgY = Width >= imgHeight ? Random.Next(Height - imgWidth) : 0;

            using (var gfx = CreateGraphics())
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(CurrentImage, imgX, imgY);
            }
        }

        void ChangeCurrentImage() =>
            CurrentImage = Images[Random.Next(Images.Length)];

        public Bitmap GetImage() =>
            CurrentImage;
    }
}
