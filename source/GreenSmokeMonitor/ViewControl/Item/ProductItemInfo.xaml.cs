using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenSmokeMonitor.ViewControl.Item
{
    /// <summary>
    /// ProductItemInfo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProductItemInfo : UserControl
    {
        bool _IsPower;
        bool _IsSafe;

        public ProductItemInfo()
        {
            InitializeComponent();
        }

        public void SetInfo(string name,ImageSource image ,int runningTime, int currentCT, int totalCT, bool isPower, bool isSafe)
        {
            _LbProductName.Content = name;
            _ImgProduceDetailImg.Source = image;
            _LbProductRunningTime.Content = runningTime.ToString();
            _LBProductCTValue.Content = currentCT.ToString();
            _LBProductCTTotalValue.Content = totalCT.ToString();

            _IsPower = isPower;
            _IsSafe = isSafe;

            PowerChange(_IsPower);
            SafeChange(_IsSafe);

            _CvPowerButton.MouseDown += new MouseButtonEventHandler(_CvPowerButton_MouseDown);
            _CvSafeButton.MouseDown += new MouseButtonEventHandler(_CvSafeButton_MouseDown);
        }

        void _CvPowerButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _IsPower = !_IsPower;
            PowerChange(_IsPower);
        }

        void _CvSafeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _IsSafe = !_IsSafe;
            SafeChange(_IsSafe);
        }

        public void PowerChange(bool isOn)
        {
            Storyboard storyboard;
            if (isOn)
            {
                storyboard = Resources["_SBPowerOn"] as Storyboard;
            }
            else
            {
                storyboard = Resources["_SBPowerOff"] as Storyboard;
            }
            storyboard.Begin(_ElPowerBody);
        }

        public void SafeChange(bool isOn)
        {
            Storyboard storyboard;
            if (isOn)
            {
                storyboard = Resources["_SBSafeOn"] as Storyboard;
            }
            else
            {
                storyboard = Resources["_SBSafeOff"] as Storyboard;
            }
            storyboard.Begin(_ElPowerBody);
        }
    }
}
