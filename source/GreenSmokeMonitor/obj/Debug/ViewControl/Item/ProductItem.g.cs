#pragma checksum "..\..\..\..\ViewControl\Item\ProductItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EEDADC3FCB4E420936A6FC04DCFB9A31"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:2.0.50727.1378
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GreenSmokeMonitor.ViewControl.Item {
    
    
    /// <summary>
    /// ProductItem
    /// </summary>
    public partial class ProductItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        internal System.Windows.Controls.Canvas _ConProductItem;
        
        internal System.Windows.Controls.Canvas _ConProductStar;
        
        internal System.Windows.Shapes.Ellipse _ConProductBackground;
        
        internal System.Windows.Controls.Canvas _ConPowerGroup;
        
        internal System.Windows.Shapes.Path _ConPowerButton;
        
        internal System.Windows.Controls.Label _ConPowerLabel;
        
        internal System.Windows.Controls.Canvas _ConSafeGroup;
        
        internal System.Windows.Shapes.Path _ConSafeButton;
        
        internal System.Windows.Controls.Label _ConSafeLabel;
        
        internal System.Windows.Controls.Image _ConProductImage;
        
        internal System.Windows.Shapes.Ellipse _ConPowerBall;
        
        internal System.Windows.Media.RotateTransform ballRotate;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GreenSmokeMonitor;component/viewcontrol/item/productitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewControl\Item\ProductItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._ConProductItem = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this._ConProductStar = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this._ConProductBackground = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 4:
            this._ConPowerGroup = ((System.Windows.Controls.Canvas)(target));
            return;
            case 5:
            this._ConPowerButton = ((System.Windows.Shapes.Path)(target));
            return;
            case 6:
            this._ConPowerLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this._ConSafeGroup = ((System.Windows.Controls.Canvas)(target));
            return;
            case 8:
            this._ConSafeButton = ((System.Windows.Shapes.Path)(target));
            return;
            case 9:
            this._ConSafeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this._ConProductImage = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this._ConPowerBall = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 12:
            this.ballRotate = ((System.Windows.Media.RotateTransform)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
