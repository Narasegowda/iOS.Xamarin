// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using AppraisalApp.Models;
using CoreAnimation;
using CoreGraphics;
using ExtAppraisalApp.Services;
using ExtAppraisalApp.Utilities;
using Foundation;
using UIKit;

namespace ExtAppraisalApp
{

    public partial class LoginViewController : UIViewController, WorkerDelegateInterface
    {
        private UIPickerView pickerView;

        public static StoreLocatorModel storeLocatorModel;

        Dictionary<string, string> storeNamesID = new Dictionary<string, string>();

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            
            // hide keyboard on touch outside area
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5
            View.AddGestureRecognizer(g);

            AppDelegate.appDelegate.IsZipCodeValid = false;

            storeLocatorModel = new StoreLocatorModel();

            AppDelegate.appDelegate.IsInfoSaved = false;
            AppDelegate.appDelegate.IsFactorySaved = false;
            AppDelegate.appDelegate.IsAftermarketSaved = false;
            AppDelegate.appDelegate.IsHistorySaved = false;
            AppDelegate.appDelegate.IsReconditionsSaved = false;
            AppDelegate.appDelegate.IsPhotosSaved = false;

        }

        partial void GetStartBtn_TouchUpInside(UIButton sender)
        { 
            try{
               
                if(!AppDelegate.appDelegate.IsZipCodeValid){
                    string zip = txtZip.Text;

                    if (zip == "")
                    {
                        Utility.ShowAlert("ZIP/Dealer Code", "A ZIP/Dealer is required.!!", "OK");

                    }
                    else if (!(zip.Length == 6 || zip.Length == 5))
                    {
                        Utility.ShowAlert("ZIP/Dealer Code", "Your ZIP/Dealer (" + zip + ") is Incorrect", "OK");

                    }
                    else
                    {
                        string code = null;
                        code = ServiceFactory.getWebServiceHandle().ValidateZipDealer(Convert.ToInt32(txtZip.Text));



                        if (code == null)
                        {
                            List<Stores> storesList = ServiceFactory.getWebServiceHandle().SearchNearestStores(txtZip.Text);
                            if(null != storesList){
                                
                                AnimateFlipHorizontally(txtZip, true, 0.5, null);
                                AnimateFlipHorizontally(GetStartBtn, true, 0.5, null);
                                txtZip.Text = "";
                                GetStartBtn.SetTitle("Go", UIControlState.Normal);
                                AppDelegate.appDelegate.IsZipCodeValid = true;

                                foreach(Stores stores in storesList){
                                    storeNamesID.Add(stores.OrgID, stores.Name);
                                    storeLocatorModel.Items.Add(stores.Name);
                                }

                                if (AppDelegate.appDelegate.IsZipCodeValid)
                                {
                                    pickerView = new UIPickerView();
                                    pickerView.Model = storeLocatorModel;
                                    pickerView.ShowSelectionIndicator = true;

                                    // To create a toolbar with done button
                                    UIToolbar toolbar = new UIToolbar();
                                    toolbar.BarStyle = UIBarStyle.Black;
                                    toolbar.Translucent = true;
                                    toolbar.SizeToFit();
                                    UIBarButtonItem flexibleSpaceLeft = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null, null);
                                    UIBarButtonItem doneBtn = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) => {
                                        foreach (UIView view in this.View.Subviews)
                                        {
                                            txtZip.Text = storeLocatorModel.Items[(int)pickerView.SelectedRowInComponent(0)].ToString();
                                            var Id = storeNamesID.FirstOrDefault(x => x.Value.Contains(txtZip.Text)).Key;


                                            AppDelegate.appDelegate.storeId = short.Parse(Id);
                                            System.Diagnostics.Debug.WriteLine(" id :: " + AppDelegate.appDelegate.storeId);
         

                                            txtZip.Text = storeLocatorModel.Items[(int)pickerView.SelectedRowInComponent(0)].ToString();
                                            txtZip.ResignFirstResponder();
                                        }

                                    });
                                    toolbar.SetItems(new UIBarButtonItem[] { flexibleSpaceLeft, doneBtn }, true);

                                    // To assign inputview has pickerview
                                    txtZip.InputView = pickerView;
                                    txtZip.InputAccessoryView = toolbar;

                                    txtZip.TouchDown += SetPicker;
                                    txtZip.Placeholder = "Select Stores";
                                }

                            }
                        }
                        else if(null != code){
                            AppDelegate.appDelegate.storeId = Convert.ToInt16(code);
                            this.PerformSegue("decodeSegue", this);
                        }
                        else
                        {
                            Utility.ShowAlert("ZIP/Dealer", "Please Enter valid ZIP/Dealer Code", "OK");

                        }
                    }
                }else{
                    // add store locator here
                    AppDelegate.appDelegate.IsZipCodeValid = false;
                    this.PerformSegue("decodeSegue", this);
                }

            }catch(Exception exc){
                Console.WriteLine("Exception occured :: " + exc.Message);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "decodeSegue")
            {
                var controller = (DecodeViewController)((UINavigationController)segue.DestinationViewController).TopViewController;

                controller.SetDetailItem(this);
            }
        }

        private void SetPicker(object sender, EventArgs e)
        {
            UITextField field = (UITextField)sender;
            if (field.Text != "")
                pickerView.Select(storeLocatorModel.Items.IndexOf(field.Text), 0, true);
        }

        // Inner Class : StatusChange Model : Pickerview
        public class StoreLocatorModel : UIPickerViewModel
        {
            public event EventHandler<EventArgs> ValueChanged;

            /// <summary>
            /// The items to show up in the picker
            /// </summary>
            public List<string> Items { get; private set; }

            /// <summary>
            /// The current selected item
            /// </summary>
            public string SelectedItem
            {
                get { return Items[selectedIndex]; }
            }

            int selectedIndex = 0;

            public StoreLocatorModel()
            {
                Items = new List<string>();
            }

            /// <summary>
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Items.Count;
            }

            /// <summary>
            /// called by the picker to get the text for a particular row in a particular
            /// spinner item
            /// </summary>
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Items[(int)row];
            }

            /// <summary>
            /// called by the picker to get the number of spinner items
            /// </summary>
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            /// <summary>
            /// called when a row is selected in the spinner
            /// </summary>
            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                selectedIndex = (int)row;
                if (ValueChanged != null)
                {
                    ValueChanged(this, new EventArgs());
                }
            }

        }


        public void AnimateFlipHorizontally(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
        {
            var m34 = (nfloat)(-1 * 0.001);

            var minTransform = CATransform3D.Identity;
            minTransform.m34 = m34;
            minTransform = minTransform.Rotate((nfloat)((isIn ? 1 : -1) * Math.PI * 0.5), (nfloat)0.0f, (nfloat)1.0f, (nfloat)0.0f);
            var maxTransform = CATransform3D.Identity;
            maxTransform.m34 = m34;

            view.Layer.Transform = isIn ? minTransform : maxTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () => {
                    view.Layer.AnchorPoint = new CGPoint((nfloat)0.5, (nfloat)0.5f);
                    view.Layer.Transform = isIn ? maxTransform : minTransform;
                },
                onFinished
            );
        }

        public void UpdateDatas(bool show)
        {
            txtZip.Text = "";
            txtZip.Placeholder = "ZIP/DEALER CODE";
            txtZip.EndEditing(true);
            this.View.EndEditing(true);
            GetStartBtn.SetTitle("Get Started", UIControlState.Normal);
            AppDelegate.appDelegate.IsZipCodeValid = false;
        }

        public void performNavigate(int index)
        {
            System.Diagnostics.Debug.WriteLine("perform Navigate");
        }

        public void ShowDoneIcon(int index)
        {
            System.Diagnostics.Debug.WriteLine("perform Navigate");
        }

        public void ShowPartialDoneIcon(int index)
        {
            System.Diagnostics.Debug.WriteLine("perform Navigate");
        }
    }

}
