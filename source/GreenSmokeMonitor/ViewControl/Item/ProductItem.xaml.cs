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
    /// ProductItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProductItem : UserControl
    {
        public delegate void PowerChangeHandler(object sender, ProductItemEventArg e);
        public event PowerChangeHandler PowerChange;

        public delegate void SelectItemHandler(object sender, EventArgs e);
        public event SelectItemHandler SelectItem;

        string _ID;
        string _ProductName;
        int _CTValue;
        bool _IsPowerOn = true;
        bool _IsSafeOn = true;
        bool _IsConnected = false;

        public int CTValue
        {
            get { return _CTValue; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ProductName
        {
            get { return _ProductName;}
            set {_ProductName = value;}
        }

        public bool IsPowerOn
        {
            get { return _IsPowerOn; }
        }

        public bool IsSafeOn
        {
            get { return _IsSafeOn; }
        }

        public ImageSource ProductImageSource
        {
            get { return _ConProductImage.Source; }
        }

        public ProductItem(string id, string name, ImageSource productImageSource,bool isPower, bool isSafe)
        {
            InitializeComponent();

            _ID = id;
            _ProductName = name;
            _ConProductImage.Source = productImageSource;
            _IsPowerOn = isPower;
            _IsSafeOn = isSafe;

            _ConProductItem.MouseDown += new MouseButtonEventHandler(_ConProductItem_MouseDown);
        }

        void _ConProductItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard storyboard = Resources["_SBItemMouseOver"] as Storyboard;
            storyboard.Begin(this);

            SelectItem(this, EventArgs.Empty);
        }

        public void ChangeCTValue(int ctValue)
        {
            _CTValue = ctValue;

            Storyboard powerRunningStoryboard = Resources["_SBPowerRunning"] as Storyboard;
            if (_CTValue <= 0)
            {
                powerRunningStoryboard.Stop(_ConPowerBall);

                _ConPowerBall.Visibility = Visibility.Hidden;
            }
            else
            {
                powerRunningStoryboard.SpeedRatio = ctValue * 0.5;

                _ConPowerBall.Visibility = Visibility.Visible;

                powerRunningStoryboard.Begin(this);
            }
        }

        public void Disconnected()
        {
            _IsPowerOn = false;
            _IsConnected = true;
        }

        public void Connected()
        {
            _IsConnected = true;
        }

        public void PowerOn()
        {
            Storyboard storyboard = Resources["SBThisPowerOn"] as Storyboard;
            storyboard.Begin(this);
        }

        public void PowerOff()
        {
            Storyboard storyboard = Resources["SBThisPowerOff"] as Storyboard;
            storyboard.Begin(this);
        }
    }

    public class ProductItemEventArg : EventArgs
    {
        private EnumEventMode _EventMode;

        public EnumEventMode EventMode
        {
            get { return _EventMode; }
        }

        public enum EnumEventMode
        {
            Power,
            Safe,
            NameChanged
        }

        public ProductItemEventArg(EnumEventMode tegetEvent)
        {
            _EventMode = tegetEvent;
        }
    }
}
