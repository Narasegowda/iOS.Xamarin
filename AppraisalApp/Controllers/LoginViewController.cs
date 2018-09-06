// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        private string zipCode;

        Dictionary<string, string> storeNamesID = new Dictionary<string, string>();

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        private bool keyboardPushedUp;

        // Detect the device whether iPad or iPhone
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public override void ViewDidLoad()
        {

            // hide keyboard on touch outside area
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5
            View.AddGestureRecognizer(g);

            if (UserInterfaceIdiomIsPhone)
            {
                Console.WriteLine("width :: " + this.View.Bounds.Width + " Height :: " + this.View.Bounds.Height);
                // iphone 8 plus :: width :: 414 Height :: 736
                // iphone 7 width :: 375 Height :: 667

                if(this.View.Bounds.Width == 375){
                    boxImg.Frame = new CGRect(0, 0, 500, 500);
                }else{
                    boxImg.Frame = new CGRect(0, 0, 550, 550);
                }

                if (this.View.Bounds.Height == 667)
                {
                    boxImg.Center = new CGPoint(this.View.Bounds.Width / 2, this.View.Bounds.Height / 1.5);
                    ComponentView.Center = new CGPoint(this.View.Bounds.Width / 2, this.View.Bounds.Height / 1.5);
                }
                else
                {
                    boxImg.Center = new CGPoint(this.View.Bounds.Width / 2, this.View.Bounds.Height / 2);
                    ComponentView.Center = new CGPoint(this.View.Bounds.Width / 2, this.View.Bounds.Height / 2.2);
                }

                if(this.View.Bounds.Width == 375){
                    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, keyboardWillHide);
                    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, keyboardWillShow);
                }


            }
            else
            {
                //if(InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight){
                //    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, keyboardWillHide);
                //    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, keyboardWillShow);
                //}
            }

            AppDelegate.appDelegate.IsZipCodeValid = false;

            storeLocatorModel = new StoreLocatorModel();

            AppDelegate.appDelegate.IsInfoSaved = false;
            AppDelegate.appDelegate.IsFactorySaved = false;
            AppDelegate.appDelegate.IsAftermarketSaved = false;
            AppDelegate.appDelegate.IsHistorySaved = false;
            AppDelegate.appDelegate.IsReconditionsSaved = false;
            AppDelegate.appDelegate.IsPhotosSaved = false;
            AppDelegate.appDelegate.IsAllDataSaved = false;

            txtZip.ShouldReturn = (tf) =>
            {

                return true;
            };

        }

        partial void GetStartBtn_TouchUpInside(UIButton sender)
        {
            GoClick();
        }

        public void GoClick()
        {
            try
            {

                if (!AppDelegate.appDelegate.IsZipCodeValid)
                {
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
                        zipCode = txtZip.Text;
                        Utility.ShowLoadingIndicator(this.View, "", true);
                        CallWebservice();
                    }
                }
                else
                {
                    AppDelegate.appDelegate.IsZipCodeValid = false;
                    this.PerformSegue("decodeSegue", this);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception occured :: " + exc.Message);
            }
        }

        Task CallWebservice()
        {
            return Task.Factory.StartNew(() =>
            {
                ServiceCall();
            });
        }

        private void ServiceCall()
        {
            string code = null;
            code = ServiceFactory.getWebServiceHandle().ValidateZipDealer(Convert.ToInt32(zipCode));

            if (code == null)
            {
                List<Stores> storesList = ServiceFactory.getWebServiceHandle().SearchNearestStores(zipCode);
                if (null != storesList && storesList.Count > 0)
                {
                    InvokeOnMainThread(() =>
                    {
                        Utility.HideLoadingIndicator(this.View);
                        AnimateFlipHorizontally(txtZip, true, 0.5, null);
                        AnimateFlipHorizontally(GetStartBtn, true, 0.5, null);
                        txtZip.Text = "";
                        GetStartBtn.SetTitle("Go", UIControlState.Normal);
                        AppDelegate.appDelegate.IsZipCodeValid = true;

                        foreach (Stores stores in storesList)
                        {
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
                            UIBarButtonItem doneBtn = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) =>
                            {
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
                    });


                }
                else
                {
                    InvokeOnMainThread(() =>
                    {
                        Utility.HideLoadingIndicator(this.View);
                        Utility.ShowAlert("Appraisal App", "No Nearest Stores Found!!", "OK");
                    });

                }
            }
            else if (null != code)
            {
                AppDelegate.appDelegate.storeId = Convert.ToInt16(code);
                InvokeOnMainThread(() =>
                {
                    this.PerformSegue("decodeSegue", this);
                });

            }
            else
            {
                InvokeOnMainThread(() =>
                {
                    Utility.ShowAlert("ZIP/Dealer", "Please Enter valid ZIP/Dealer Code", "OK");
                });
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

        private void keyboardWillHide(NSNotification notification)
        {
            try
            {
                keyboardPushedUp = false;
                CGPoint point = scrollview.ContentOffset;
                point.Y = point.Y - 90;
                scrollview.SetContentOffset(point, true);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Null exception :: " + ex.Message);
            }
            catch (Exception ex1)
            {
                Debug.WriteLine("Exception occurred :: " + ex1.Message);
            }
        }

        private void keyboardWillShow(NSNotification notification)
        {
            try
            {
                if (!keyboardPushedUp)
                {
                    CGPoint point = scrollview.ContentOffset;
                    point.Y = point.Y + 90;
                    scrollview.SetContentOffset(point, true);
                    keyboardPushedUp = true;
                }


            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Null exception :: " + ex.Message);
            }
            catch (Exception ex1)
            {
                Debug.WriteLine("Exception occurred :: " + ex1.Message);
            }
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
                () =>
                {
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
