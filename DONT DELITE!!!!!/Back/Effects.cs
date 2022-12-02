using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;

namespace DONT_DELITE_____.Back
{
    internal class Effects
    {
        MainWindow mw = new MainWindow();
        private Bitmap result;
        private string path;
        private System.Windows.Controls.Image image;
        private int currentBitmap = 0;
        private Bitmap currentPicture;
        private CustomBitmap myBitmap;
        private List<Bitmap> bitmapList = new List<Bitmap>();

        #region Matrix
        ColorMatrix invert = new ColorMatrix(new float[][]
        {
            new float[] { -1,   0,   0,  0,  1},
            new float[] {  0,  -1,   0,  0,  1},
            new float[] {  0,   0,  -1,  0,  1},
            new float[] {  0,   0,   0,  1,  0},
            new float[] {  1,   1,   1,  0,  1}
        });
        ColorMatrix gray = new ColorMatrix(new float[][]
        {
            new float[]{0.299f, 0.299f, 0.299f, 0, 0},
            new float[]{0.587f, 0.587f, 0.587f, 0, 0},
            new float[]{0.114f, 0.114f, 0.114f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 0}
        });
        ColorMatrix fog = new ColorMatrix(new float[][]
        {
            new float[]{1+0.3f, 0, 0, 0, 0},
            new float[]{0, 1+0.7f, 0, 0, 0},
            new float[]{0, 0, 1+1.3f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        ColorMatrix flash = new ColorMatrix(new float[][]
        {
            new float[]{1+0.9f, 0, 0, 0, 0},
            new float[]{0, 1+1.5f, 0, 0, 0},
            new float[]{0, 0, 1+1.3f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        ColorMatrix frozen = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
            new float[]{1+0.3f, 0, 0, 0, 0},
            new float[]{0, 1+0f, 0, 0, 0},
            new float[]{0, 0, 1+5f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        ColorMatrix arctic = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
                    new float[]{1,0,0,0,0},
                    new float[]{0,1,0,0,0},
                    new float[]{0,0,1,0,0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 1, 0, 1}
        });
        ColorMatrix sepia = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
            new float[]{.393f, .349f, .272f, 0, 0},
            new float[]{.769f, .686f, .534f, 0, 0},
            new float[]{.189f, .168f, .131f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        ColorMatrix kakao = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
            new float[]{.393f, .349f, .272f+1.3f, 0, 0},
            new float[]{.769f, .686f+0.5f, .534f, 0, 0},
            new float[]{.189f+2.3f, .168f, .131f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
                });
        ColorMatrix cuji = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
            new float[]{.393f, .349f+0.5f, .272f, 0, 0},
            new float[]{.769f+0.3f, .686f, .534f, 0, 0},
            new float[]{.189f, .168f, .131f+0.5f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        ColorMatrix dramatic = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
        {
            new float[]{.393f+0.3f, .349f, .272f, 0, 0},
            new float[]{.769f, .686f+0.2f, .534f, 0, 0},
            new float[]{.189f, .168f, .131f+0.9f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
        });
        #endregion

        #region effects
        public Bitmap Invert(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, invert);
            return currentPicture;
        }
        public Bitmap Gray(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, gray);
            return currentPicture;
        }
        public Bitmap Fog(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, fog);
            return currentPicture;
        }
        public Bitmap Flash(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, flash);
            return currentPicture;
        }
        public Bitmap Frozen(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, frozen);
            return currentPicture;
        }
        public Bitmap Arctic(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, arctic);
            return currentPicture;
        }
        public Bitmap Sepia(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, sepia);
            return currentPicture;
        }
        public Bitmap Kakao(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, kakao);
            return currentPicture;
        }
        public Bitmap Cuji(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, cuji);
            return currentPicture;
        }
        public Bitmap Dramatic(MainWindow mainWindowCls, Bitmap currentPicture)
        {
            currentPicture = MatrixConvertBitmap(currentPicture, dramatic);
            return currentPicture;
        }
        #endregion
        public Bitmap Sliders(MainWindow mainWindowCls, Bitmap original)
        {
            float changered = (float)mainWindowCls.RedSlider.Value * 0.02f;
            float changegreen = (float)mainWindowCls.GreenSlider.Value * 0.02f;
            float changeblue = (float)mainWindowCls.BlueSlider.Value * 0.02f;
            float changetrans = (float)mainWindowCls.TransSlider.Value * 0.02f;
            Bitmap aBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(aBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1+changered, 0, 0, 0, 0},
                    new float[]{0, 1+changegreen, 0, 0, 0},
                    new float[]{0, 0, 1+changeblue, 0, 0},
                    new float[]{0, 0, 0, 1+changetrans, 0},
                    new float[]{0, 0, 0, 0, 1}
                    //new float[] {1, 0, 0, 0, 0},
                    //new float[] {0, 1, 0, 0, 0},
                    //new float[] {0, 0, 1, 0, 0},
                    //new float[] {0, 0, 0, 1, 0},
                    //new float[] {changered, changegreen, changeblue, 0, 1}
                }); ;

            // Set an image attribute to our color matrix so that we can apply it to a bitmap
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(colorMatrix);

            //Uses graphics class to redraw the bitmap with our Color matrix applied
            g.DrawImage(original,                                                                   // Bitmap
                            new System.Drawing.Rectangle(0, 0, original.Width, original.Height),    // Contains the image
                            0,                                                                      // x, y, width, and height
                            0,
                            original.Width,
                            original.Height,
                            GraphicsUnit.Pixel,                                     // Unit of measure
                            attr);                                                  // Our ColorMatrix being applied
            g.Dispose();
            //addPicture(aBitmap);
            return aBitmap;
        }

        public Bitmap MatrixConvertBitmap(Bitmap original, ColorMatrix cM)
        {
            Bitmap aBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(aBitmap);

            ColorMatrix colorMatrix = cM;

            // Set an image attribute to our color matrix so that we can apply it to a bitmap
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(colorMatrix);

            //Uses graphics class to redraw the bitmap with our Color matrix applied
            g.DrawImage(original,                                                                   // Bitmap
                            new System.Drawing.Rectangle(0, 0, original.Width, original.Height),    // Contains the image
                            0,                                                                      // x, y, width, and height
                            0,
                            original.Width,
                            original.Height,
                            GraphicsUnit.Pixel,                                     // Unit of measure
                            attr);                                                  // Our ColorMatrix being applied
            g.Dispose();

            return aBitmap;
        }
    }
}