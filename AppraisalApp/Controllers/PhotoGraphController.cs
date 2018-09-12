using ExtAppraisalApp.Utilities;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace ExtAppraisalApp
{
    public partial class PhotoGraphController : UIViewController
    {
        private MasterViewController masterViewController;

        private bool LeftCarImageUploaded = false;
        private bool RightCarImageUploaded = false;
        private bool SeatCarImageUploaded = false;
        private bool BackSeatImageUploaded = false;
        private bool FrontCarImageUploaded = false;
        private bool BackCarImageUploaded = false;
        private bool DashBoardImageUploaded = false;
        private bool VINImageUplaoded = false;
        private bool RimImageUploaded = false;
        private bool OdometerImageUploaded = false;

        // Detect the device whether iPad or iPhone
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }



        //partial void PhotosSaveBtn_Activated(UIBarButtonItem sender)
        //{
        //    if (!LeftCarImageUploaded)
        //    {
        //        Left.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!RightCarImageUploaded)
        //    {
        //        Right.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!SeatCarImageUploaded)
        //    {
        //        Seat.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!BackSeatImageUploaded)
        //    {
        //        Seats.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!FrontCarImageUploaded)
        //    {
        //        Front.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!BackCarImageUploaded)
        //    {
        //        Back.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!OdometerImageUploaded)
        //    {
        //        Odometer.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!DashBoardImageUploaded)
        //    {
        //        Dashboard.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!VINImageUplaoded)
        //    {
        //        VIN.Layer.BorderColor = UIColor.Red.CGColor;
        //    }
        //    if (!RimImageUploaded)
        //    {
        //        Rim.Layer.BorderColor = UIColor.Red.CGColor;
        //    }

        //    if (LeftCarImageUploaded && RightCarImageUploaded && SeatCarImageUploaded && BackSeatImageUploaded && FrontCarImageUploaded && BackCarImageUploaded
        //       && OdometerImageUploaded && DashBoardImageUploaded && VINImageUplaoded && RimImageUploaded)
        //    {
        //        // Navigate to Summary 
        //        if (null == masterViewController)
        //        {
        //            if (!UserInterfaceIdiomIsPhone)
        //                masterViewController = (MasterViewController)((UINavigationController)SplitViewController.ViewControllers[0]).TopViewController;
        //        }

        //        ViewWorker viewWorker = new ViewWorker();
        //        viewWorker.WorkerDelegate = (ExtAppraisalApp.Utilities.WorkerDelegateInterface)masterViewController;
        //        viewWorker.ShowDoneImg(6);

        //        AppDelegate.appDelegate.IsPhotosSaved = true;

        //        this.PerformSegue("summarySegue", this);
        //    }


        //}


        void ActionButton_TouchUpInside()
        {
            var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            var image = UIImage.FromBundle("camera.png");
            var camera = UIAlertAction.Create("Camera ", UIAlertActionStyle.Default, (s) => { CameraBtna_TouchUpInside(); });
            camera.SetValueForKey(image, new NSString("image"));


            alert.AddAction(camera);

            var gallery = UIAlertAction.Create("Gallery", UIAlertActionStyle.Default, (k) => { GalleryButtona_TouchUpInside(); });
            var galleryicon = UIImage.FromBundle("gallery.png");
            gallery.SetValueForKey(galleryicon, new NSString("image"));
            alert.AddAction(gallery);

            var cancelaction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
            alert.AddAction(cancelaction);
            UIPopoverPresentationController presentationPopover = alert.PopoverPresentationController;
            if (presentationPopover != null)
            {
                presentationPopover.SourceView = this.View;
                presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Right;
            }
            this.PresentViewController(alert, true, null);
        }




        string currentButton = null;

        //partial void Leftb_Image_TouchUpInside(UIButton sender)
        //{
        //   // viewPopup.Hidden = false;
        //    currentButton = "left";
        //    ActionButton_TouchUpInside();

        //}


        partial void Front_TouchUpInside(UIButton sender)
        {
            //viewPopup.Hidden = false;
            currentButton = "front";
            ActionButton_TouchUpInside();


        }



        partial void Back_TouchUpInside(UIButton sender)
        {
            //viewPopup.Hidden = false;
            currentButton = "back";
            ActionButton_TouchUpInside();

        }

        partial void Left_TouchUpInside(UIButton sender)
        {
            currentButton = "left";
            ActionButton_TouchUpInside();
        }

        partial void Right_TouchUpInside(UIButton sender)
        {
            currentButton = "right";
            ActionButton_TouchUpInside();
        }
        partial void Seats_TouchUpInside(UIButton sender)
        {
            currentButton = "seats";
            ActionButton_TouchUpInside();
        }

        partial void Seat_TouchUpInside(UIButton sender)
        {
            currentButton = "seat";
            ActionButton_TouchUpInside();
        }
        partial void Odometer_TouchUpInside(UIButton sender)
        {
            currentButton = "odometer";
            ActionButton_TouchUpInside();
        }

        partial void Dashboard_TouchUpInside(UIButton sender)
        {
            currentButton = "dashboard";
            ActionButton_TouchUpInside();
        }
        partial void Rim_TouchUpInside(UIButton sender)
        {
            currentButton = "rim";
            ActionButton_TouchUpInside();
        }

        partial void VIN_TouchUpInside(UIButton sender)
        {
            currentButton = "VIN";
            ActionButton_TouchUpInside();
        }
        //partial void Odometer_TouchUpInside(UIButton sender)
        //{
        //    //viewPopup.Hidden = false;
        //    currentButton = "odometer";
        //    ActionButton_TouchUpInside();
        //}

        //partial void Dashboard_TouchUpInside(UIButton sender)
        //{
        //    //viewPopup.Hidden = false;
        //    currentButton = "dashboard";
        //    ActionButton_TouchUpInside();
        //}

        //partial void Seat_TouchUpInside(UIButton sender)
        //{
        //    //viewPopup.Hidden = false;
        //    currentButton = "seat";
        //    ActionButton_TouchUpInside();
        //}

        //partial void Front_TouchUpInside(UIButton sender)
        //{
        //    //viewPopup.Hidden = false;
        //    currentButton = "front";
        //    ActionButton_TouchUpInside();
        //}

        UIImagePickerController imagePicker;

        void GalleryButtona_TouchUpInside()
        {
            //viewPopup.Hidden = true;
            imagePicker = new UIImagePickerController();
            imagePicker.PrefersStatusBarHidden();
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imagePicker.AllowsEditing = true;
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;
            PresentViewController(imagePicker, true, () => { });

        }



        void CameraBtna_TouchUpInside()
        {
            Console.WriteLine("Capture button clicked ");
            try
            {
                //viewPopup.Hidden = true;
                imagePicker = new UIImagePickerController();
                imagePicker.PrefersStatusBarHidden();
                imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                imagePicker.AllowsEditing = true;
                imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
                imagePicker.Canceled += Handle_Canceled;
                PresentViewController(imagePicker, true, () => { });
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception occured :: " + exc.Message);
            }

        }
        public void setImage(UIImage Image)
        {
            switch (currentButton)
            {
                case "front":
                    //Right_Image.SizeToFit();
                    //Right_Image.SetImage(Image, UIControlState.Normal);
                    //Right_Image.TintColor = UIColor.Clear;
                    //Right_Image.SetImage(Image.SetNilValueForKey)
                    Front.TintColor = UIColor.Clear;
                    Front.SetBackgroundImage(Image, UIControlState.Normal);
                    FrontCarImageUploaded = true;
                    Front.Layer.BorderColor = UIColor.Black.CGColor;

                    // Right_Image.SizeToFit();
                    break;
                case "back":
                    Back.TintColor = UIColor.Clear;
                    Back.SetBackgroundImage(Image, UIControlState.Normal);
                    // Left_Image.SetBackgroundImage(Image, UIControlState.Normal);
                    BackCarImageUploaded = true;
                    Back.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                case "right":
                    Right.TintColor = UIColor.Clear;
                    Right.SetBackgroundImage(Image, UIControlState.Normal);
                    RightCarImageUploaded = true;
                    Right.Layer.BorderColor = UIColor.Black.CGColor;
                    /// Front.SetBackgroundImage(Image, UIControlState.Normal);
                    break;
                case "left":
                    Left.TintColor = UIColor.Clear;
                    Left.SetBackgroundImage(Image, UIControlState.Normal);
                    LeftCarImageUploaded = true;
                    Left.Layer.BorderColor = UIColor.Black.CGColor;
                    /// Odometer.SetBackgroundImage(Image, UIControlState.Normal);
                    break;
                case "seat":
                    Seat.TintColor = UIColor.Clear;
                    Seat.SetBackgroundImage(Image, UIControlState.Normal);
                    ///Seat.SetBackgroundImage(Image, UIControlState.Normal);
                    SeatCarImageUploaded = true;
                    Seat.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                case "seats":
                    Seats.TintColor = UIColor.Clear;
                    Seats.SetBackgroundImage(Image, UIControlState.Normal);

                    BackSeatImageUploaded = true;
                    Seats.Layer.BorderColor = UIColor.Black.CGColor;
                    ///Seat.SetBackgroundImage(Image, UIControlState.Normal);
                    break;
                case "dashboard":
                    Dashboard.TintColor = UIColor.Clear;
                    Dashboard.SetBackgroundImage(Image, UIControlState.Normal);
                    DashBoardImageUploaded = true;
                    Dashboard.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                case "odometer":
                    Odometer.TintColor = UIColor.Clear;
                    Odometer.SetBackgroundImage(Image, UIControlState.Normal);
                    OdometerImageUploaded = true;
                    Odometer.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                case "rim":
                    Rim.TintColor = UIColor.Clear;
                    Rim.SetBackgroundImage(Image, UIControlState.Normal);
                    RimImageUploaded = true;
                    Rim.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                case "VIN":
                    VIN.TintColor = UIColor.Clear;
                    VIN.SetBackgroundImage(Image, UIControlState.Normal);
                    VINImageUplaoded = true;
                    VIN.Layer.BorderColor = UIColor.Black.CGColor;
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                    break;
            }
        }

        public void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {

            var originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;

            var meta = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;

            try
            {
                if (originalImage != null)
                {
                    Console.WriteLine("got the original image");
                    //Right_Image.SetBackgroundImage(originalImage, UIControlState.Normal);

                    //imageView.Image = originalImage; // display
                    setImage(originalImage);
                    var documentsDirectory = Environment.GetFolderPath
                                              (Environment.SpecialFolder.Personal);
                    string buttonName = currentButton + ".png";
                    string pngFilename = System.IO.Path.Combine(documentsDirectory, buttonName);
                    NSData imgData = originalImage.AsJPEG(0.0f);
                    NSError err = null;
                    if (imgData.Save(pngFilename, false, out err))
                    {
                        Console.WriteLine("saved as " + pngFilename);
                    }
                    else
                    {
                        Console.WriteLine("NOT saved as" + pngFilename + " because" + err.LocalizedDescription);
                    }

                    //ALAssetsLibrary library = new ALAssetsLibrary();

                    //library.WriteImageToSavedPhotosAlbum(originalImage.CGImage, meta, (assetUrl, error) => {
                    //    Console.WriteLine("assetUrl:" + assetUrl);
                    //});
                    var imagea = UIImage.LoadFromData(imgData);

                    //imagea.SaveToPhotosAlbum((AppraisalPhotoAlbum, eror) =>
                    //{

                    //});

                    imagePicker.DismissModalViewController(true);
                    Amazon.Aws amazonS3 = new Amazon.Aws();
                    Task.Run(() => amazonS3.UploadFile(pngFilename));
                    ActivityLoader();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception occured :: " + exc.Message);
            }

            // dismiss the picker
            //imagePicker.DismissModalViewController(true);
            //imagePicker.DismissModalViewControllerAnimated(true);
        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            imagePicker.DismissModalViewController(true);
            //imagePicker.DismissModalViewControllerAnimated(true);
        }

        protected PhotoGraphController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public async void ActivityLoader()
        {
            //LoadingOverlay loadPop;
            //var bounds = UIScreen.MainScreen.Bounds;
            //loadPop = new LoadingOverlay(bounds);
            //View.Add(loadPop);
            Utility.ShowLoadingIndicator(this.View, "Uploading ...", true);
            await Task.Delay(2000);
            Utility.HideLoadingIndicator(this.View);
            //loadPop.Hide();
        }

        public override void ViewDidLoad()
        {
            {

                base.ViewDidLoad();

                Front.Layer.BorderWidth = 2.0f;
                Front.Layer.BorderColor = UIColor.Black.CGColor;
                Back.Layer.BorderWidth = 2.0f;
                Back.Layer.BorderColor = UIColor.Black.CGColor;
                Right.Layer.BorderWidth = 2.0f;
                Right.Layer.BorderColor = UIColor.Black.CGColor;
                Left.Layer.BorderWidth = 2.0f;
                Left.Layer.BorderColor = UIColor.Black.CGColor;
                Seat.Layer.BorderWidth = 2.0f;
                Seat.Layer.BorderColor = UIColor.Black.CGColor;
                Seats.Layer.BorderWidth = 2.0f;
                Seats.Layer.BorderColor = UIColor.Black.CGColor;
                Dashboard.Layer.BorderWidth = 2.0f;
                Dashboard.Layer.BorderColor = UIColor.Black.CGColor;
                Odometer.Layer.BorderWidth = 2.0f;
                Odometer.Layer.BorderColor = UIColor.Black.CGColor;
                Rim.Layer.BorderWidth = 2.0f;
                Rim.Layer.BorderColor = UIColor.Black.CGColor;
                VIN.Layer.BorderWidth = 2.0f;
                VIN.Layer.BorderColor = UIColor.Black.CGColor;

                setPersistedImage();

            }

        }

        private void setPersistedImage()
        {


            UIImage rightImage = LoadImage("right");
            if (rightImage != null)
            {
                Right.SetBackgroundImage(rightImage, UIControlState.Normal);
                Right.TintColor = UIColor.Clear;
                RightCarImageUploaded = true;
                Right.Layer.BorderColor = UIColor.Black.CGColor;
            }
            UIImage leftImage = LoadImage("left");
            if (leftImage != null)
            {
                Left.TintColor = UIColor.Clear;
                Left.SetBackgroundImage(leftImage, UIControlState.Normal);
                LeftCarImageUploaded = true;
                Left.Layer.BorderColor = UIColor.Black.CGColor;

            }
            UIImage frontImage = LoadImage("front");
            if (frontImage != null)
            {

                Front.TintColor = UIColor.Clear;
                Front.SetBackgroundImage(frontImage, UIControlState.Normal);
                FrontCarImageUploaded = true;
                Front.Layer.BorderColor = UIColor.Black.CGColor;
            }
            UIImage backImage = LoadImage("back");
            if (backImage != null)
            {
                Back.TintColor = UIColor.Clear;
                Back.SetBackgroundImage(backImage, UIControlState.Normal);
                BackCarImageUploaded = true;
                Back.Layer.BorderColor = UIColor.Black.CGColor;

            }
            UIImage seatImage = LoadImage("seat");
            if (seatImage != null)
            {
                Seat.TintColor = UIColor.Clear;
                Seat.SetBackgroundImage(seatImage, UIControlState.Normal);
                SeatCarImageUploaded = true;
                Left.Layer.BorderColor = UIColor.Black.CGColor;
            }
            UIImage seatsImage = LoadImage("seats");
            if (seatsImage != null)
            {
                Seats.TintColor = UIColor.Clear;
                Seats.SetBackgroundImage(seatsImage, UIControlState.Normal);
                BackSeatImageUploaded = true;
                Seats.Layer.BorderColor = UIColor.Black.CGColor;
            }
            UIImage dashImage = LoadImage("dashboard");
            if (dashImage != null)
            {
                Dashboard.TintColor = UIColor.Clear;
                Dashboard.SetBackgroundImage(dashImage, UIControlState.Normal);
                DashBoardImageUploaded = true;
                Dashboard.Layer.BorderColor = UIColor.Black.CGColor;

            }
            UIImage odoImage = LoadImage("odometer");
            if (odoImage != null)
            {
                Odometer.TintColor = UIColor.Clear;
                Odometer.SetBackgroundImage(odoImage, UIControlState.Normal);
                OdometerImageUploaded = true;
                Odometer.Layer.BorderColor = UIColor.Black.CGColor;

            }
            UIImage rimImage = LoadImage("rim");
            if (rimImage != null)
            {
                Rim.TintColor = UIColor.Clear;
                Rim.SetBackgroundImage(rimImage, UIControlState.Normal);
                RimImageUploaded = true;
                Rim.Layer.BorderColor = UIColor.Black.CGColor;

            }
            UIImage vinImage = LoadImage("VIN");
            if (vinImage != null)
            {
                VIN.TintColor = UIColor.Clear;
                VIN.SetBackgroundImage(vinImage, UIControlState.Normal);
                VINImageUplaoded = true;
                VIN.Layer.BorderColor = UIColor.Black.CGColor;
            }

        }
        public UIImage LoadImage(string filename)


        {
            string buttonName = filename + ".png";
            var documentsDirectory = Environment.GetFolderPath
                                             (Environment.SpecialFolder.Personal);
            string pngFilename = System.IO.Path.Combine(documentsDirectory, buttonName);

            UIImage image = null;
            //var jpegData = NSData.FromFile(pngFilename);
            //if (jpegData != null)
            image = UIImage.FromFile(pngFilename);
            return image;
        }
    }
}