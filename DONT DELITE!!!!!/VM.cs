using DONT_DELITE_____.Back;
using Microsoft.Graph.SecurityNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using Image = System.Windows.Controls.Image;

namespace DONT_DELITE_____
{
    internal class VM : INotifyPropertyChanged
    {
        private double sliderValueRed;
        private double sliderValueGreen;
        private double sliderValueBlue;
        private Image image;
        private int currentBitmap = 0;
        private RelayCommand openCommand;
        private RelayCommand invertCommand;
        private System.Drawing.Bitmap bmp;
        private System.Drawing.Bitmap buffer;
        private Stack<RelayCommand> commands;
        private List<Bitmap> bitmapList;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public Image SelectedImage
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("SelectedImage");
            }
        }
        //public int currentBitmapNow
        //{
        //    get { return currentBitmap; }
        //    set
        //    {
        //        currentBitmap = value;
        //        OnPropertyChanged("currentBitmap");
        //    }
        //}
        //public List<Bitmap> bitmapListNow
        //{
        //    get { return bitmapList; }
        //    set
        //    {
        //        currentBitmap = value;
        //        OnPropertyChanged("bitmapList");
        //    }
        //}

        
        public VM(Image image)
        {
            this.image = image;
            commands = new Stack<RelayCommand>();
        }
        #region Slider
        public double SliderValueRed
        {
            get { return sliderValueRed; }
            set
            {
                sliderValueRed = value;
                OnPropertyChanged("SliderValueRed");
            }
        }

        public double SliderValueGreen
        {
            get { return sliderValueGreen; }
            set
            {
                sliderValueGreen = value;
                OnPropertyChanged("SliderValueGreen");
            }
        }

        public double SliderValueBlue
        {
            get { return sliderValueBlue; }
            set
            {
                sliderValueBlue = value;
                OnPropertyChanged("SliderValueBlue");
            }
        }
        #endregion

        public RelayCommand OpenCommand => openCommand ??
                   (openCommand = new RelayCommand(obj =>
                   {
                       OpenFile();
                   }));

        private void OpenFile()
        {
            try
            {
                //dialogService = new PictureFileDialogService(image);
                //System.Drawing.Bitmap _base = dialogService.Open();
                //buffer = _base;
                //bmp = _base;
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Вы забыли загрузить изображение", "Упс!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ex.Message + " " + ex.StackTrace);
            }
        }
        

        public RelayCommand InvertCommand => invertCommand ??
                   (invertCommand = new RelayCommand(obj =>
                   {
                       InvertPicture();
                   }));

        private void InvertPicture()
        {
            //dialogService = new PictureFileDialogService(image);
            //dialogService.Invert();
        }
    }
}
