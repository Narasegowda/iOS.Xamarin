using ExtAppraisalApp.Utilities; using Foundation; using System; using System.Threading.Tasks; using UIKit;  namespace ExtAppraisalApp {     public partial class PhotoViewController : UIViewController     {
        partial void PhotosSegment_Changed(UISegmentedControl sender)
        {
            var segmentIndex = sender.SelectedSegment;              if(segmentIndex == 0){                 AdditionalPhotosContainer.Hidden = true;                 PhotosContainer.Hidden = false;             }else{                 PhotosContainer.Hidden = true;                 AdditionalPhotosContainer.Hidden = false;             }
        }          private MasterViewController masterViewController;          // Detect the device whether iPad or iPhone         static bool UserInterfaceIdiomIsPhone         {             get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }         }                   partial void PhotosSaveBtn_Activated(UIBarButtonItem sender)         {             if(!AppDelegate.appDelegate.LeftCarImageUploaded){                  var dictionary = new NSDictionary(new NSString("Left"), new NSString("Left"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs",null, dictionary);                 //Left.Layer.BorderColor = UIColor.Red.CGColor;             }              if(!AppDelegate.appDelegate.RightCarImageUploaded){                 var dictionary = new NSDictionary(new NSString("Right"), new NSString("Right"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs",null, dictionary);                 //Right.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.SeatCarImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Seat"), new NSString("Seat"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Seat.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.BackSeatImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Seats"), new NSString("Seats"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Seats.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.FrontCarImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Front"), new NSString("Front"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Front.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.BackCarImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Back"), new NSString("Back"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Back.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.OdometerImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Odometer"), new NSString("Odometer"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Odometer.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.DashBoardImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Dashboard"), new NSString("Dashboard"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Dashboard.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.VINImageUplaoded)             {                 var dictionary = new NSDictionary(new NSString("VIN"), new NSString("VIN"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //VIN.Layer.BorderColor = UIColor.Red.CGColor;             }              if (!AppDelegate.appDelegate.RimImageUploaded)             {                 var dictionary = new NSDictionary(new NSString("Rim"), new NSString("Rim"));                  NSNotificationCenter.DefaultCenter.PostNotificationName("UpdatePhotoGraphs", null, dictionary);                 //Rim.Layer.BorderColor = UIColor.Red.CGColor;             }              if(AppDelegate.appDelegate.LeftCarImageUploaded && AppDelegate.appDelegate.RightCarImageUploaded && AppDelegate.appDelegate.SeatCarImageUploaded && AppDelegate.appDelegate.BackSeatImageUploaded && AppDelegate.appDelegate.FrontCarImageUploaded && AppDelegate.appDelegate.BackCarImageUploaded                 && AppDelegate.appDelegate.OdometerImageUploaded && AppDelegate.appDelegate.DashBoardImageUploaded && AppDelegate.appDelegate.VINImageUplaoded && AppDelegate.appDelegate.RimImageUploaded){                 // Navigate to Summary                  if (null == masterViewController)                 {                     if (!UserInterfaceIdiomIsPhone)                         masterViewController = (MasterViewController)((UINavigationController)SplitViewController.ViewControllers[0]).TopViewController;                 }                  ViewWorker viewWorker = new ViewWorker();                 viewWorker.WorkerDelegate = (ExtAppraisalApp.Utilities.WorkerDelegateInterface)masterViewController;                 viewWorker.ShowDoneImg(6);                  AppDelegate.appDelegate.IsPhotosSaved = true;                  this.PerformSegue("summarySegue", this);             }                     }          protected PhotoViewController(IntPtr handle) : base(handle)         {             // Note: this .ctor should not contain any initialization logic.         }          public async void ActivityLoader()         {             //LoadingOverlay loadPop;             //var bounds = UIScreen.MainScreen.Bounds;             //loadPop = new LoadingOverlay(bounds);             //View.Add(loadPop);             Utility.ShowLoadingIndicator(this.View, "Uploading ...", true);             await Task.Delay(2000);             Utility.HideLoadingIndicator(this.View);             //loadPop.Hide();         }          public override void ViewDidLoad()         {             PhotosContainer.Hidden = false;             AdditionalPhotosContainer.Hidden = true;               base.ViewDidLoad();         }



        private void setPersistedImage()         {               //    UIImage rightImage = LoadImage("right");             //if (rightImage != null)             //{             //    Right.SetBackgroundImage(rightImage, UIControlState.Normal);             //    Right.TintColor = UIColor.Clear;             //    RightCarImageUploaded = true;             //    Right.Layer.BorderColor = UIColor.Black.CGColor;             //}             //UIImage leftImage = LoadImage("left");             //if (leftImage != null)             //{             //    Left.TintColor = UIColor.Clear;             //    Left.SetBackgroundImage(leftImage, UIControlState.Normal);             //    LeftCarImageUploaded = true;             //    Left.Layer.BorderColor = UIColor.Black.CGColor;              //}             //UIImage frontImage = LoadImage("front");             //if (frontImage != null)             //{                              //    Front.TintColor = UIColor.Clear;             //    Front.SetBackgroundImage(frontImage, UIControlState.Normal);             //    FrontCarImageUploaded = true;             //    Front.Layer.BorderColor = UIColor.Black.CGColor;             //}             //UIImage backImage = LoadImage("back");             //if (backImage != null)             //{             //    Back.TintColor = UIColor.Clear;             //    Back.SetBackgroundImage(backImage, UIControlState.Normal);             //    BackCarImageUploaded = true;             //    Back.Layer.BorderColor = UIColor.Black.CGColor;              //}             //UIImage seatImage = LoadImage("seat");             //if (seatImage != null)             //{             //    Seat.TintColor = UIColor.Clear;             //    Seat.SetBackgroundImage(seatImage, UIControlState.Normal);             //    SeatCarImageUploaded = true;             //    Left.Layer.BorderColor = UIColor.Black.CGColor;             //}             //UIImage seatsImage = LoadImage("seats");             //if (seatsImage != null)             //{             //    Seats.TintColor = UIColor.Clear;             //    Seats.SetBackgroundImage(seatsImage, UIControlState.Normal);             //    BackSeatImageUploaded = true;             //    Seats.Layer.BorderColor = UIColor.Black.CGColor;             //}             //UIImage dashImage = LoadImage("dashboard");             //if (dashImage != null)             //{             //    Dashboard.TintColor = UIColor.Clear;             //    Dashboard.SetBackgroundImage(dashImage, UIControlState.Normal);             //    DashBoardImageUploaded = true;             //    Dashboard.Layer.BorderColor = UIColor.Black.CGColor;              //}             //UIImage odoImage = LoadImage("odometer");             //if (odoImage != null)             //{             //    Odometer.TintColor = UIColor.Clear;             //    Odometer.SetBackgroundImage(odoImage, UIControlState.Normal);             //    OdometerImageUploaded = true;             //    Odometer.Layer.BorderColor = UIColor.Black.CGColor;              //}             //UIImage rimImage = LoadImage("rim");             //if (rimImage != null)             //{             //    Rim.TintColor = UIColor.Clear;             //    Rim.SetBackgroundImage(rimImage, UIControlState.Normal);             //    RimImageUploaded = true;             //    Rim.Layer.BorderColor = UIColor.Black.CGColor;              //}             //UIImage vinImage = LoadImage("VIN");             //if (vinImage != null)             //{             //    VIN.TintColor = UIColor.Clear;             //    VIN.SetBackgroundImage(vinImage, UIControlState.Normal);             //    VINImageUplaoded = true;             //    VIN.Layer.BorderColor = UIColor.Black.CGColor;             //}          }         public UIImage LoadImage(string filename)           {             string buttonName = filename + ".png";             var documentsDirectory = Environment.GetFolderPath                                              (Environment.SpecialFolder.Personal);             string pngFilename = System.IO.Path.Combine(documentsDirectory, buttonName);              UIImage image = null;             //var jpegData = NSData.FromFile(pngFilename);             //if (jpegData != null)                 image = UIImage.FromFile(pngFilename);             return image;         }     } }  