using AppraisalApp.Models;
using AppraisalApp.Utilities;
using CoreGraphics;
using ExtAppraisalApp;
using ExtAppraisalApp.Models;
using ExtAppraisalApp.Services;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;

namespace AppraisalApp
{
    public partial class AfterMarketViewController : UIViewController
    {
        partial void Btn_SaveAfterMarket_Activated(UIBarButtonItem sender)
        {
            SaveAfterMarketFactoryOptions();
        }

        UITableView table;
        IEnumerable<AfterMarketSection> fctoption = new List<AfterMarketSection>();



        void Switchele_ValueChanged(object sender, EventArgs e)
        {
            UISwitch switchvalue = (UISwitch)sender;
            int value = Convert.ToInt32(switchvalue.Tag);
            foreach (var aftermarket in AppDelegate.appDelegate.afterMarketOptions.sonicAfterMarketList)
            {
                if (aftermarket.AfterMarketOptionId == value)
                {
                    if (switchvalue.On)
                    {
                        aftermarket.Is_AfterMarketOption_Select = true;

                    }
                    else
                    {
                        aftermarket.Is_AfterMarketOption_Select = false;
                    }
                }
            }

        }

        public AfterMarketViewController(IntPtr handle) : base(handle)
        {
        }
        public static class globalInde
        {
            public static string selectedSegmentIndex = null;
            public static string oldselectedSegmentIndex = null;
        }
        partial void SegmentValue_Changed(UISegmentedControl sender)
        {
            string segmentID = ReconditionSegment.SelectedSegment.ToString();
            if ((globalInde.selectedSegmentIndex != null))
            {
                globalInde.oldselectedSegmentIndex = globalInde.selectedSegmentIndex.ToString();


            }
            if (segmentID == "1")
            {
                this.masterAMFO.Hidden = true;
                MasterAdditionalAMFO.Hidden = false;
                //base.ViewDidLoad();
                //var width = View.Bounds.Width;
                //var height = View.Bounds.Height;

                //table = new UITableView(new CGRect(0, 0, width, height));
                //table.AutoresizingMask = UIViewAutoresizing.All;

                //AppDelegate.appDelegate.fctoption = ServiceFactory.getWebServiceHandle().GetFactoryOptionsKBB(AppDelegate.appDelegate.vehicleID, AppDelegate.appDelegate.storeId, AppDelegate.appDelegate.invtrId, 432110);
                //List<string> tableItems = new List<string>();
                //foreach (var category in AppDelegate.appDelegate.fctoption)
                //{
                //    string str = category.Caption;
                //    tableItems.Add(str);
                //}

                //table.Source = new FactoryOptionSource(tableItems.ToArray(), this);
                //Add(table);
            }
            else
            {

                this.masterAMFO.Hidden = false;
                MasterAdditionalAMFO.Hidden = true;

            }
        }


        public void SaveAfterMarketFactoryOptions()
        {
            SIMSResponseData responseStatus;
            VehicleAfterMarketOptions vehicleFactoryOptions = new VehicleAfterMarketOptions();
            AnswerWrapper answers = new AnswerWrapper();
            answers.Answers = new List<ReconAnsKBB>();

            foreach(var ans in AppDelegate.appDelegate.afterMarketOptions.aftermarketQuestions.data){
                
                foreach(var item in ans.questions)
                {
                    ReconAnsKBB answer = new ReconAnsKBB();

                    answer.label = item.label;
                    answer.questionId = item.questionId;
                    answer.questionType = item.questionType;
                    answer.tags = item.tags;
                    answer.value = item.value;
                    answer.vehicleConditionCategory = item.vehicleConditionCategory;
                    answer.vehicleConditionCategoryName = item.vehicleConditionCategoryName;
                    answer.vehicleSectionId = item.vehicleSectionId;
                    answer.vehicleSectionName = item.vehicleSectionName;
                    answer.comment = item.comment;
                    answers.Answers.Add(answer);
                }
            }
            vehicleFactoryOptions.Answer = new AnswerWrapper();
            vehicleFactoryOptions.Answer.Answers = new List<ReconAnsKBB>();
            vehicleFactoryOptions.Answer.VehicleID = AppDelegate.appDelegate.vehicleID;
            vehicleFactoryOptions.Answer.StoreID = AppDelegate.appDelegate.storeId;
            vehicleFactoryOptions.Answer.InvtrID = AppDelegate.appDelegate.invtrId;

            vehicleFactoryOptions.Answer.Answers = answers.Answers;
            vehicleFactoryOptions.vehicleFactoryOptions = new VehicleFactoryOptions();
            vehicleFactoryOptions.vehicleFactoryOptions.AlternateFactoryOptionsLst = new List<AlternateFactoryOptions>();
            vehicleFactoryOptions.vehicleFactoryOptions.AlternateFactoryOptionsLst = AppDelegate.appDelegate.afterMarketOptions.sonicAfterMarketList;
            vehicleFactoryOptions.vehicleFactoryOptions.VehicleID = AppDelegate.appDelegate.vehicleID;
            vehicleFactoryOptions.vehicleFactoryOptions.StoreID = AppDelegate.appDelegate.storeId;
            vehicleFactoryOptions.vehicleFactoryOptions.InvtrID = AppDelegate.appDelegate.invtrId;

            //Logic to add the Selected Factory options

            responseStatus = ServiceFactory.getWebServiceHandle().SaveAfterMarketFactoryOptions(vehicleFactoryOptions);
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.masterAMFO.Hidden = false;
            MasterAdditionalAMFO.Hidden = true;
            AppDelegate.appDelegate.afterMarketOptions = ServiceFactory.getWebServiceHandle().GetAltenateFactoryOptions(AppDelegate.appDelegate.vehicleID, AppDelegate.appDelegate.storeId, AppDelegate.appDelegate.invtrId, AppDelegate.appDelegate.prospectId);
            int y = 0;
            foreach (var option in AppDelegate.appDelegate.afterMarketOptions.sonicAfterMarketList)
            {
                UISwitch switchele = new UISwitch();
                switchele.On = Convert.ToBoolean(option.Is_AfterMarketOption_Select);
                switchele.Tag = option.AfterMarketOptionId;
                switchele.ValueChanged += Switchele_ValueChanged;
                UILabel label = new UILabel();
                switchele.Frame = new CGRect(20, y + 33, UIScreen.MainScreen.Bounds.Width, 100);
                label.Frame = new CGRect(80, y, UIScreen.MainScreen.Bounds.Width, 100);
                y = y + 50;
                label.UserInteractionEnabled = true;
                label.Text = option.Description;
                AMFO.AddSubview(switchele);
                AMFO.AddSubview(label);

            }
            var width = View.Bounds.Width;
            var height = View.Bounds.Height;

            table = new UITableView(new CGRect(0, 0, width, height));
            table.AutoresizingMask = UIViewAutoresizing.All;

            List<string> tableItems = new List<string>();

            foreach (var option in AppDelegate.appDelegate.afterMarketOptions.aftermarketQuestions.data)
            {

                string str = option.Caption;
                tableItems.Add(str);

            }
            table.Source = new FactoryOptionSource(tableItems.ToArray(), this);
            AdditionAMFO.AddSubview(table);

            //Add(table);


        }

    }
}