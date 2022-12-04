using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DONT_DELITE_____.Back
{
    internal class Rotation
    {
        public Bitmap Rotat(MainWindow mainWindowCls, int angle, Bitmap currentPicture)
        {
            Bitmap img = currentPicture;
            if (angle > 180) angle -= 360;
            System.Drawing.Color bkColor = System.Drawing.Color.Transparent;
            System.Drawing.Imaging.PixelFormat pf = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            float sin = (float)Math.Abs(Math.Sin(angle * Math.PI / 180.0)); // в радианы
            float cos = (float)Math.Abs(Math.Cos(angle * Math.PI / 180.0)); // тоже
            float newImgWidth = sin * img.Height + cos * img.Width;
            float newImgHeight = sin * img.Width + cos * img.Height;
            float originX = 0f; float originY = 0f;
            if (angle > 0)
            {
                if (angle <= 90)
                    originX = sin * img.Height;
                else
                {
                    originX = newImgWidth;
                    originY = newImgHeight - sin * img.Width;
                }
            }
            else
            {
                if (angle >= -90)
                    originY = sin * img.Width;
                else
                {
                    originX = newImgWidth - sin * img.Height;
                    originY = newImgHeight;
                }
            }
            Bitmap newImg = new Bitmap((int)newImgWidth, (int)newImgHeight, pf);
            Graphics g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(originX, originY); // смещение начала координат
            g.RotateTransform((float)angle); // начало поворота
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(img, 0, 0); // Рисую изображение  0, 0
            g.Dispose();
            //addPicture(newImg);
            return newImg;
        }
    }
}
