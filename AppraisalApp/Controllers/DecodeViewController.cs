// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExtAppraisalApp.Models;
using ExtAppraisalApp.Services;
using ExtAppraisalApp.Utilities;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace ExtAppraisalApp
{
    public partial class DecodeViewController : UITableViewController
    {
        partial void txtEmail_Changed(UITextField sender)
        {   if(txtEmail.Text.Length > 0)
            {
                txtEmail.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);
                txtPhone.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);

            }


        }

        partial void txtLastName_Changed(UITextField sender)
        {
            txtLastName.Text = Regex.Replace(txtLastName.Text, @"[^a-zA-Z]+", "");
            txtLastName.ShouldChangeCharacters = (UITextField txt, NSRange range, string oopsTxt) =>
            {
                var newLength = txt.Text.Length + oopsTxt.Length - range.Length;
                return newLength <= 50;

            };
        }

        partial void txtFirstName_Changed(UITextField sender)
        {
            txtFirstName.Text = Regex.Replace(txtFirstName.Text, @"[^a-zA-Z]+", "");

            txtFirstName.ShouldChangeCharacters = (UITextField txt, NSRange range, string oopsTxt) =>
            {
                var newLength = txt.Text.Length + oopsTxt.Length - range.Length;
                return newLength <= 50;

            };
        }


        partial void txtVin_Changed(UITextField sender)
        {
            txtVin.Text = txtVin.Text.ToUpper();
        }


        partial void txtPhone_Changed(UITextField sender)
        {
            const int maxCharacters = 10;
            txtPhone.Text = Regex.Replace(txtPhone.Text, @"[^0-9]+", "");

            txtPhone.ShouldChangeCharacters = (textField, range, replacement) =>
            {
                var newContent = new NSString(textField.Text).Replace(range, new NSString(replacement)).ToString();
                int number;
                if(newContent.Length> 0){
                    txtEmail.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);
                    txtPhone.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);

                }
                return newContent.Length <= maxCharacters && (replacement.Length == 0 || int.TryParse(replacement, out number));
            };

        }

        partial void txtMileage_Changed(UITextField sender)
        {
            const int maxCharacters = 6;
            txtMileage.Text = Regex.Replace(txtMileage.Text, @"[^0-9]+", "");

            txtMileage.ShouldChangeCharacters = (textField, range, replacement) =>
            {
                var newContent = new NSString(textField.Text).Replace(range, new NSString(replacement)).ToString();
                int number;
                return newContent.Length <= maxCharacters && (replacement.Length == 0 || int.TryParse(replacement, out number));
            };
        }

        private LoginViewController loginViewController;

        public void SetDetailItem(LoginViewController masterViewController)
        {
            this.loginViewController = masterViewController;
        }

        partial void BtnCancel_Activated(UIBarButtonItem sender)
        {
            this.DismissModalViewController(true);


            ViewWorker worker = new ViewWorker();
            worker.WorkerDelegate = loginViewController;
            worker.UpdateUI(false);
        }

        partial void BtnDecode_Activated(UIBarButtonItem sender)
        {
            View.EndEditing(true);
            DoneDecodeVin();
        }

        public void DoneDecodeVin()
        {
            try
            {
                string email = txtEmail.Text;
                string vin = txtVin.Text;
                string firstname = txtFirstName.Text;
                string lastname = txtLastName.Text;
                string mileage = txtMileage.Text;
                string phone = txtPhone.Text;

                if (string.IsNullOrEmpty(vin) || string.IsNullOrEmpty(mileage) || string.IsNullOrEmpty(firstname) ||string.IsNullOrEmpty(lastname) || (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone)))
                
                {
                    txtVin.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);
                    txtMileage.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);
                    txtFirstName.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);
                    txtLastName.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);

                    if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
                    {
                        txtEmail.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);
                        txtPhone.AttributedPlaceholder = new NSAttributedString("Required", null, UIColor.Red);

                    }

                }
                else if(phone=="" && !(Regex.Match(email, "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$").Success)){
                    
                    Utility.ShowAlert("CarCash", "Your Email (" + email + ") is Incorrect", "OK");
                }
                else
                {
                    txtEmail.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);
                    txtPhone.AttributedPlaceholder = new NSAttributedString("", null, UIColor.Red);
                    var UserID =UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();

                    System.Diagnostics.Debug.WriteLine("USER ID :: " + UserID);

                    if(UserID == null)
                    {
                        UserID = Convert.ToString(new Guid());
                    }

                    Utility.ShowLoadingIndicator(this.View, "Creating Appraisal", true);
                    CallWebservice(txtVin.Text, AppDelegate.appDelegate.storeId, Convert.ToInt32(txtMileage.Text), UserID, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text);
                }

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine("Exception occured :: " + exc.Message);
            }
        }


        Task CallWebservice(string Vin, short storeId, int mileage, string ddcuserid, string firstname, string lastname, string email, string phone)
        {
            return Task.Factory.StartNew(() => {
                CreateAppraisalRequest apprrequest = new CreateAppraisalRequest();
                AppraisalResponse appresponse = new AppraisalResponse();
                apprrequest.VIN = Vin;
                apprrequest.StoreID = storeId;
                apprrequest.Mileage = mileage;
                apprrequest.DDCUserId = ddcuserid;
                apprrequest.FirstName = firstname;
                apprrequest.LastName = lastname;
                apprrequest.Phone = phone;
                apprrequest.Email = email;
                apprrequest.Is_Extrn_Customer = AppDelegate.appDelegate.IsCustomer;
               
                appresponse = ServiceFactory.getWebServiceHandle().CreateAppraisalKBB(apprrequest);

                Console.WriteLine("vehicle id :: " + appresponse.VehicleID);

                if (null != appresponse.VIN)
                {
                    
                    InvokeOnMainThread(() =>
                    {
                        Utility.HideLoadingIndicator(this.View);
                        AppDelegate.appDelegate.vehicleID = appresponse.VehicleID;
                        AppDelegate.appDelegate.storeId = appresponse.StoreID;
                        AppDelegate.appDelegate.invtrId = appresponse.InvtrID;
                        AppDelegate.appDelegate.trimId = appresponse.KBBTrimId;
                        AppDelegate.appDelegate.mileage = Convert.ToInt32(txtMileage.Text);

                        var storyboard = UIStoryboard.FromName("Main", null);
                        var splitViewController = storyboard.InstantiateViewController("SplitViewControllerID");
                        var appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
                        appDelegate.Window.RootViewController = splitViewController;
                    });

                }
                else
                {
                    InvokeOnMainThread(() =>
                    {
                        Utility.HideLoadingIndicator(this.View);
                        Utility.ShowAlert("AppraisalApp", "Decode VIN Failed !!", "OK");
                    });

                }


            });
        }

        public override void WillDisplayHeaderView(UITableView tableView, UIView headerView, nint section)
        {
            if (headerView.GetType() == typeof(UITableViewHeaderFooterView))
            {
                UITableViewHeaderFooterView tableViewHeaderFooterView = (UITableViewHeaderFooterView)headerView;
                tableViewHeaderFooterView.TextLabel.TextColor = UIColor.Black;
                var font = UIFont.SystemFontOfSize(18);
                tableViewHeaderFooterView.TextLabel.Font = font;
                tableViewHeaderFooterView.TextLabel.TextAlignment = UITextAlignment.Natural;
            }


        }

        ZXing.Mobile.MobileBarcodeScanner scanner;

        public override void ViewDidLoad()
        {
            try
            {
                txtVin.ShouldChangeCharacters = (textField, range, replacementString) => {
                    var newLength = textField.Text.Length + replacementString.Length - range.Length;
                    return newLength <= 17;
                };
                txtPhone.ShouldChangeCharacters = (textField, range, replacementString) => {
                    var newLength = textField.Text.Length + replacementString.Length - range.Length;
                    return newLength <= 10;
                };

                txtMileage.ShouldChangeCharacters = (textField, range, replacementString) => {
                    var newLength = textField.Text.Length + replacementString.Length - range.Length;
                    return newLength <= 6;
                };

                txtVin.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
                txtVin.ShouldReturn = (tf) =>
                {
                    //txtMileage.SecureTextEntry = true;
                    txtMileage.ReturnKeyType = UIReturnKeyType.Next;
                    txtMileage.BecomeFirstResponder();
                    return true;
                };
                txtMileage.ShouldReturn = (tf) =>
                {
                    //txtFirstName.SecureTextEntry = true;
                    txtFirstName.ReturnKeyType = UIReturnKeyType.Next;
                    txtFirstName.BecomeFirstResponder();
                    return true;
                };
                txtFirstName.ShouldReturn = (tf) =>
                {
                    txtLastName.ReturnKeyType = UIReturnKeyType.Next;
                    txtLastName.BecomeFirstResponder();
                    return true;
                };
                txtLastName.ShouldReturn = (tf) =>
                {
                    txtEmail.ReturnKeyType = UIReturnKeyType.Next;
                    txtEmail.BecomeFirstResponder();
                    return true;
                };
                txtEmail.ShouldReturn = (tf) =>
                {
                    txtPhone.ReturnKeyType = UIReturnKeyType.Done;
                    txtPhone.BecomeFirstResponder();
                    return true;
                };
                txtPhone.ShouldReturn = (tf) =>
                {
                    txtPhone.EndEditing(true);
                    DoneDecodeVin();
                    return true;
                };

                if(AppDelegate.appDelegate.CustomerLogin){
                    
                    if(!string.IsNullOrEmpty(AppDelegate.appDelegate.GuestLastName))
                    {
                        txtLastName.Text = AppDelegate.appDelegate.GuestLastName;
                    }
                    if (!string.IsNullOrEmpty(AppDelegate.appDelegate.GuestEmail))
                    {
                        txtEmail.Text = AppDelegate.appDelegate.GuestEmail;
                        txtPhone.Placeholder = "";
                    }

                    if (!string.IsNullOrEmpty(AppDelegate.appDelegate.GuestPhone))
                    {
                        txtPhone.Text = AppDelegate.appDelegate.GuestPhone;
                        txtEmail.Placeholder = "";
                    }
                }

                // hide keyboard on touch outside area
                var g = new UITapGestureRecognizer(() => View.EndEditing(true));
                g.CancelsTouchesInView = false; //for iOS5
                View.AddGestureRecognizer(g);

                base.ViewDidLoad();
                AppDelegate.appDelegate.photoAcesss = false;
                // Perform any additional setup after loading the view, typically from a nib.

                if (this.btnScan != null)
                {
                    this.btnScan.TouchUpInside += async (sender, e) =>
                    {
                        scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
                        scanner.UseCustomOverlay = false;
                        scanner.TopText = "Hold camera up to barcode to scan";
                        scanner.BottomText = "Barcode will automatically scan";
                        //scanner.FlashButtonText = "Flash";
                        scanner.CancelButtonText = "Cancel";
                        //scanner.Torch(true);
                        scanner.AutoFocus();
                        var result = await scanner.Scan(true);
                        HandleScanResult(result);
                    };
                }

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine("Exception :: " + exc.Message);
            }
        }
        public DecodeViewController() : base("DecodeViewController", null)
        {
        }

        public DecodeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void HandleScanResult(ZXing.Result result)
        {
            string strScannedVIN = "";
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                if (result.Text.Length == 18 && result.Text.StartsWith("I", StringComparison.Ordinal))
                {
                    String TrimmedText = result.Text.Substring(1);
                    System.Diagnostics.Debug.WriteLine("After Trimming:" + TrimmedText);
                    strScannedVIN = TrimmedText;
                }
                else
                {
                    strScannedVIN = result.Text;
                }

                txtVin.Text = strScannedVIN;

                //UIPasteboard.General.String = strScannedVIN;
                //Task.ShowToast("Copied to Clipboard").SetDuration(5000);
            }
            else
            {
                Utility.ShowAlert("Vin Scan", "Scan Failed!!", "OK");

            }
        }

    }
}