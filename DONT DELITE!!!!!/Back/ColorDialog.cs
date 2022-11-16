using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DONT_DELITE_____
{
    internal class ColorDialog : Window
    {
        private MainWindow myParentWindow;
        private String controlType;
       // private RGB rgbControl;
        // private BSC bscControl;
        //private GreyCustom greyControl;
        //private CustomMatrix customControl;
        private Crop CropControl;

        /// <summary>
        /// Initialize a Color Dialog
        /// </summary>
        /// <param name="parentWindow">Parent Window, probably this</param>
        /// <param name="cT">A string either: "RGB", "BSC", "Grey", "Matrix"</param>
        public ColorDialog(MainWindow parentWindow, String cT)
        {
            myParentWindow = parentWindow;
            controlType = cT;

            InitControl();
        }


        private void InitControl()
        {
            //if (controlType == "RGB")
            //{
            //    CreateRGB();
            //    this.Width = rgbControl.Width + 26;
            //    this.Height = rgbControl.Height + 26;
            //    this.Title = "Red, Green, and Blue Channel Modifier";
            //}
            //else if (controlType == "BSC")
            //{
            //    CreateColorBSC();
            //    this.Width = bscControl.Width + 26;
            //    this.Height = bscControl.Height + 26;
            //    this.Title = "Brightness, Saturation, and Contrast Modifier";
            //}
            //else if (controlType == "Grey")
            //{
            //    CreateCustomGrey();
            //    this.Width = greyControl.Width + 26;
            //    this.Height = greyControl.Height + 26;
            //    this.Title = "Custom Grayscale Filter";
            //}
            //else if (controlType == "Matrix")
            //{
            //    CreateCustomMatrix();
            //    this.Width = customControl.Width + 26;
            //    this.Height = customControl.Height + 26;
            //    this.Title = "Custom Color Matrix Transform";
            //}
        }

        //private void CreateCustomMatrix()
        //{
        //    customControl = new WPF_Image_Editor.CustomMatrix(myParentWindow, this);
        //    this.Content = customControl;
        //}

        //private void CreateColorBSC()
        //{
        //    bscControl = new WPF_Image_Editor.BSC(myParentWindow, this);
        //    this.Content = bscControl;
        //}

        private void CreateRGB()
        {
            //rgbControl = new DONT_DELITE_____.RGB(myParentWindow, this);
           // this.Content = rgbControl;
        }

        //private void CreateCustomGrey()
        //{
        //    greyControl = new DONT_DELITE_____.GreyCustom(myParentWindow, this);
        //    this.Content = greyControl;
        //}
        private void CreateCustomGrey()
        {
            CropControl = new DONT_DELITE_____.Crop(myParentWindow, this);
            this.Content = CropControl;
        }
        
    }
}

